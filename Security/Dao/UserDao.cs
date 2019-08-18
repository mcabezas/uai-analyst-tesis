/**
 * Created by Marcelo Cabezas on 2019-May-12 11:03:36 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Data;
using Commons.Generics;
using DBW.DBWrapper.Result;
using DBW.DBWrapper.Result.impl;
using Layers.Dao;
using Layers.Model.State.Persistence;
using Security.Model;

namespace Security.Dao
{
    public class UserDao : GenericEntityDao<User, int>
    {
        public override int Insert(User aUser)
        {
            int newUserId = InsertUser(aUser);

            InsertPermissions(aUser, newUserId);

            InsertGroups(aUser, newUserId);

            return newUserId;
        }

        public override User FindById(int anId)
        {
            User aUser = FindByIdLazyMode(anId);

            aUser.Permissions = FindPermissionsByUserIdLazyMode(anId);

            aUser.Groups = FindGroupsByUserIdLazyMode(anId);

            return aUser;
        }

        private IMCollection<Permission> FindPermissionsByUserIdLazyMode(int aUserId)
        {
            const string queryUserPermission = "SELECT p.id, p.description " 
                                                + "FROM user_permission up "
                                                + "join PERMISSION p on up.permission_id=p.id "
                                                + "WHERE up.user_id=@USER_ID ";
            IMCollection<DbRow> dbPermissionRows = Database.ExecuteNativeQuery(queryUserPermission, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@USER_ID", DbType.Int32));
                command.Parameters["@USER_ID"].Value = aUserId;
            });
            ResultTransformer<Permission> transformerPermission = new ResultTransformer<Permission>(dbPermissionRows);
            return transformerPermission.Transform();
        }
        
        private IMCollection<Group> FindGroupsByUserIdLazyMode(int aUserId)
        {
            const string queryUserPermission = "SELECT g.id, g.description " 
                                               + "FROM user_group ug "
                                               + "join _GROUP g on ug.group_id=g.id "
                                               + "WHERE ug.user_id=@USER_ID ";
            IMCollection<DbRow> dbGroupRows = Database.ExecuteNativeQuery(queryUserPermission, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@USER_ID", DbType.Int32));
                command.Parameters["@USER_ID"].Value = aUserId;
            });
            ResultTransformer<Group> transformerPermission = new ResultTransformer<Group>(dbGroupRows);
            return transformerPermission.Transform();
        }


        public override User FindByIdLazyMode(int anId)
        {
            const string query = "SELECT id, first_name, last_name, email, idiom_id FROM _user WHERE id=@ID";

            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
            
            ResultTransformer<User> transformer = new ResultTransformer<User>(dbRows);
            return transformer.Transform().GetFirstOrDefault(new User());
        }

        public override IMCollection<User> FindAll()
        {
            const string query = "SELECT id, first_name, last_name FROM _user";
            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) => { });
            ResultTransformer<User> transformer = new ResultTransformer<User>(dbRows);
            return transformer.Transform();
        }

        public override User Update(User anEntity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(User aUser)
        {
            RemoveUserPermission(aUser.Id);

            RemoveUser(aUser.Id);
        }

        public override void DeleteById(int aUserId)
        {
            RemoveUserPermission(aUserId);

            RemoveUser(aUserId);

        }

        private void RemoveUser(int aUserId)
        {
            const string query = "DELETE FROM _user where id=@ID";
            Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = aUserId;
            });
        }
        
        private void RemoveUserPermission(int aUserId)
        {
            const string queryUserPermission = "DELETE FROM user_permission where user_id=@USER_ID";
            Database.ExecuteNativeQuery(queryUserPermission, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@USER_ID", DbType.Int32));
                command.Parameters["@USER_ID"].Value = aUserId;
            });
        }

        public override void DeleteAll()
        {
            Database.ExecuteScalar("DELETE FROM user_permission");
            Database.ExecuteScalar("DELETE FROM user_group");
            Database.ExecuteScalar("DELETE FROM _user");
        }


        private void InsertGroups(User aUser, int insertedUserId)
        {
            InsertRelationshipWithPersistedGroups(aUser.Groups, insertedUserId);

            InsertRelationshipWithNonPersistedGroups(aUser.Groups, insertedUserId);
        }

        private void InsertPermissions(User aUser, int insertedUserId)
        {
            InsertRelationshipWithPersistedPermissions(aUser.Permissions, insertedUserId);

            InsertRelationshipWithNonPersistedPermissions(aUser.Permissions, insertedUserId);
        }

        private void InsertRelationshipWithPersistedPermissions(IMCollection<Permission> permissions, int insertedUserId)
        {
            InsertPermissionsRelationship(
                permissions.Filter(permission => 
                    new PersistedState().CanHandle(permission)),
                insertedUserId);
        }

        private void InsertRelationshipWithNonPersistedPermissions(IMCollection<Permission> permissions, int insertedUserId)
        {
            IMCollection<Permission> permissionsNotInserted =
                permissions.Filter(permission => new NotPersistedState().CanHandle(permission));

            IMCollection<Permission> justInsertedPermissions = new PermissionDao().Insert(permissionsNotInserted);
            
            InsertPermissionsRelationship(justInsertedPermissions, insertedUserId);
        }
        
        private void InsertRelationshipWithPersistedGroups(IMCollection<Group> groups, int insertedUserId)
        {
            InsertGroupsRelationship(
                groups.Filter(permission => 
                    new PersistedState().CanHandle(permission)),
                insertedUserId);
        }

        private void InsertRelationshipWithNonPersistedGroups(IMCollection<Group> groups, int insertedUserId)
        {
            IMCollection<Group> groupsNotInserted =
                groups.Filter(group => new NotPersistedState().CanHandle(group));

            IMCollection<Group> justInsertedGroups = new GroupDao().Insert(groupsNotInserted);
            
            InsertGroupsRelationship(justInsertedGroups, insertedUserId);
        }

        private void InsertGroupsRelationship(IMCollection<Group> groups, int userId)
        {
            groups.ForEach(group =>
                {
                    const string queryGroupPermission = "INSERT INTO user_group (user_id, group_id) " +
                                                        "VALUES (@USER_ID, @GROUP_ID)";

                    Database.ExecuteSimpleInsert(queryGroupPermission, (command, newParameter) =>
                    {
                        command.Parameters.Add(newParameter("@USER_ID", DbType.Int32));
                        command.Parameters["@USER_ID"].Value = userId;
                        command.Parameters.Add(newParameter("@GROUP_ID", DbType.Int32));
                        command.Parameters["@GROUP_ID"].Value = group.Id;
                    });
                }
            );
        }

        private int InsertUser(User aUser)
        {
            const string query = "INSERT INTO _user"
                                 + "(first_name, last_name, email, idiom_id) "
                                 + "VALUES (@FIRSTNAME, @LASTNAME, @EMAIL, @IDIOM_ID)";

            return Database.ExecuteInsert(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@FIRSTNAME", DbType.String));
                command.Parameters["@FIRSTNAME"].Value = aUser.FirstName;
                
                command.Parameters.Add(newParameter("@LASTNAME", DbType.String));
                command.Parameters["@LASTNAME"].Value = aUser.LastName;
                
                command.Parameters.Add(newParameter("@EMAIL", DbType.String));
                command.Parameters["@EMAIL"].Value = aUser.Email;

                command.Parameters.Add(newParameter("@IDIOM_ID", DbType.Int32));
                command.Parameters["@IDIOM_ID"].Value = aUser.Idiom.DbIdValue();
            });
        }

        private void InsertPermissionsRelationship(IMCollection<Permission> permissions, int groupId)
        {
            permissions.ForEach(permission =>
                {
                    const string queryGroupPermission = "INSERT INTO user_permission (user_id, permission_id) " +
                                                        "VALUES (@USER_ID, @PERMISSION_ID)";

                    Database.ExecuteSimpleInsert(queryGroupPermission, (command, newParameter) =>
                    {
                        command.Parameters.Add(newParameter("@USER_ID", DbType.Int32));
                        command.Parameters["@USER_ID"].Value = groupId;
                        command.Parameters.Add(newParameter("@PERMISSION_ID", DbType.Int32));
                        command.Parameters["@PERMISSION_ID"].Value = permission.Id;
                    });
                }
            );
        }
    }
}
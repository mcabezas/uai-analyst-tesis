/**
 * Created by Marcelo Cabezas on 2019-May-12 11:03:36 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Data;
using Commons.Generics;
using Commons.Generics.impl;
using DBW.DBWrapper.Result;
using DBW.DBWrapper.Result.impl;
using Layers.Dao;
using Layers.Model.State.Persistence;
using Security.Model;

namespace Security.Dao
{
    public class GroupDao : GenericEntityDao<Group, int>
    {
        public override int Insert(Group aGroup)
        {
            int newGroupId = InsertGroup(aGroup);

            InsertRelationshipWithNonPersistedPermissions(aGroup.Permissions, newGroupId);

            InsertRelationshipWithPersistedPermissions(aGroup.Permissions, newGroupId);

            return newGroupId;
        }

        public IMCollection<Group> Insert(IMCollection<Group> groups)
        {
            IMCollection<Group> insertedPermissions = new MCollection<Group>();
            groups.ForEach(group =>
            {
                int groupId = Insert(group);
                insertedPermissions.Add(new Group() {
                    Id = groupId, Description = group.Description, Permissions = group.Permissions
                });
            });
            return insertedPermissions;
        }

        private void InsertRelationshipWithPersistedPermissions(IMCollection<Permission> permissions, int groupId)
        {
            InsertPermissionsRelationship(
                permissions.Filter(permission => 
                    new PersistedState().CanHandle(permission)),
                groupId);
        }

        private void InsertRelationshipWithNonPersistedPermissions(IMCollection<Permission> permissions, int groupId)
        {
            IMCollection<Permission> permissionsNotInserted =
                permissions.Filter(permission => new NotPersistedState().CanHandle(permission));

            IMCollection<Permission> justInsertedPermissions = new PermissionDao().Insert(permissionsNotInserted);
            
            InsertPermissionsRelationship(justInsertedPermissions, groupId);
        }

        private void InsertPermissionsRelationship(IMCollection<Permission> permissions, int groupId)
        {
            permissions.ForEach(permission =>
                {
                    const string queryGroupPermission = "INSERT INTO group_permission (group_id, permission_id) " +
                                                        "VALUES (@GROUP_ID, @PERMISSION_ID)";

                    Database.ExecuteSimpleInsert(queryGroupPermission, (command, newParameter) =>
                    {
                        command.Parameters.Add(newParameter("@GROUP_ID", DbType.Int32));
                        command.Parameters["@GROUP_ID"].Value = groupId;
                        command.Parameters.Add(newParameter("@PERMISSION_ID", DbType.Int32));
                        command.Parameters["@PERMISSION_ID"].Value = permission.Id;
                    });
                }
            );
        }

        private int InsertGroup(Group aGroup)
        {
            const string queryGroup = "INSERT INTO _group (description) " +
                                      "VALUES (@DESCRIPTION)";

            int groupId = Database.ExecuteInsert(queryGroup, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@DESCRIPTION", DbType.String));
                command.Parameters["@DESCRIPTION"].Value = aGroup.Description;
            });
            return groupId;
        }

        public override Group FindByIdLazyMode(int anId)
        {
            const string queryGroup = "SELECT id, description FROM _group WHERE id=@ID";
            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(queryGroup, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
            ResultTransformer<Group> transformerGroup = new ResultTransformer<Group>(dbRows);
            Group aGroup = transformerGroup.Transform().GetFirstOrDefault(new Group());

            return aGroup;
        }
        
        public override Group FindById(int anId)
        {
            Group aGroup = FindByIdLazyMode(anId);

            aGroup.Permissions = FindPermissionsByGroupIdLazyMode(anId);

            return aGroup;
        }


        private IMCollection<Permission> FindPermissionsByGroupIdLazyMode(int aGroupId)
        {
            const string queryGroupPermission = "SELECT p.id, p.description " 
                + "FROM group_permission gp "
                + "join PERMISSION p on gp.permission_id=p.id "
                + "WHERE gp.group_id=@GROUP_ID ";
            IMCollection<DbRow> dbPermissionRows = Database.ExecuteNativeQuery(queryGroupPermission, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@GROUP_ID", DbType.Int32));
                command.Parameters["@GROUP_ID"].Value = aGroupId;
            });
            ResultTransformer<Permission> transformerPermission = new ResultTransformer<Permission>(dbPermissionRows);
            return transformerPermission.Transform();
        }

        public override IMCollection<Group> FindAll()
        {
            const string query = "SELECT id, description FROM _group";
            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) => { });
            ResultTransformer<Group> transformer = new ResultTransformer<Group>(dbRows);
            return transformer.Transform();
        }

        public override Group Update(Group anEntity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(Group aGroup)
        {
            DeleteById(aGroup.Id);
        }

        public override void DeleteById(int aGroupId)
        {
            RemoveGroupPermission(aGroupId);

            RemoveGroup(aGroupId);
        }

        private void RemoveGroup(int aGroupId)
        {
            const string queryGroup = "DELETE FROM _group where id=@ID";
            Database.ExecuteNativeQuery(queryGroup, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = aGroupId;
            });
        }

        private void RemoveGroupPermission(int aGroupId)
        {
            const string queryGroupPermission = "DELETE FROM group_permission where group_id=@GROUP_ID";
            Database.ExecuteNativeQuery(queryGroupPermission, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@GROUP_ID", DbType.Int32));
                command.Parameters["@GROUP_ID"].Value = aGroupId;
            });
        }

        public override void DeleteAll()
        {
            Database.ExecuteScalar("DELETE FROM group_permission");

            Database.ExecuteScalar("DELETE FROM _group");
        }
    }
}
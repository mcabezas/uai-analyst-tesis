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
using Security.Model;

namespace Security.Dao.impl
{
    public class PermissionDao : AbstractEntityDao<Permission, int>
    {
        public override int Insert(Permission aPermission)
        {
            const string query = "INSERT INTO permission (description) " +
                                 "VALUES (@DESCRIPTION)"; 
            
            return Database.ExecuteInsert(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@DESCRIPTION", DbType.String));
                command.Parameters["@DESCRIPTION"].Value = aPermission.Description;
            });
        }

        public override Permission FindByIdLazyMode(int anId)
        {
            const string query = "SELECT id, description FROM permission WHERE id=@ID";

            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
            
            ResultTransformer<Permission> transformer = new ResultTransformer<Permission>(dbRows);
            return transformer.Transform().GetFirstOrDefault(Permission.NullPermission);
        }

        public override IMCollection<Permission> FindAll()
        {
            const string query = "SELECT id, description FROM permission";
            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) => { });
            ResultTransformer<Permission> transformer = new ResultTransformer<Permission>(dbRows);
            return transformer.Transform();
        }

        public override Permission Update(Permission aPermission)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(Permission aPermission)
        {
            DeleteById(aPermission.Id);
        }

        public override void DeleteById(int anId)
        {
            const string query = "DELETE FROM permission where id=@ID";
            Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
        }

        public override void DeleteAll()
        {
            const string query = "DELETE FROM permission";
            Database.ExecuteScalar(query);
        }
    }
}
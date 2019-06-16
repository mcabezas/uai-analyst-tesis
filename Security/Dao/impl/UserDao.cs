/**
 * Created by Marcelo Cabezas on 2019-May-12 11:03:36 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data;
using Commons.Generics;
using DBW.DBWrapper.Result;
using DBW.DBWrapper.Result.impl;
using Security.Model;
using static Security.Model.Entity;
using static Security.Model.User;

namespace Security.Dao.impl
{
    public class UserDao : AbstractEntityDao<User, int>
    {
        public override int Insert(User aUser)
        {
            const string query = "INSERT INTO _user(first_name, last_name, idiom_id) " +
                                 "VALUES (@FIRSTNAME, @LASTNAME, @IDIOM_ID)";

            return Database.ExecuteInsert(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@FIRSTNAME", DbType.String));
                command.Parameters["@FIRSTNAME"].Value = aUser.FirstName;
                command.Parameters.Add(newParameter("@LASTNAME", DbType.String));
                command.Parameters["@LASTNAME"].Value = aUser.LastName;
                command.Parameters.Add(newParameter("@IDIOM_ID", DbType.Int32));
                if (aUser.Idiom.Id == NullId) command.Parameters["@IDIOM_ID"].Value = DBNull.Value;
                else command.Parameters["@IDIOM_ID"].Value = aUser.Idiom.Id;

            });
        }

        public override User FindByIdLazyMode(int anId)
        {
            const string query = "SELECT id, first_name, last_name, idiom_id FROM _user WHERE id=@ID";

            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
            
            ResultTransformer<User> transformer = new ResultTransformer<User>(dbRows);
            return transformer.Transform().GetFirstOrDefault(NullUser);
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
            DeleteById(aUser.Id);
        }

        public override void DeleteById(int anId)
        {
            const string query = "DELETE FROM _user where id=@ID";
            Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
        }

        public override void DeleteAll()
        {
            const string query = "DELETE FROM _user";
            Database.ExecuteScalar(query);
        }
    }
}
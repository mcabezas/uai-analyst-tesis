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
using static Security.Model.User;

namespace Security.Dao.impl
{
    public class UserDao : AbstractEntityDao<User, int>
    {
        public override object Insert(User anEntity)
        {
            const string query = "INSERT INTO users(first_name, last_name) " +
                                 "VALUES (@FIRSTNAME, @LASTNAME)"; 
            
            return Database.ExecuteInsert(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@FIRSTNAME", DbType.String));
                command.Parameters["@FIRSTNAME"].Value = anEntity.FirstName;
                command.Parameters.Add(newParameter("@LASTNAME", DbType.String));
                command.Parameters["@LASTNAME"].Value = anEntity.LastName;
            });
        }

        public override User FindById(int anId)
        {
            const string query = "SELECT id, first_name, last_name FROM users WHERE id=@ID";

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
            const string query = "SELECT id, first_name, last_name FROM users";
            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) => { });
            ResultTransformer<User> transformer = new ResultTransformer<User>(dbRows);
            return transformer.Transform();
        }

        public override User Update(User anEntity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(User anEntity)
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteAll()
        {
            const string query = "DELETE FROM users";
            Database.ExecuteScalar(query);
        }
    }
}
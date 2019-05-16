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
    public class UserDao : AbstractDao<User, int>
    {
        public override void Insert(User anEntity)
        {
            const string query = "INSERT INTO USERS(first_name, last_name) " + 
                    "VALUES (@FIRSTNAME, @LASTNAME)";
            Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@FIRSTNAME", DbType.String));
                command.Parameters["@FIRSTNAME"].Value = anEntity.FirstName;
                command.Parameters.Add(newParameter("@LASTNAME", DbType.String));
                command.Parameters["@LASTNAME"].Value = anEntity.LastName;
            });
        }

        public override User FindById(int anId)
        {
            throw new System.NotImplementedException();
        }

        public override IMCollection<User> FindAll()
        {
            const string query = "SELECT id, first_name, last_name FROM users";
            IMCollection<DbRow> resultSet = Database.ExecuteNativeQuery(query, (command, newParameter) => { });
            ResultTransformer<User> transformer = new ResultTransformer<User>(resultSet);
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
            Database.ExecuteNativeNonQuery(query);
        }
    }
}
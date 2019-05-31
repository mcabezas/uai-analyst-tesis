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
    public class IdiomDao : AbstractEntityDao<Idiom, int>
    {
        public override int Insert(Idiom anEntity)
        {
            const string query = "INSERT INTO idiom (description) " +
                                 "VALUES (@DESCRIPTION)"; 
            
            return Database.ExecuteInsert(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@DESCRIPTION", DbType.String));
                command.Parameters["@DESCRIPTION"].Value = anEntity.Description;
            });
        }

        public override Idiom FindById(int anId)
        {
            const string query = "SELECT id, description FROM idiom WHERE id=@ID";

            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
            
            ResultTransformer<Idiom> transformer = new ResultTransformer<Idiom>(dbRows);
            return transformer.Transform().GetFirstOrDefault(Idiom.NullIdiom);
        }

        public override IMCollection<Idiom> FindAll()
        {
            const string query = "SELECT id, description FROM idiom";
            IMCollection<DbRow> dbRows = Database.ExecuteNativeQuery(query, (command, newParameter) => { });
            ResultTransformer<Idiom> transformer = new ResultTransformer<Idiom>(dbRows);
            return transformer.Transform();
        }

        public override Idiom Update(Idiom anEntity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(Idiom anEntity)
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteById(int anId)
        {
            const string query = "DELETE FROM idiom where id=@ID";
            Database.ExecuteNativeQuery(query, (command, newParameter) =>
            {
                command.Parameters.Add(newParameter("@ID", DbType.Int32));
                command.Parameters["@ID"].Value = anId;
            });
        }

        public override void DeleteAll()
        {
            const string query = "DELETE FROM idiom";
            Database.ExecuteScalar(query);
        }
    }
}
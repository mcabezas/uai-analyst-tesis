/**
 * Created by Marcelo Cabezas on 2019-May-12 11:20:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using DBW.DBWrapper.Core.impl;
using DBW.DBWrapper.Engine;

namespace Security.Dao.impl
{
    public abstract class AbstractDao<TEntity, TPrimaryKey> : IDao<TEntity, TPrimaryKey>
    {
        protected readonly IDatabase Database = PostgresDatabaseFactory.Instance.GetDatabase();
        
        public abstract void Insert(TEntity anEntity);

        public abstract TEntity FindById(TPrimaryKey anId);

        public abstract IMCollection<TEntity> FindAll();

        public abstract TEntity Update(TEntity anEntity);

        public abstract void Delete(TEntity anEntity);
        public abstract void DeleteAll();
        
    }
}
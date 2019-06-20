/**
 * Created by Marcelo Cabezas on 2019-May-12 11:20:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using DBW.DBWrapper.Core.impl;
using DBW.DBWrapper.Engine;

namespace Layers.Dao
{
    public abstract class GenericEntityDao<TEntity, TPrimaryKey> : IDao<TEntity, TPrimaryKey>
    {
        protected readonly IDatabase Database = PostgresDatabaseFactory.Instance.GetDatabase();
//        protected readonly IDatabase Database = SqlServerDatabaseFactory.Instance.GetDatabase();
        
        public abstract int Insert(TEntity aUser);

        public abstract TEntity FindByIdLazyMode(TPrimaryKey anId);
        public virtual TEntity FindById(TPrimaryKey anId)
        {
            return FindByIdLazyMode(anId);
        }

        public abstract IMCollection<TEntity> FindAll();

        public abstract TEntity Update(TEntity anEntity);

        public abstract void Delete(TEntity anEntity);
        
        public abstract void DeleteById(TPrimaryKey anId);

        public abstract void DeleteAll();
        
    }
}
/**
 * Created by Marcelo Cabezas on 2019-May-12 11:20:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using DBW.DBWrapper.Core.impl;
using DBW.DBWrapper.Engine;

namespace Security.DAO.impl
{
    public abstract class AbstractDao<TEntity, TPK> : IDao<TEntity, TPK>
    {
        protected readonly IDatabase Database = PostgresDatabaseFactory.Instance.GetDatabase();
        
        public abstract TEntity Insert(TEntity entity);

        public abstract TEntity FindById(TPK entity);

        public abstract TEntity Update(TEntity entity);

        public abstract void Delete(TEntity entity);
    }
}
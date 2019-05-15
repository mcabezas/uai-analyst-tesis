/**
 * Created by Marcelo Cabezas on 2019-May-13 7:18:58 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Security.DAO;

namespace Security.Service.impl
{
    public class GenericService<TEntity, TPrimaryKey> : IService<TEntity, TPrimaryKey>
    {
        protected IDao<TEntity, TPrimaryKey> Dao;

        public TEntity Insert(TEntity entity)
        {
            return Dao.Insert(entity);
        }

        public TEntity FindById(TPrimaryKey id)
        {
            return Dao.FindById(id);
        }

        public TEntity Update(TEntity entity)
        {
            return Dao.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Dao.Delete(entity);
        }
    }
}
/**
 * Created by Marcelo Cabezas on 2019-May-13 7:18:58 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using Security.Dao;
using Security.Model;

namespace Security.Service.impl
{
    public class GenericService<TEntity, TPrimaryKey> : IService<TEntity, TPrimaryKey> where TEntity:Entity
    {
        protected IDao<TEntity, TPrimaryKey> Dao;

        public int Insert(TEntity anEntity)
        {
            return Dao.Insert(anEntity);
        }

        public TEntity FindByIdLazyMode(TPrimaryKey anId)
        {
            return Dao.FindByIdLazyMode(anId);
        }

        public TEntity FindById(TPrimaryKey anId)
        {
            return Dao.FindById(anId);
        }

        public IMCollection<TEntity> FindAll()
        {
            return Dao.FindAll();
        }

        public TEntity Update(TEntity anEntity)
        {
            return Dao.Update(anEntity);
        }

        public void Delete(TEntity anEntity)
        {
            Dao.Delete(anEntity);
        }
        
        public void DeleteById(TPrimaryKey anId)
        {
            Dao.DeleteById(anId);
        }

        public void DeleteAll()
        {
            Dao.DeleteAll();
        }
    }
}
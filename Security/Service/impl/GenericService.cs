/**
 * Created by Marcelo Cabezas on 2019-May-13 7:18:58 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using Security.Dao;

namespace Security.Service.impl
{
    public class GenericService<TEntity, TPrimaryKey> : IService<TEntity, TPrimaryKey>
    {
        protected IDao<TEntity, TPrimaryKey> Dao;

        public void Insert(TEntity anEntity)
        {
            Dao.Insert(anEntity);
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

        public void DeleteAll()
        {
            Dao.DeleteAll();
        }
    }
}
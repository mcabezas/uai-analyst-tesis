/**
 * Created by Marcelo Cabezas on 2019-May-12 7:21:50 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;

namespace Security.Service
{
    public interface IService<TEntity, in TPrimaryKey>
    {
        int Insert(TEntity anEntity);
        
        TEntity FindByIdLazyMode(TPrimaryKey anId);
        
        IMCollection<TEntity> FindAll();

        TEntity Update(TEntity anEntity);
        
        void Delete(TEntity anEntity);
        
        void DeleteById(TPrimaryKey anId);


        void DeleteAll();
    }
}
/**
 * Created by Marcelo Cabezas on 2019-May-12 7:21:50 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Security.DAO
{
    public interface IDao<TEntity, in TPrimaryKey>
    {
        TEntity Insert(TEntity entity);
        TEntity FindById(TPrimaryKey id);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
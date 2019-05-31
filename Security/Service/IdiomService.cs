/**
 * Created by Marcelo Cabezas on 2019-May-13 7:16:56 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Security.Dao.impl;
using Security.Model;
using Security.Service.impl;

namespace Security.Service
{
    public class IdiomService : GenericService<Idiom, int>

    {
        public IdiomService()
        {
            Dao = new IdiomDao();
        }

   }
}
/**
 * Created by Marcelo Cabezas on 2019-Jun-20 10:47:08 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Layers.Cache
{
    public interface ICache
    {
        void Put(object anElement);
        object Get(object anElementExample);
    }
}
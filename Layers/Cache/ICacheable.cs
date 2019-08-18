/**
 * Created by Marcelo Cabezas on 2019-Jun-20 11:09:15 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Layers.Cache
{
    public interface ICacheable<TId>
    {
        TId GetId();
    }
}
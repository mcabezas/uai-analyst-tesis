/**
 * Created by Marcelo Cabezas on 2019-May-11 4:57:04 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using DBW.DBWrapper.Engine;

namespace DBW.DBWrapper.Core
{
    public interface IDatabaseFactory
    {
        IDatabase GetDatabase();
    }
}
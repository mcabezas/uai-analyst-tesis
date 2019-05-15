/**
 * Created by Marcelo Cabezas on 2019-May-14 7:23:32 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;

namespace DBW.DBWrapper.Result
{
    public interface IResultTransformer <T>
    {
        IMCollection<T> Transform();
    }
}
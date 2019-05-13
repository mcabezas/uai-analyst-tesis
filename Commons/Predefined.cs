/**
 * Created by Marcelo Cabezas on 2019-Apr-28 12:26:47 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;

namespace Commons
{
    public static class Predefined
    {
        public static bool IsEmpty<T>(IMCollection<T> e)
        {
            return e?.Count == 0;
        }
    }
}
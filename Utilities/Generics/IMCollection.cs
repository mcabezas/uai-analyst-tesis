/**
 * Created by Marcelo Cabezas on 2019-May-04 7:17:36 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;

namespace Utilities.Generics
{
    public interface IMCollection<T> : IList<T>
    {
        IMCollection<T> From(IEnumerable<T> source);
        
        IMCollection<T> Filter(Predicate<T> aPredicate);
        
        void ForEach(Predicate<T> aPredicate, Action<T> anAction);
        
        void ForEach(Action<T> anAction);

        T GetFirst();

        bool IsEmpty();

    }
}
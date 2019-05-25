/**
 * Created by Marcelo Cabezas on 2019-May-04 7:17:36 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;

namespace Commons.Generics
{
    public interface IMCollection<TSource> : IList<TSource>
    {
        IMCollection<TSource> From(IEnumerable<TSource> source);
        
        IMCollection<TSource> Filter(Predicate<TSource> aPredicate);
        
        void ForEach(Predicate<TSource> aPredicate, Action<TSource> anAction);
        
        void ForEach(Action<TSource> anAction);

        TSource GetFirst();
        
        TSource GetFirstOrDefault(TSource source);


        bool IsEmpty();

    }
}
/**
 * Created by Marcelo Cabezas on 2019-Apr-27 7:27:09 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;

namespace Utilities.Generics
{
    public class Collection<T> : List<T>
    {
        public static Collection<T> From(IEnumerable<T> source)
        {
            Collection<T> collection = new Collection<T>();
            collection.AddRange(source);
            return collection;
        }

        public Collection<T> Filter(Predicate<T> condition)
        {
            Collection<T> result = new Collection<T>();
            ForEach(condition, item => { result.Add(item); });
            return result;
        }

        public void ForEach(Predicate<T> condition, Action<T> action)
        {
            ForEach(item => { if (condition(item)) action(item); });
        }

        public new void ForEach(Action<T> action)
        {
            foreach (var item in this) {
                action(item);
            }
        }
    }
}
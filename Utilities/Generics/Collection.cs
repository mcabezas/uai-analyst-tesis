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
    public class Collection<T> : List<T>, ICollection<T>
    {
        public ICollection<T> From(IEnumerable<T> source)
        {
            Collection<T> collection = new Collection<T>();
            collection.AddRange(source);
            return collection;
        }

        public ICollection<T> Filter(Predicate<T> aPredicate)
        {
            Collection<T> newCollection = new Collection<T>();
            ForEach(aPredicate, anItem => { newCollection.Add(anItem); });
            return newCollection;
        }

        public void ForEach(Predicate<T> aPredicate, Action<T> anAction)
        {
            ForEach(anItem => { if (aPredicate(anItem)) anAction(anItem); });
        }

        public new void ForEach(Action<T> anAction)
        {
            foreach (var anItem in this) {
                anAction(anItem);
            }
        }
        
        public T GetFirst()
        {
            return this[0];
        }

    }
}
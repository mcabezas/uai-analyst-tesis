/**
 * Created by Marcelo Cabezas on 2019-Apr-27 7:27:09 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;

namespace Commons.Generics.impl
{
    public sealed class MCollection<T> : List<T>, IMCollection<T>
    {
        public IMCollection<T> From(IEnumerable<T> source)
        {
            MCollection<T> mCollection = new MCollection<T>();
            mCollection.AddRange(source);
            return mCollection;
        }

        public IMCollection<T> Filter(Predicate<T> aPredicate)
        {
            MCollection<T> newMCollection = new MCollection<T>();
            ForEach(aPredicate, anItem => {
                newMCollection.Add(anItem);
            });
            return newMCollection;
        }

        public void ForEach(Predicate<T> aPredicate, Action<T> anAction)
        {
            ForEach(anItem => {
                if (aPredicate(anItem)) anAction(anItem);
            });
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

        public bool IsEmpty()
        {
            return Count==0;
        }
    }
}
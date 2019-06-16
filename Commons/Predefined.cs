/**
 * Created by Marcelo Cabezas on 2019-Apr-28 12:26:47 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Text.RegularExpressions;
using Commons.Generics;

namespace Commons
{
    public static class Predefined
    {
        public static bool IsEmpty<T>(IMCollection<T> e)
        {
            return e?.Count == 0;
        }
        
        public static bool Like(this string toSearch, string toFind)
        {
            return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
        }
    }
}
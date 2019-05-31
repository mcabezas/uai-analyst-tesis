/**
 * Created by Marcelo Cabezas on 2019-May-30 6:56:38 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Text;

namespace SecurityTest.Builder
{
    public static class RandomGenerator
    {
        public static string RandomString(int size, bool lowerCase)    
        {    
            StringBuilder builder = new StringBuilder();    
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }    
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
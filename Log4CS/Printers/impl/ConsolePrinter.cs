/**
 * Created by Marcelo Cabezas on 2019-May-04 1:39:18 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace Log4CS.Printers.impl
{
    public class ConsolePrinter : IPrintable
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
/**
 * Created by Marcelo Cabezas on 2019-Apr-19 11:02:25 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace TesterSuite.Core.Asserts
{
    public class AssertFactory
    {
        #region Properties

        private static readonly Lazy<AssertFactory> Lazy = new Lazy<AssertFactory>(() => new AssertFactory());
        public static Assert Instance
        {
            get => Lazy.Value.AssertMethod;
            set => Lazy.Value.AssertMethod = value;
        }

        public Assert AssertMethod;

        #endregion
        
        #region Constructor

        private AssertFactory()
        {
            AssertMethod = new Assert();
        }

        #endregion   
    }
}
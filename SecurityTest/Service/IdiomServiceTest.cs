/**
 * Created by Marcelo Cabezas on 2019-May-30 7:40:48 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Commons.Generics;
using Commons.Generics.impl;
using Security.Model;
using Security.Service;
using SecurityTest.Builder;
using TesterSuite.Core.Suites.impl;

namespace SecurityTest.Service
{
    public class IdiomServiceTest :  TestSuite
    {
        private readonly IService<Idiom, int> _idiomService = new IdiomService();
        private readonly IdiomBuilder _idiomBuilder = new IdiomBuilder();

        protected override void SetUp()
        {
            _idiomService.DeleteAll();
        }

        protected override IMCollection<Action> Tests()
        {
            MCollection<Action> tests = new MCollection<Action>
            {
                CanInsertAnIdiom,
                CanInsertMoreThanOneIdiom,
                CanDeleteAnIdiom,
            };

            return tests;
        }

        private void CanInsertAnIdiom()
        {
            Assertion.AreEqual(0, _idiomService.FindAll().Count);
            _idiomService.Insert(_idiomBuilder.Build());
            Assertion.AreEqual(1, _idiomService.FindAll().Count);
        }

        private void CanInsertMoreThanOneIdiom()
        {
            Assertion.AreEqual(0, _idiomService.FindAll().Count);
            _idiomService.Insert(_idiomBuilder.Build());
            _idiomService.Insert(_idiomBuilder.Build());
            _idiomService.Insert(_idiomBuilder.Build());
            Assertion.AreEqual(3, _idiomService.FindAll().Count);
        }


        private void CanDeleteAnIdiom()
        {
            Assertion.AreEqual(0, _idiomService.FindAll().Count);
            int insertedIdiomId = _idiomService.Insert(_idiomBuilder.Build());
            Assertion.AreEqual(1, _idiomService.FindAll().Count);
            _idiomService.DeleteById(insertedIdiomId);
            Assertion.AreEqual(0, _idiomService.FindAll().Count);
        }
    }
}
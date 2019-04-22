/**
 * Created by Marcelo Cabezas on 2019-Apr-19 3:55:29 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace TesterSuite.Core.Utilities
{
    public static class VType
    {
        public static List<Type> GetDerivedTypes(Type baseType, Assembly assembly)
        {
            // Get all types from the given assembly
            Type[] types = assembly.GetTypes();
            List<Type> derivedTypes = new List<Type>();

            for (int i = 0, count = types.Length; i < count; i++)
            {
                Type type = types[i];
                if (IsSubclassOf(type, baseType))
                {
                    // The current type is derived from the base type,
                    // so add it to the list
                    derivedTypes.Add(type);
                }
            }

            return derivedTypes;
        }

        public static bool IsSubclassOf(Type type, Type baseType)
        {
            if (type == null || baseType == null || type == baseType)
                return false;

            if (baseType.IsGenericType == false)
            {
                if (type.IsGenericType == false)
                    return type.IsSubclassOf(baseType);
            }
            else
            {
                baseType = baseType.GetGenericTypeDefinition();
            }

            type = type.BaseType;
            Type objectType = typeof(object);

            while (type != objectType && type != null)
            {
                Type curentType = type.IsGenericType ?
                    type.GetGenericTypeDefinition() : type;
                if (curentType == baseType)
                    return true;

                type = type.BaseType;
            }

            return false;
        }

        public static IEnumerable<TestSuite> GetAllTestSuites()
        {
            List<TestSuite> suites = new List<TestSuite>();
            List<Type> types = GetDerivedTypes(typeof(TestSuite),
                Assembly.GetExecutingAssembly());
            foreach (Type type in types)
            {
                suites.Add((TestSuite)Activator.CreateInstance(type));
            }
            return suites;
        }
    }
}
/**
 * Created by Marcelo Cabezas on 2019-May-13 8:46:48 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Linq;
using System.Reflection;
using Commons.Generics;
using Commons.Generics.impl;

namespace DBW.DBWrapper.Result.impl
{
    public class ResultTransformer<T> : IResultTransformer<T> where T : new()
    {
        private readonly IMCollection<DbRow> _dbRows;

        public ResultTransformer(IMCollection<DbRow> dbRows)
        {
            _dbRows = dbRows;
        }

        public IMCollection<T> Transform()
        {
            IMCollection<T> resultObjects = new MCollection<T>(); 
                
            _dbRows.ForEach(row =>
            {
                T item = new T();

                row.Columns.ForEach(column =>
                {
                    string columnName = column.Name?.ToLower().Replace("_", "");
                    PropertyInfo propertyInfo = GetPrivatePropertyInfo(typeof(T), columnName);
                    propertyInfo?.SetValue(item, Convert.ChangeType(column.Value, column.Type));
                });

                resultObjects.Add(item);
            });
            
            return resultObjects;
        }

        private PropertyInfo GetPrivatePropertyInfo(IReflect type, string propertyName)
        {
            
            const BindingFlags flags = BindingFlags.Instance
                                       | BindingFlags.GetProperty
                                       | BindingFlags.SetProperty
                                       | BindingFlags.GetField
                                       | BindingFlags.SetField
                                       | BindingFlags.NonPublic
                                       | BindingFlags.Public;

            var props = type.GetProperties(flags);
            return props.FirstOrDefault(propInfo => 
                string.Equals(propInfo.Name, 
                    propertyName, 
                    StringComparison.CurrentCultureIgnoreCase));
        }

    }

}
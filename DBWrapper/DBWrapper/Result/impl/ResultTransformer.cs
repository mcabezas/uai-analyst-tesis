/**
 * Created by Marcelo Cabezas on 2019-May-13 8:46:48 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Linq;
using System.Reflection;
using Commons;
using Commons.Generics;
using Commons.Generics.impl;

namespace DBW.DBWrapper.Result.impl
{
    public class ResultTransformer<TEntity> : IResultTransformer<TEntity> where TEntity : new()
    {
        private readonly IMCollection<DbRow> _dbRows;

        public ResultTransformer(IMCollection<DbRow> dbRows)
        {
            _dbRows = dbRows;
        }

        public IMCollection<TEntity> Transform()
        {
            IMCollection<TEntity> resultObjects = new MCollection<TEntity>(); 
                
            _dbRows.ForEach(row =>
            {
                TEntity entity = new TEntity();

                row.Columns.ForEach(column =>
                {
                    string columnName = column.Name?.ToLower();

                    if (column.Value == DBNull.Value) return;
                    
                    if (Predefined.Like(columnName,"%_id"))
                    {
                        //Getting relationship instance
                        PropertyInfo entityRelationshipInfo = GetPrivatePropertyInfo(typeof(TEntity), columnName?.Split("_id")[0]);
                        object entityRelationshipInstance = entityRelationshipInfo.GetValue(entity);

                        //Setting up relationship id
                        PropertyInfo entityRelationshipIdInfo = GetPrivatePropertyInfo(entityRelationshipInstance.GetType(), "id");
                        entityRelationshipIdInfo?.SetValue(entityRelationshipInstance, Convert.ChangeType(column.Value, column.Type));
                    }
                    else
                    {
                        columnName = columnName?.Replace("_", "");
                        PropertyInfo propertyInfo = GetPrivatePropertyInfo(typeof(TEntity), columnName);
                        propertyInfo?.SetValue(entity, Convert.ChangeType(column.Value, column.Type));
                    }
                });

                resultObjects.Add(entity);
            });
            
            return resultObjects;
        }

        private PropertyInfo GetPrivatePropertyInfo(IReflect aType, string aPropertyName)
        {
            
            const BindingFlags flags = BindingFlags.Instance
                                       | BindingFlags.GetProperty
                                       | BindingFlags.SetProperty
                                       | BindingFlags.GetField
                                       | BindingFlags.SetField
                                       | BindingFlags.NonPublic
                                       | BindingFlags.Public;

            var props = aType.GetProperties(flags);
            return props.FirstOrDefault(propInfo => 
                string.Equals(propInfo.Name, 
                    aPropertyName, 
                    StringComparison.CurrentCultureIgnoreCase));
        }

    }

}
/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data;
using System.Data.SqlClient;
using Logger;
using ORM.Query;
using ORM.Result;
using ORM.Session.Exceptions;
using ORM.Session.Handlers;
using ORM.Session.States;
using Utilities.Generics;

namespace ORM.Session
{
    public class Session : IDisposable
    {
        private readonly Logger.Logger _logger = LoggerFactory.Instance.GetLogger(typeof(Session));

        private SqlConnection _connection;


        public bool IsOpen { get; private set; }
        private DateTime _lastConnectionRefresh;

        public Session()
        {
            IsOpen = false;
        }

        #region CloseConnection
        
        public void Dispose()
        {
            CloseConnection();
        }

        private void CloseConnection()
        {
            IsOpen = false;
            _connection?.Close();
            _connection?.Dispose();
        }

        public void Close()
        {
            _logger.Debug("Closing connection...");
            CloseConnection();
        }
        
        #endregion

        #region OpenConnection
        
        public Session Open(int connectionTimeout = 30)
        {
            SessionState sessionState = SessionStateHandler.ToHandle(this);
            sessionState.Open(this, connectionTimeout);
            return this;
        }

        public void OpenDbConnectionWhenOpen()
        {
            //The connection it is already open... do nothing
        }

        public void OpenDbConnectionWhenNotOpen(int connectionTimeout)
        {
            DatabaseProperties dbProperties = SessionFactory.Instance.DatabaseProperties;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = dbProperties.Host;
            builder.UserID = dbProperties.User;
            builder.Password = dbProperties.Password;
            builder.InitialCatalog = dbProperties.Schema;
            builder.ConnectTimeout = connectionTimeout;

            _logger.Debug("Connecting to SQL Server ... ");
            _connection = new SqlConnection(builder.ConnectionString);
            _lastConnectionRefresh = DateTime.Now;
            _connection.Open();
            IsOpen = true;
        }

        #endregion

        #region ExecuteQuery

        private T ExecuteOnSafeSqlCommand<T>(string query, Func<SqlCommand, T> executeSqlCommand)
        {
            Open();
            using (SqlCommand command = new SqlCommand(query, _connection)) {
                return executeSqlCommand(command);
            }
        }


        public int ExecuteNativeNonQuery(string query, int commandTimeout = 30)
        {
            return ExecuteOnSafeSqlCommand(query, command => {
                command.CommandTimeout = commandTimeout;
                _lastConnectionRefresh = DateTime.Now;
                return command.ExecuteNonQuery();
            });            
        }

        public ResultSet ExecuteNativeQuery(string query, int commandTimeout = 30)
        {
            return ExecuteOnSafeSqlCommand(query, command => {
                ResultSet resultSet = new ResultSet();
                command.CommandTimeout = commandTimeout;
                var reader = command.ExecuteReader();
                _lastConnectionRefresh = DateTime.Now;

                while (reader.Read()) {
                    _lastConnectionRefresh = DateTime.Now;

                    DbRow row = new DbRow();
                    for (int i = 0; i <= reader.FieldCount-1; i++) {
                        DbColumn column = new DbColumn();
                        column.Type = reader.GetFieldType(i);
                        column.Value = reader.GetValue(i);
                        column.Name = reader.GetName(i);
                        row.Columns.Add(column);
                    }
                    resultSet.Rows.Add(row);
                }
                reader.Close();
                return resultSet;
            });            
        }

        #endregion

        #region Helpers

        #endregion
    }
}
/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;
using System.Timers;
using Log4CS.Core;
using Log4CS.Core.impl;
using ORM.Result;
using static ORM.Session.Handlers.SessionStateHandler;

namespace ORM.Session
{
    public sealed class SqlSession : ISession, IStateHandleable
    {
        private readonly ILogger _logger = new Logger(typeof(SqlSession));

        private SqlConnection _connection;
        private Timer _refreshTimer;
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        private bool _isOpen;
        private void DbConnectionRefresh(object source, ElapsedEventArgs e)
        {
            _logger.Debug("Refreshing db connection...");
        }

        public SqlSession(SqlConnectionStringBuilder connectionStringBuilder)
        {
            _isOpen = false;
            _connectionStringBuilder = connectionStringBuilder;
        }

        #region CloseConnection
        
        public void Dispose()
        {
            CloseConnection();
        }

        public void Close()
        {
            _logger.Debug("Closing connection...");
            CloseConnection();
        }

        bool ISession.IsOpen()
        {
            return _isOpen;
        }

        private void CloseConnection()
        {
            ToHandleSessionState(this).Close(this);
        }

        public void CloseDbConnectionWhenOpen()
        {
            _isOpen = false;
            _connection.Close();
            _connection.Dispose();
            _refreshTimer.Enabled = false;
            _refreshTimer.Dispose();
        }

        internal static void CloseDbConnectionWhenClose()
        {
            /* The connection it's already close... do nothing */
        }

        #endregion

        #region OpenConnection
        
        public ISession Open(int connectionTimeout = 30)
        {
            ToHandleSessionState(this).Open(this, connectionTimeout);
            return this;
        }

        public void OpenDbConnectionWhenOpen()
        {
            /* The connection it's already open... do nothing */
        }

        public void OpenDbConnectionWhenNotOpen(int connectionTimeout)
        {
            _logger.Debug("Connecting to SQL Server ... ");
            _connectionStringBuilder.ConnectTimeout  = connectionTimeout;
            _connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            _connection.Open();
            _isOpen = true;

            ScheduleDbConnectionRefresh();
        }

        private void ScheduleDbConnectionRefresh()
        {
            _logger.Debug("Scheduling a DB connection refresh");
            const int refreshRatio = 500;
            _refreshTimer = new Timer();
            _refreshTimer.Elapsed += DbConnectionRefresh;
            _refreshTimer.Interval = _connectionStringBuilder.ConnectTimeout * refreshRatio;
            _refreshTimer.Enabled = true;
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
                return command.ExecuteNonQuery();
            });            
        }

        public ResultSet ExecuteNativeQuery(string query, int commandTimeout = 30)
        {
            return ExecuteOnSafeSqlCommand(query, command => {
                ResultSet resultSet = new ResultSet();
                command.CommandTimeout = commandTimeout;
                var reader = command.ExecuteReader();

                while (reader.Read()) {
                    DbRow row = new DbRow();
                    for (int ii = 0; ii <= reader.FieldCount-1; ii++) {
                        DbColumn column = new DbColumn
                        {
                            Type = reader.GetFieldType(ii), 
                            Value = reader.GetValue(ii),
                            Name = reader.GetName(ii)
                        };
                        row.Columns.Add(column);
                    }
                    resultSet.Rows.Add(row);
                }
                reader.Close();
                return resultSet;
            });            
        }

        #endregion
    }
}
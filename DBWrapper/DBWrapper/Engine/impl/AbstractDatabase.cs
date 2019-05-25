/**
 * Created by Marcelo Cabezas on 2019-May-09 9:03:43 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data;
using System.Data.Common;
using System.Timers;
using Commons.Generics;
using Commons.Generics.impl;
using DBW.DBWrapper.Result;
using DBW.DBWrapper.States;
using DBW.DBWrapper.States.impl;
using Log4CS.Core;
using Log4CS.Core.impl;
using DbColumn = DBW.DBWrapper.Result.DbColumn;


namespace DBW.DBWrapper.Engine.impl
{
    public abstract class AbstractDatabase : IConnection, IDisposable, IStateHandleable, IDatabase
    {
        private readonly ILogger _logger = new Logger(typeof(AbstractDatabase));

        private bool _isOpen;
        protected DbConnection Connection;
        private readonly string _connectionString;
        private Timer _refreshTimer;

        protected AbstractDatabase(string connectionString)
        {
            _isOpen = false;
            _connectionString = connectionString;
        }

        private void ScheduleDbConnectionRefresh()
        {
            _logger.Debug("Scheduling a DB connection refresh");
            const int refreshRatio = 500;
            _refreshTimer = new Timer();
            _refreshTimer.Elapsed += DbConnectionRefresh;
            _refreshTimer.Interval = Connection.ConnectionTimeout * refreshRatio;
            _refreshTimer.Enabled = true;
        }
        
        public void CloseDbConnectionWhenOpen()
        {
            _isOpen = false;
            Connection.Close();
            Connection.Dispose();
            _refreshTimer.Enabled = false;
            _refreshTimer.Dispose();
        }
        
        private void DbConnectionRefresh(object source, ElapsedEventArgs e)
        {
            _logger.Debug("Refreshing db connection...");
        }

        public void Dispose()
        {
            CloseConnection();
        }

        private void CloseConnection()
        {
            ConnectionStateHandler.ToHandleConnectionState(this).Close(this);
        }
        
        public IConnection Open(int connectionTimeout = 30)
        {
            ConnectionStateHandler.ToHandleConnectionState(this).Open(this, connectionTimeout);
            return this;
        }

        private void CheckConnectionState(int connectionTimeout = 30)
        {
            ConnectionStateHandler.ToHandleConnectionState(this).Open(this, connectionTimeout);
        }

        public void Close()
        {
            _logger.Debug("Closing connection...");
            CloseConnection();
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public void OpenDbConnectionWhenNotOpen(int connectionTimeout)
        {
            _logger.Debug("Connecting to DB Server ... ");
            Connection = ConnectionFactory(_connectionString);
            Connection.Open();
            _isOpen = true;

            ScheduleDbConnectionRefresh();
        }

        private T ExecuteOnSafeSqlCommand<T>(string query, Func<DbCommand, T> executeSqlCommand)
        {
            CheckConnectionState();
            using (DbCommand command = CommandFactory(query)) {
                return executeSqlCommand(command);
            }
        }

        protected abstract DbCommand CommandFactory(string query);


        public void OpenDbConnectionWhenOpen()
        {
            /* The connection it's already open... do nothing */
        }
        
        public void CloseDbConnectionWhenClose()
        {
            /* The connection it's already close... do nothing */
        }

        public object ExecuteInsert(string query, Action<DbCommand, Func<string, DbType, DbParameter>> parametersConfiguration, int commandTimeout = 30)
        {
            
            return ExecuteOnSafeSqlCommand(PrepareInsertOutput(query),
                command => {
                parametersConfiguration(command, NewParameter);
                command.CommandTimeout = commandTimeout;
                return command.ExecuteScalar();
            });
        }

        public void ExecuteScalar(string query, int commandTimeout = 30)
        {
            ExecuteOnSafeSqlCommand(query,
                command => {
                    command.CommandTimeout = commandTimeout;
                    return command.ExecuteScalar();
                });
        }

        protected abstract string PrepareInsertOutput(string query);

        public IMCollection<DbRow> ExecuteNativeQuery(string query, Action<DbCommand, Func<string, DbType, DbParameter>> parametersConfiguration,int commandTimeout = 30)
        {
            return ExecuteOnSafeSqlCommand(query, command =>
            {
                parametersConfiguration(command, NewParameter);
                IMCollection<DbRow> dbRows = new MCollection<DbRow>();
                command.CommandTimeout = commandTimeout;
                var reader = command.ExecuteReader();

                while (reader.Read()) {
                    DbRow row = new DbRow();
                    for (int ii = 0; ii <= reader.FieldCount-1; ii++)
                    {
                        DbColumn column = new DbColumn(reader.GetFieldType(ii),
                            reader.GetName(ii),
                            reader.GetValue(ii));
                        row.Columns.Add(column);
                    }
                    dbRows.Add(row);
                }
                reader.Close();
                return dbRows;
            });
        }

        protected abstract DbParameter NewParameter(string aParameterName, DbType aDbType);

        protected abstract DbConnection ConnectionFactory(string aConnectionString);

    }
}
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
using Utilities.Generics;

namespace ORM.Session
{
    public class Session : IDisposable
    {
        private readonly Logger.Logger _logger = LoggerFactory.Instance.GetLogger(typeof(Session));

        #region OnClosedSession
        
        public delegate void ClosedSessionEventHandler(object source, EventArgs args);
        public event ClosedSessionEventHandler ClosedSession;
        protected virtual void OnClosedSession()
        {
            ClosedSession?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private SqlConnection _connection;


        public bool IsOpen { get; private set; }

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
            _logger.Debug("Closing connection");
            CloseConnection();
            OnClosedSession();
        }
        
        #endregion

        #region OpenConnection
        
        public void Open()
        {
            DatabaseProperties dbProperties = SessionFactory.Instance.DatabaseProperties;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = dbProperties.Host;
            builder.UserID = dbProperties.User;
            builder.Password = dbProperties.Password;
            builder.InitialCatalog = dbProperties.Schema;

            CloseConnection();
            _logger.Debug("Connecting to SQL Server ... ");
            _connection = new SqlConnection(builder.ConnectionString);
            _connection.Open();
            IsOpen = true;
        }
        
        #endregion

        #region ExecuteQuery

        public void ExecuteNativeNonQuery(string query)
        {
            CheckConnection();

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public ResultSet ExecuteNativeQuery(string query)
        {
            CheckConnection();

            ResultSet resultSet = new ResultSet();

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DbRow row = new DbRow();
                    for (int i = 0; i <= reader.FieldCount-1; i++) //The mathematical formula for reading the next fields must be <=
                    {
                        DbColumn column = new DbColumn();
                        column.Type = reader.GetFieldType(i);
                        column.Value = reader.GetValue(i);
                        column.Name = reader.GetName(i);
                        row.Columns.Add(column);
                    }
                    resultSet.Rows.Add(row);
                }
            }

            return resultSet;
        }

        #endregion

        #region Helpers

        private void CheckConnection()
        {
            if (!IsOpen) throw new NonOpenConnectionException();
        }

        #endregion
    }
}
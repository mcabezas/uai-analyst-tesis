/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;
using Logger;
using ORM.Query;

namespace ORM.Session
{
    public class Session : IDisposable
    {
        private readonly Logger.Logger _logger = LoggerFactory.Instance.GetLogger(typeof(Session));

        #region OnClossedSession
        
        public delegate void ClosedSessionEventHandler(object source, EventArgs args);
        public event ClosedSessionEventHandler ClosedSession;
        protected virtual void OnClosedSession()
        {
            ClosedSession?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private SqlConnection _connection;

        public bool IsOpen { get; private set; }

        public void Close()
        {
            if (!IsOpen){ return; }
            _logger.Debug("Closing connection");
            _connection.Close();
            _connection.Dispose();
            _connection = null;
            IsOpen = false;
            OnClosedSession();
        }

        public ResultSet ExecuteNativeQuery(string query)
        {
            ResultSet resultSet = new ResultSet();
            if (!IsOpen) { return resultSet; }
            //Todo Implement this method on the right way
            //new SqlCommand(query, _connection).ExecuteReader();
            return resultSet;
        }

        public void Open()
        {
            DatabaseProperties dbProperties = SessionFactory.Instance.DatabaseProperties;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = dbProperties.Host;
            builder.UserID = dbProperties.User;
            builder.Password = dbProperties.Password;
            builder.InitialCatalog = dbProperties.Schema;

            _logger.Debug("Connecting to SQL Server ... ");
            _connection?.Dispose();
            _connection = new SqlConnection(builder.ConnectionString);
            _connection.Open();
            IsOpen = true;
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
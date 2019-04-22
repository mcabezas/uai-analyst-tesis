/**
 * Created by Marcelo Cabezas on 2019-Apr-21 12:48:08 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace ORM
{
    public class DatabaseProperties
    {
        public string Host { get; }
        public string Port { get; }
        public string User { get; }
        public string Password { get; }
        public string Schema { get; }

        public DatabaseProperties(string host, string port, string user, string password, string schema)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            Schema = schema;
        }
    }
}
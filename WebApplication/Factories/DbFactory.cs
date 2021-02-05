using System;
using MySql.Data.MySqlClient;

namespace WebApplication.Factories
{
    public class DbFactory
    {
        private static MySqlConnection _currentConnection;
        private static PropertiesFactory _propertiesFactory = new();
        public static string ConnectionString { get; set; }
        
        public MySqlConnection GetConnection()
        {
            if (_currentConnection == null)
            {
                _propertiesFactory ??= new PropertiesFactory();
                ConnectionString = _propertiesFactory.BuildConnectionString();
                var newConnection = new MySqlConnection(ConnectionString);
                _currentConnection = newConnection;
                return _currentConnection;
            }

            return _currentConnection;
        }
        
        
        public MySqlConnection GetConnection(string type)
        {
            switch (type)
            {
                case "mysql":
                    try
                    {
                        if (_currentConnection == null)
                        {
                            _propertiesFactory ??= new PropertiesFactory();
                            var newConnection = new MySqlConnection(_propertiesFactory.BuildConnectionString());
                            _currentConnection = newConnection;
                            return _currentConnection;
                        }
                        return _currentConnection;
                    }
                    catch
                    {
                        throw new Exception("Can't create connection");
                    }
/*
                    break;
*/
            }
            return _currentConnection;
        }
    }
    
}


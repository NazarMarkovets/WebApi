using System;
using MySql.Data.MySqlClient;


namespace WebApplication
{
    public class DbFactory
    {
        private static MySqlConnection currentConnection; 
        public static string _connectionString { get; set; }
        
        public MySqlConnection GetConnection()
        {
            if (currentConnection == null)
            {
                _connectionString = @"server=localhost;port=3306;database=everlastingblog;userid=root;password=warKrawT228787898787899";
                var newConnection = new MySqlConnection(_connectionString);
                currentConnection = newConnection;
                return currentConnection;
            }

            return currentConnection;
        }
        
        public MySqlConnection GetConnection(string type, string connectionString)
        {
            switch (type)
            {
                case "mysql":
                    try
                    {
                        if (currentConnection == null)
                        {
                            var newConnection = new MySqlConnection(connectionString);
                            currentConnection = newConnection;
                            return currentConnection;
                        }
                        return currentConnection;
                    }
                    catch
                    {
                        throw new Exception("Can't create connection");
                    }
/*
                    break;
*/
            }
            return currentConnection;
        }
    }
    
}


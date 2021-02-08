using System;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using WebApplication.Factories;
using WebApplication.Props;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;

namespace WebApplicationTest
{
    
    [TestClass]
    public class WebApplicationTest
    {
        //private Properties _properties;
        //private CommentsRepository _commentRepository;
        private DbFactory _dbFactory = new();
        private PropertiesFactory _propertiesFactory;
        private bool hadRows = false;
        [TestInitialize]
        public void TestInitialization()
        {
            //_commentRepository = new CommentsRepository();
            //_properties = new Properties();
            _dbFactory = new DbFactory();
            _propertiesFactory = new PropertiesFactory();
            
        }

        [TestMethod("Check status from connection factory. IS it closed?")]
        public void Get_DB_Connection()
        {
            string expectedStatus = "Closed";
            var factConnection = _dbFactory.GetConnection();
            string actualStatus = factConnection.State.ToString();
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestMethod ("Does connection factory always return the same object")]
        public void Factory_returns_the_same_connection_obj()
        {
            var first = _dbFactory.GetConnection();
            var second = _dbFactory.GetConnection();
            Assert.AreEqual(first,second);
        }

        [TestMethod("Get json properties for object")]
        public void GetPropertiesForConnectionFromJson()
        {
            var json = _propertiesFactory.GetPropertiesForConnection();
            
            Assert.IsNotNull(json);
            
        }


        [TestMethod("JSON is created correctly")]
        public void Create_json_if_not_exists()
        {
            string jsonfileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                "connectionProperties.json");
            _propertiesFactory.CreatePropertiesIfNotExists();
            if (!File.Exists(jsonfileLocation))
            {
                throw new FileNotFoundException();
            }
            else
            {
                Properties expectedProperties = new Properties(
                    "server=localhost;",
                    "port=3306;",
                    "database=everlastingblog;",
                    "userid=root;", 
                    "password=warKrawT228787898787899;");
                Properties actualProperties;
                using (StreamReader file = File.OpenText(jsonfileLocation))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    actualProperties = (Properties)serializer.Deserialize(file, typeof(Properties));
                    
                }
                Assert.IsTrue(expectedProperties.Server == actualProperties.Server && 
                              expectedProperties.Port == actualProperties.Port &&
                              expectedProperties.Database == actualProperties.Database &&
                              expectedProperties.Userid == actualProperties.Userid &&
                              expectedProperties.Password == actualProperties.Password);
            }
            
        }
        [TestMethod("Connection string is built correct")]
        public void Build_Connection_String_Correct()
        {
            string _connectionString = @"server=localhost;port=3306;database=everlastingblog;userid=root;password=warKrawT228787898787899;";
            var get =_propertiesFactory.BuildConnectionString();
            Assert.IsNotNull(get);
            Assert.AreEqual(_connectionString, get);
        }

        [TestMethod("Can read data from db")]
        public void GetDataFromDb()
        {
            var connect = _dbFactory.GetConnection();
            connect.Open();
            const string query = "select id,content, author_name, author_email,article_id,created_at from everlastingcomments.comment";
            var command = new MySqlCommand(query, connect);
            var sqlDataReader = command.ExecuteReader();
            bool hasRows = sqlDataReader.HasRows;
            connect.Close();
            connect.Dispose();
            sqlDataReader.Close();
            if (!hasRows)
            {
                InsertRow();
                Assert.IsTrue(hadRows,"successfully read after inserted operations");
            }
            connect.Close();
            Assert.IsTrue(true,"successfully read");
        }

        [TestMethod("Insert Row to database")]
        public void InsertRow()
        {
            var connection = _dbFactory.GetConnection();
            connection.Open();
            
            var transaction = connection.BeginTransaction();
            MySqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "insert into everlastingcomments.comment " +
                                         "values(null, 'test', 'test','al.com', 1, null);";
            command.CommandText += "select id,content, author_name, author_email,article_id,created_at from everlastingcomments.comment ";
            var reader = command.ExecuteReader();
            hadRows = reader.HasRows;
            Assert.IsTrue(hadRows);
            reader.Close();
            transaction.Rollback();
            connection.Close();
            
        }
    }
}


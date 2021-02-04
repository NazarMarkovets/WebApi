using System;
using System.Data;
using System.Dynamic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication;
using WebApplication.Models;
using WebApplication.Props;
using WebApplication.Repository;

namespace WebApplicationTest
{
    
    [TestClass]
    public class WebApplicationTest
    {
        private CommentsRepository _commentsRepository;
        private DbFactory _dbFactory;
        private Properties _properties;
        private PropertiesFactory _propertiesFactory;
         
        [TestInitialize]
        public void TestInitialization()
        {
            _commentsRepository = new CommentsRepository();
            _dbFactory = new DbFactory();
            _properties = new Properties();
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
                Properties actualProperties = new Properties();
                //string expected = JsonConvert.SerializeObject(templateForJson);
                using (StreamReader file = File.OpenText(jsonfileLocation))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    actualProperties = (Properties)serializer.Deserialize(file, typeof(Properties));
                    
                }
                
                Assert.IsTrue(expectedProperties.server == actualProperties.server && 
                              expectedProperties.port == actualProperties.port &&
                              expectedProperties.database == actualProperties.database &&
                              expectedProperties.userid == actualProperties.userid &&
                              expectedProperties.password == actualProperties.password);
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
    }
}

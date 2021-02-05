using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebApplication.Factories;
using WebApplication.Props;
using WebApplication.Repository;

namespace WebApplicationTest
{
    
    [TestClass]
    public class WebApplicationTest
    {
        public CommentsRepository CommentRepository { get; private set; }
        private DbFactory _dbFactory;
        public Properties Properties { get; private set; }
        private PropertiesFactory _propertiesFactory;
         
        [TestInitialize]
        public void TestInitialization()
        {
            CommentRepository = new CommentsRepository();
            _dbFactory = new DbFactory();
            Properties = new Properties();
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
    }
}

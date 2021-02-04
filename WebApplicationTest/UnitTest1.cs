using System.Data;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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

        [TestMethod]
        public void Get_DB_Connection()
        {
            string expectedStatus = "Closed";
            var factConnection = _dbFactory.GetConnection();
            string actualStatus = factConnection.State.ToString();
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [TestMethod]
        public void Factory_returns_the_same_connection_obj()
        {
            var first = _dbFactory.GetConnection();
            var second = _dbFactory.GetConnection();
            Assert.AreEqual(first,second);
        }

        [TestMethod("Get json properties")]
        public void Inject_props_json_to_connectionString()
        {
            var json = _propertiesFactory.ReadProperties();
            
            Assert.IsNotNull(json);
            
        }

        [TestMethod]
        public void Build_Connection_String_Correct()
        {
            string _connectionString = @"server=localhost;port=3306;database=everlastingblog;userid=root;password=warKrawT228787898787899;";
            var get =_propertiesFactory.BuildConnectionString();
            Assert.IsNotNull(get);
            Assert.AreEqual(_connectionString, get);
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManageController : ControllerBase
    {
        
        private static DbFactory connectionFactory = new DbFactory();
        
        [HttpGet] //api/Manage
        public IEnumerable<string> Get()
        {
            var mysqlconnection = connectionFactory.GetConnection();
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.CommandText = "SELECT  FROM author";
            return new string[] {"value1", "value2"};
            
        }
        

        [HttpGet("{id}")]
        public string GetById(int id)
        {
            string result = null;
            var mySqlConnection = connectionFactory.GetConnection();
            MySqlCommand sqlCommand = mySqlConnection.CreateCommand();
            sqlCommand.CommandText = "select first_name from everlastingblog.author where id="+id;
            
            try
            {
                mySqlConnection.Open();
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader[0].ToString();
                    Console.WriteLine(result);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            mySqlConnection.Close();
            
            return result;
            
           }

        [HttpPost]
        public void Post([FromBody]string name)
        {
 
            Author author = new Author();
            author.id = 958;
            author.first_name = name;
            author.last_name = name+"Koval";
            author.email = name+"newemail";
            author.password = name+"newpassword";
            author.username = name+"newusername";

            //using ADO.NET technology - Status OK
            
            var mySqlConnection = connectionFactory.GetConnection();
            //var sqlExpression = "INSERT INTO  author VALUES (@id)";
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = mySqlConnection;
           
           sqlCommand.CommandText = $"insert into author values ('{0}','{1}')";
           sqlCommand.CommandText = "INSERT INTO author VALUES ('')";
           try
            {
                mySqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine(sqlCommand.ToString());
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 
        
        

        // [HttpPut]
        // public void Update(string tmp)
        // {
        //     
        // }
        //
        // [HttpDelete("{id}")]
        // public void DeleteSmth(int id)
        // {
        //     
        // }

    }
}
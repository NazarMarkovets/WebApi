using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication.Factories;
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
            connectionFactory.GetConnection();
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.CommandText = "SELECT  FROM author";
            return new[] {"value1", "value2"};
            
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
            author.Id = 958;
            author.FirstName = name;
            author.LastName = name+"Koval";
            author.Email = name+"newemail";
            author.Password = name+"newpassword";
            author.Username = name+"newusername";

            //using ADO.NET technology - Status OK
            
            var mySqlConnection = connectionFactory.GetConnection();
            //var sqlExpression = "INSERT INTO  author VALUES (@id)";
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = mySqlConnection;
           
           //sqlCommand.CommandText = "insert into author values ('{0}','{1}')";
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
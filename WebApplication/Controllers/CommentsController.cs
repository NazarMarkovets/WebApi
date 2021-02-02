using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        
        private static DbFactory connectionFactory = new DbFactory();
        private static List<Comment> commentsList = new List<Comment>();
        
        
        [HttpGet] //api/Comments
        public ActionResult<List<Comment>> GetAll()
        {
            commentsList.Clear();
            var mysqlconnection = connectionFactory.GetConnection();

            try
            {
                mysqlconnection.Open();
                string sqlQuery =
                    "select id,content, author_name, author_email,created_at from everlastingcomments.comment;";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, mysqlconnection);

                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        var item = new Comment
                        {
                            Id = sqlDataReader.GetInt32(0),
                            Content = sqlDataReader.GetString(1),
                            AuthorName = sqlDataReader.GetString(2),
                            AuthorEmail = sqlDataReader.GetString(3),
                            CreatedAt = sqlDataReader.GetDateTime(4)
                        };
                        commentsList.Add(item);
                    }

                }

            }
            catch
            {
                return BadRequest();
            }
            finally
            {
                    mysqlconnection.Close();
            }
            
            return commentsList;
        }
        
        [Microsoft.AspNetCore.Mvc.HttpGet("{article_id}")] //api/Comments/{id}
        public ActionResult<Comment> GetCommentById(int article_id)
        {
            var item = new Comment();
            //var searchResult = _comment.ReturnComment();
            var mysqlconnection = connectionFactory.GetConnection();
            
            //todo
            
            return item;
        }

        #region GoodGetIdRequest

        /*
        [Microsoft.AspNetCore.Mvc.HttpGet("{article_id}")] //api/Comments/{id}
        public ActionResult<List<Comment>> GetCommentById(int article_id)
        {   
            
            //Попробовать с объектом
            commentsList.Clear();
            var mysqlconnection = connectionFactory.GetConnection();

            try
            {
                mysqlconnection.Open();
                string sqlQuery =
                    "select id,content,author_name,author_email,created_at from everlastingcomments.comment where article_id="+article_id;

                MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, mysqlconnection);

                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        var item = new Comment
                        {
                            Id = sqlDataReader.GetInt32(0),
                            Content = sqlDataReader.GetString(1),
                            AuthorName = sqlDataReader.GetString(2),
                            AuthorEmail = sqlDataReader.GetString(3),
                            CreatedAt = sqlDataReader.GetDateTime(4)
                        };
                        commentsList.Add(item);
                    }
                    mysqlconnection.Close();
                }

            }
            catch (Exception)
            {
                throw new DataException();
            }
            return commentsList;
        }
         */

        #endregion

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Post([Microsoft.AspNetCore.Mvc.FromBody]string name)
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
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication.Factories;
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
        
        [HttpGet("{article_id}")] //api/Comments/{id}
        public ActionResult<Comment> GetCommentById(int articleId)
        {
            var item = new Comment();
            //var searchResult = _comment.ReturnComment();
            connectionFactory.GetConnection();
            
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
           sqlCommand.CommandText = "insert into everlastingcomments.comment values(null, 'breaking news', 'Jonny', 'jonny1@gmail.com', 1, null);";
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
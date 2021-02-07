using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Renci.SshNet.Messages.Authentication;
using WebApplication.Factories;
using WebApplication.Models;
using WebApplication.Repository;
using WebApplication.Validation;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        
        private static DbFactory connectionFactory = new DbFactory();
        
        private static CommentsRepository _commentsRepository = new CommentsRepository();
        
        [HttpGet] //api/Comments
        public ActionResult<List<Comment>> GetAll()
        {
            var commentsList = new List<Comment>();
            commentsList.Clear();
            var mysqlconnection = connectionFactory.GetConnection();

            try
            {
                mysqlconnection.Open();
                string sqlQuery =
                    "select id,content, author_name, author_email,article_id,created_at from everlastingcomments.comment;";
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
                            ArticleId = sqlDataReader.GetInt32(4),
                            CreatedAt = sqlDataReader.GetDateTime(5)
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
        public ActionResult<List<Comment>> GetCommentById(int article_id)
        {
            try
            {
                var commentsList = new List<Comment>();
                commentsList = _commentsRepository.FindCommentsByArticleId(article_id);

                return commentsList;
            }
            finally
            {
                BadRequest();
            }
            
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostWriteComment(Comment comment)
        {
            Checker requestvalidator = new Checker();
            var responseMessage = requestvalidator.IsValid(comment);
            if (responseMessage.IsSuccessStatusCode)
            {
                _commentsRepository.InsertComment(comment);
                return CreatedAtAction(nameof(GetCommentById), new { article_id = comment.ArticleId }, comment);
            }
            else
            {
                return BadRequest();
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
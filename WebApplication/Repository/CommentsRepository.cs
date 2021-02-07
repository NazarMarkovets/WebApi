using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using WebApplication.Factories;
using WebApplication.Models;


namespace WebApplication.Repository
{
    public class CommentsRepository
    {
        public void InsertComment(Comment comment)
        {
            var mySqlConnection = _factory.GetConnection();
            var commandText =
                $"insert into everlastingcomments.comment values(null, '{comment.Content}', '{comment.AuthorName}' , '{comment.AuthorEmail}' , 1, null);";
            MySqlCommand sqlCommand = new MySqlCommand(commandText, mySqlConnection);
            
            try
            {
                mySqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                
            }
            
            finally
            {
                mySqlConnection.Close();
            }
        }

        private DbFactory _factory = new DbFactory();

        public List<Comment> FindCommentsByArticleId(int articleId)
        {
            var commentsList = new List<Comment>();
            var connection = _factory.GetConnection();
            try
            {
                connection.Open();
                string sqlQuery =
                    $"select id,content,author_name,author_email,created_at from everlastingcomments.comment where article_id="+
                    articleId;

                MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, connection);

                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        var comment = new Comment
                        {
                            Id = sqlDataReader.GetInt32(0),
                            Content = sqlDataReader.GetString(1),
                            AuthorName = sqlDataReader.GetString(2),
                            AuthorEmail = sqlDataReader.GetString(3),
                            ArticleId = articleId,
                            CreatedAt = sqlDataReader.GetDateTime(4)
                        };
                        commentsList.Add(comment);

                    }
                    
                }

            }
            catch (DataException ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
            return commentsList;
        }
    }
}
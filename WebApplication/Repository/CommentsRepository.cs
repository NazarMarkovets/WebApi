using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class CommentsRepository
    {
        private DbFactory _factory = new DbFactory();
        public List<Comment> FindById(int article_id)
        {
            var commentsList = new List<Comment>();
            var connection = _factory.GetConnection();
            try
            {
                connection.Open();
                string sqlQuery =
                    "select id,content,author_name,author_email,created_at from everlastingcomments.comment where article_id=" +
                    article_id;

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
                            ArticleId = article_id,
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
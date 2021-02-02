using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    [Table("comment")]
    public class Comment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("author_name")]
        public string AuthorName { get; set; }
        [Column("author_email")]
        public string AuthorEmail { get; set; }
        [Column("article_id")]
        public int ArticleId { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

       
    }
    
    
    
    
}
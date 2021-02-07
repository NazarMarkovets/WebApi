using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Models
{
    [Table("comment")]
    public class Comment
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("content")]
        [Required(ErrorMessage ="Content gets error" )]
        [StringLength(500, MinimumLength=3, ErrorMessage = "Lenght is too short",ErrorMessageResourceType = typeof(BadRequestResult))]
        public string Content { get; set; }
        [Column("author_name")]
        [Required(ErrorMessage ="Content gets error" )]
        [StringLength(500, MinimumLength=3, ErrorMessage = "Lenght is too short",ErrorMessageResourceType = typeof(BadRequestResult))]
        public string AuthorName { get; set; }
        [Column("author_email")]
        [Required(ErrorMessage ="Email is empty", AllowEmptyStrings =false)]
        [EmailAddress(ErrorMessage = "The field email is not valid. Try to add '@' to email")]
        [StringLength(100, MinimumLength=3, ErrorMessage = "Lenght is too short",ErrorMessageResourceType = typeof(BadRequestResult))]
        public string AuthorEmail { get; set; }
        [Column("article_id")]
        [Range(1,Int32.MaxValue, ErrorMessage = "Invalid article_id. It must be more than 0")]
        public int ArticleId { get; set; }
        [Column("created_at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

       
    }
    
    
    
    
}
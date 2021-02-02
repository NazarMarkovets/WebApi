using System.ComponentModel.DataAnnotations.Schema;



namespace WebApplication.Models
{
    [Table("arcicle")]
    public class Article
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("slur")]
        public string Slur { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
    }
}
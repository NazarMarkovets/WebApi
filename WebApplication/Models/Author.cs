namespace WebApplication.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public Author(){}
        public Author(string fname)
        {
            FirstName = fname;
            Email = null;
            Password = null;
            Username = null;

        }
    }
    
    
}
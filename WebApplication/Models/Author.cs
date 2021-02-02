using System;


namespace WebApplication.Models
{
    public class Author
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }

        public Author(){}
        public Author(string fname, string lname)
        {
            first_name = fname;
            lname = last_name;
            email = null;
            password = null;
            username = null;

        }
    }
    
    
}
namespace WebApplication.Props
{
    public class Properties
    {
        //_connectionString = @"server=localhost;port=3306;database=everlastingblog;userid=root;password=warKrawT228787898787899;";
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Userid { get; set; }
        public string Password { get; set; }

        public Properties()
        {
            
        }
        public Properties(string server, string port, string database, string userid, string password)
        {
            this.Server = server;
            this.Port = port;
            this.Database = database;
            this.Userid = userid;
            this.Password = password;
        }

        
        
    }
    
    
}
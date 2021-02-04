using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication.Props
{
    public class Properties
    {
        //_connectionString = @"server=localhost;port=3306;database=everlastingblog;userid=root;password=warKrawT228787898787899;";
        public string server { get; set; }
        public string port { get; set; }
        public string database { get; set; }
        public string userid { get; set; }
        public string password { get; set; }

        public Properties()
        {
            
        }
        public Properties(string server, string port, string database, string userid, string password)
        {
            this.server = server;
            this.port = port;
            this.database = database;
            this.userid = userid;
            this.password = password;
        }

        
        
    }
    
    
}
using System;
using System.IO;
using Newtonsoft.Json;
using WebApplication.Props;

namespace WebApplication.Factories
{
    public class PropertiesFactory
    {
        private Properties Properties { get; set; }
         //path: C:\Users\nazar\AppData\Roaming\connectionProperties.json
        private string _jsonfileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "connectionProperties.json");

        
        //Get properties from json to fill properties object
        public Properties GetPropertiesForConnection()
        {
            if (Properties is null)
            {
                using (StreamReader r = new StreamReader(_jsonfileLocation))
                {
                    string json = r.ReadToEnd();
                    Properties = JsonConvert.DeserializeObject<Properties>(json);
                }
                return Properties;
            }
            return Properties;
        }
        
        //Create json not exists
        public void CreatePropertiesIfNotExists()
        // use path C:\Users\nazar\AppData\Roaming\connectionProperties.json
        {
            Properties templateForJson = new Properties(
                "server=localhost;",
                "port=3306;",
                "database=everlastingblog;",
                "userid=root;", 
                "password=warKrawT228787898787899;");
            string stringjson = JsonConvert.SerializeObject(templateForJson);
            if (File.Exists(_jsonfileLocation))
            {
                File.Delete(_jsonfileLocation);
                using (var streamWriter = new StreamWriter(_jsonfileLocation, true))
                {
                    streamWriter.WriteLine(stringjson);
                    streamWriter.Close();
                }
            }
            else if (!File.Exists(_jsonfileLocation))
            {
                using (var streamWriter = new StreamWriter(_jsonfileLocation, true))
                {
                    streamWriter.WriteLine(stringjson);
                    streamWriter.Close();
                }
                
            }
        }

        //Configurate the finish connection string for DbFactory
        public string BuildConnectionString()
        {
            if (Properties is null)
            {
                var properties = GetPropertiesForConnection();
                var builtConnectionString = properties.Server+properties.Port+properties.Database+properties.Userid+properties.Password;
                return builtConnectionString;
            }
            else
            {
                var builtConnectionString = Properties.Server + Properties.Port + Properties.Database + Properties.Userid + Properties.Password;
                return builtConnectionString;
            }
        }
    }
    
    
    
    
}
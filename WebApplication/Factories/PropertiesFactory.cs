using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;
using WebApplication.Props;

namespace WebApplication
{
    public class PropertiesFactory
    {
        private Properties _properties { get; set; }
         //path: C:\Users\nazar\AppData\Roaming\connectionProperties.json
        private string jsonfileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "connectionProperties.json");

        
        //Get properties from json to fill properties object
        public Properties GetPropertiesForConnection()
        {
            if (_properties is null)
            {
                using (StreamReader r = new StreamReader(jsonfileLocation))
                {
                    string json = r.ReadToEnd();
                    _properties = JsonConvert.DeserializeObject<Properties>(json);
                }
                return _properties;
            }
            return _properties;
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
            if (File.Exists(jsonfileLocation))
            {
                File.Delete(jsonfileLocation);
                using (var streamWriter = new StreamWriter(jsonfileLocation, true))
                {
                    streamWriter.WriteLine(stringjson.ToString());
                    streamWriter.Close();
                }
            }
            else if (!File.Exists(jsonfileLocation))
            {
                using (var streamWriter = new StreamWriter(jsonfileLocation, true))
                {
                    streamWriter.WriteLine(stringjson.ToString());
                    streamWriter.Close();
                }
                
            }
        }

        //Configurate the finish connection string for DbFactory
        public string BuildConnectionString()
        {
            if (_properties is null)
            {
                var properties = GetPropertiesForConnection();
                var builtConnectionString = properties.server+properties.port+properties.database+properties.userid+properties.password;
                return builtConnectionString;
            }
            else
            {
                var builtConnectionString = _properties.server + _properties.port + _properties.database + _properties.userid + _properties.password;
                return builtConnectionString;
            }
        }
    }
    
    
    
    
}
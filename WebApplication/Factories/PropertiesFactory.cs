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
        private string path = @"../";
        private static string json;
        

        public Properties ReadProperties()
        {
            if (_properties is null)
            {
                using (StreamReader r = new StreamReader("file.json"))
                {
                    string json = r.ReadToEnd();
                    _properties = JsonConvert.DeserializeObject<Properties>(json);
                }
                return _properties;
            }
            return _properties;
        }

        
        public string BuildConnectionString()
        {
            if (_properties is null)
            {
                var properties = ReadProperties();
                var builtConnectionString = properties.server+properties.port+properties.database+properties.user+properties.pass;
                return builtConnectionString;
            }
            else
            {
                var builtConnectionString = _properties.server + _properties.port + _properties.database + _properties.user + _properties.pass;
                return builtConnectionString;
            }
        }
    }
    
    
    
    
}
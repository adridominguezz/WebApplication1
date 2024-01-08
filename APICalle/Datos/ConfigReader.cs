using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICalle
{
    public class ConfigReader
    {
        public string test { get; set; }
        public string ruta { get; set; }

        public string pizzeria { get; set; }
        
        public ConfigReader()
        {
            test = ConfigurationManager.AppSettings["test"];
            ruta = ConfigurationManager.AppSettings["ruta"];
            pizzeria = ConfigurationManager.AppSettings["pizzeria"];
        }

        

        

    }
}

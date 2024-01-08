using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APICalle.pizzasXML
{
    [XmlRoot("pizzas")]
    public class ItemsReaderPizza
    {
        [XmlElement("pizza")]

        public List<Pizza> Pizzas { get; set; }

    }

    public class Pizza
    {
        [XmlAttribute("nombre")]

        public string nombre { get; set; }

        [XmlAttribute("precio")]

        public int precio { get; set; }

        [XmlElement("ingrediente")]

        public List<Ingrediente> ingredientes { get; set; }



    }

    public class Ingrediente
    {
        [XmlAttribute("nombre")]

        public string nombre { get; set; }
    }
}

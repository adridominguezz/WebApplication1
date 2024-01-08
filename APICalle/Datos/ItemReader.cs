using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace APICalle
{
    [XmlRoot("item")]
    public class ItemsReaded {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Itemid")]
        public string Itemid { get; set; }
    }
}
     


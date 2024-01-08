using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace APICalle.pizzasXML
{
    public class LecturaXMLPizza
    {
        public void LecturaXML_Nodes(string filepath)
        {
            string testpizza = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                testpizza += node.InnerText;

            }
            MessageBox.Show(testpizza);
        }


        public ItemsReaderPizza? LecturaXML_Deserialize(string filepath)
        {
            ItemsReaderPizza? i = null;
            var serializer = new XmlSerializer(typeof(ItemsReaderPizza));
            using (Stream reader = new FileStream(filepath, FileMode.Open))
            {
                i = serializer.Deserialize(reader) as dynamic;
            }
            return i;
        }
    }
}

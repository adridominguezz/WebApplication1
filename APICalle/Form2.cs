using APICalle.pizzasXML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APICalle
{
    public partial class Form2 : Form
    {
        ConfigReader reader { get; set; }
        LecturaXMLPizza lect { get; set; }

        public Form2()
        {
            InitializeComponent();


            reader = new ConfigReader();
            lect = new LecturaXMLPizza();
            ItemsReaderPizza items = lect.LecturaXML_Deserialize(reader.pizzeria);
            TreeNode nodo = new TreeNode("pizzas");
            treeView1.Nodes.Add(nodo);

            foreach (var pizza in items.Pizzas)
            {
                TreeNode pizzaNode = new TreeNode(pizza.nombre + " - Precio: " + pizza.precio.ToString());
                nodo.Nodes.Add(pizzaNode);

                foreach (var ingrediente in pizza.ingredientes)
                {
                    TreeNode ingredienteNode = new TreeNode(ingrediente.nombre);
                    nodo.Nodes.Add(ingredienteNode);
                }
            }
        }
    }
}

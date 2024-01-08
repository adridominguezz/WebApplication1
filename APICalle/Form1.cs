using APICalle.pizzasXML;

namespace APICalle
{
    public partial class Form1 : Form
    {
        ConfigReader Config { get; set; }

        LecturaXMLPizza lect { get; set; }


        public Form1()
        {
            InitializeComponent();
            Config = new ConfigReader();
            label1.Text = Config.test;

            Config = new ConfigReader();
            lect = new LecturaXMLPizza();
            ItemsReaderPizza items = lect.LecturaXML_Deserialize(Config.pizzeria);
            TreeNode nodo = new TreeNode("Pizzas");
            treeView1.Nodes.Add(nodo);

            int numNodo = 0;
            foreach (var pizza in items.Pizzas)
            {
                TreeNode pizzaNode = new TreeNode(pizza.nombre);
                TreeNode pizzaPrecio = new TreeNode("Precio: " + pizza.precio.ToString());
                
                nodo.Nodes.Add(pizzaNode);
                nodo.Nodes[numNodo].Nodes.Add(pizzaPrecio);


                foreach (var ingrediente in pizza.ingredientes)
                {
                    TreeNode ingredienteNode = new TreeNode(ingrediente.nombre);
                    nodo.Nodes[numNodo].Nodes.Add(ingredienteNode);
                }
                numNodo++;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new LecturaXML().LecturaXML_Nodes(Config.ruta);
            new LecturaXML().LecturaXML_Deserialize(Config.ruta);
        }

        
    }
}
using ClassLibrary1;
using QuestPDF.Infrastructure;

namespace TestProject1
   
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]       
        public void TestConectionUserGet()
        {
            SQLconn t = new SQLconn();
            t.GetUsers(new User() { Users = "Chemaria", pass = "1192" }); 
        }

        [TestMethod]
        public void TestConectionUserCreate()
        {
            SQLconn t = new SQLconn();
            t.CreateUsers(new User() { Users = "Chemaria", pass = "1192", email = "chemaria@email.com", Administrador = 1, idNegocio=1, Manage = 1, validated = 1 });
        }

        [TestMethod]
        public void TestConectionUserUpdate()
        {
            SQLconn t = new SQLconn();
            t.UpdateUsers(new User() { Users = "Pedro", pass = "8942" });
        }

        [TestMethod]
        public void TestConectionUserDelete()
        {
            SQLconn t = new SQLconn();
            t.DeleteUsers(new User() { Users = "Pedro" });

        }
        [TestMethod]
        public void TestPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            new PDFGenerate().generar("Bienvenido");
        }

        


    }
}
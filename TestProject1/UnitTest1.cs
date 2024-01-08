using ClassLibrary1;
namespace TestProject1
   
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]       
        public void TestConectionUserGet()
        {
            Class1 t = new Class1();
            t.conect(true);
            t.GetUsers(new User() { Users = "Chemaria", pass = "1192" }); 
        }

        [TestMethod]
        public void TestConectionUserCreate()
        {
            Class1 t = new Class1();
            t.conect(true);
            t.CreateUsers(new User() { Users = "Chemaria", pass = "1192", email = "chemaria@email.com", Administrador = 1, idNegocio=1, Manage = 1, validated = 1 });
        }

        [TestMethod]
        public void TestConectionUserUpdate()
        {
            Class1 t = new Class1();
            t.conect(true);
            t.UpdateUsers(new User() { Users = "Pedro", pass = "8942" });
        }

        [TestMethod]
        public void TestConectionUserDelete()
        {
            Class1 t = new Class1();
            t.conect(true);
            t.DeleteUsers(new User() { Users = "Pedro" });

        }
    }
}
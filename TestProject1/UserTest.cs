using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class UserTest
    {
        public void TestConectionUSer()
        {

            SQLconn t = new SQLconn();
            t.conect(true);
            t.GetUsers(new User() { Users = "Juan"});
            //Assert.AreEqual()
            //System.Diagnostics.Trace.WriteLine(t.conec.ConnectionString);
        }
    }
}

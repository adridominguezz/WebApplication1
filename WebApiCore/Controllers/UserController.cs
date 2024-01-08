using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;

namespace WebApiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private Class1 accessSql = new Class1();
        private ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUser")]
        public IList<User> Get(String name, String passw)
        {
            accessSql.conect(true);
            User user = new User() { Users = name, pass = passw};
            IList<User> uss = accessSql.GetUsers(user);
            return uss;
        }

        [HttpPost(Name = "PostUser")]
        public void Post([FromBody] UserIgnore usuario) 
        {
            accessSql.conect(true);
            User uss = new User { Users = usuario.Users, pass = usuario.pass, email = usuario.email };
            accessSql.CreateUsers(uss);
        }


        [HttpPut(Name = "PutUser")]
        public void Put( String name, String password, String email, int? Admin, int? Manag, int? idNego, int? valid )
        {
            accessSql.conect(true);
            User user = new User() 
            { 
                Users = name, 
                pass = password, 
                email = email, 
                Administrador = Admin, 
                Manage = Manag,
                idNegocio = idNego,
                validated = valid
            };
            accessSql.UpdateUsers(user);
        }

        [HttpDelete(Name = "DeleteUser")]
        public void Put(String name)
        {
            accessSql.conect(true);
            User user = new User()
            {
                Users = name
            };
            accessSql.DeleteUsers(user);
        }

        
        
    }
}

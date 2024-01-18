using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using ClassLibrary1.PostgreDataStruct;

namespace WebApiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private Class1 accessSql = new Class1();
        private PostgreeCon postgreeCon = new PostgreeCon();
        private ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUser")]
        public User Get(String name, String passw, String email)
        {
            accessSql.conect(true);
            postgreeCon.IniciarCon();

            User user = new User() { Users = name, pass = passw };
            UsuarioPostGre usuarioPostGre = new UsuarioPostGre();
            String consulta = "select * from usuario where email = @Email";


            //postgreeCon.ConsultaTest<UsuarioPostGre>(consulta, new { Email = email });
            //IList<UsuarioPostGre> lista = postgreeCon.ConsultaTest<UsuarioPostGre>(consulta, new { Email = email });
            


            User uss = accessSql.GetUsers(user)[0];

            uss.UsuarioPostGre = postgreeCon.ConsultaTest<UsuarioPostGre>($" select  * from usuario where email like '%{email}%'")[0];
            


            return uss;
        }


        [HttpPost(Name = "PostUser")]
        public void Post(String name, String password, String email, int? Admin, int? Manag, int? idNego, int? valid, String? imagen) 
        {
            accessSql.conect(true);
            postgreeCon.IniciarCon();

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

            string imageUrl = imagen; 

            // Convierte la imagen a Base64
            string base64String = ConvertImageToBase64(imageUrl);

            accessSql.CreateUsers(user);
            postgreeCon.InsertTest<UsuarioPostGre>("usuario", new UsuarioPostGre() { email = email, imagenusuario = new imagen() { cabezera = "Foto", imgbase = base64String  } });
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


        static string ConvertImageToBase64(string imageUrl)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    // Descarga la imagen desde la URL
                    byte[] imageBytes = webClient.DownloadData(imageUrl);

                    // Convierte los bytes a Base64
                    string base64String = Convert.ToBase64String(imageBytes);

                    return base64String;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir la imagen a Base64: {ex.Message}");
                return null;
            }
        }


    }
}

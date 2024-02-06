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
    public class LibroController : ControllerBase
    {
        private SQLconn accessSql = new SQLconn();
        private ILogger<LibroController> _logger;

        public LibroController(ILogger<LibroController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetLibro")]
        public Libro Get( int year)
        {
            accessSql.conect(true);

            Libro libro = new Libro() { year = year };


            Libro uss = accessSql.Getlibros(libro)[0];

            

            return uss;
        }


        [HttpPost(Name = "PostLibro")]
        public void Post(String titulo, String autor, int year) 
        {
            accessSql.conect(true);

            Libro libro = new Libro()
            {
                titulo = titulo,
                autor = autor,
                year = year,
            };



            accessSql.CreateLibro(libro);
           
        }


        [HttpPut(Name = "PutLibro")]
        public void Put(String titulo, String autor, int year)
        {
            accessSql.conect(true);
            Libro libro = new Libro()
            {
                titulo = titulo,
                autor = autor,
                year = year,
            };
            accessSql.UpdateLibro(libro);
        }

        [HttpDelete(Name = "DeleteLibro")]
        public void Put(String titulo)
        {
            accessSql.conect(true);
            Libro libro = new Libro()
            {
                titulo = titulo
            };
            accessSql.DeleteLibro(libro);
        }


       


    }
}

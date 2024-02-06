using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary1.PostgreDataStruct;

namespace ClassLibrary1
{
    
    public class SQLconn
    {
        public static SqlConnection? conec { get; set; }

      
        public void conect(bool auth)
        {
            conec = new SqlConnection();

            conec.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Data Source=ADRI-PORTATIL; Initial Catalog=webAPI";
            conec.Open();
        }

        

        public DataSet TestDB()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select * from TUser", conec);

            adapter.Fill(ds);
            return ds;
        }
        

        public DataSet queryGenericStored( string query, List<KeyValuePair<string, dynamic>> parameters = null) { 
            DataSet ds = new DataSet() ;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conec);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Clear();

            foreach (KeyValuePair<string, dynamic> param in parameters)
            {
                AddParameters(ref adapter, param);
            }
            adapter.Fill(ds);
            return ds;
            
        }

        public void AddParameters (ref SqlDataAdapter sel, KeyValuePair<string, dynamic> val)
        {
            if(val.Value != null)
            {
                sel.SelectCommand.Parameters.AddWithValue(val.Key, val.Value);
            }
        }

        public IList<User> GetUsers(User user_to_search)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@User", user_to_search.Users));
            userparam.Add(new KeyValuePair<string, dynamic>("@Pass", user_to_search.pass));
            userparam.Add(new KeyValuePair<string, dynamic>("@email", user_to_search.email));
            userparam.Add(new KeyValuePair<string, dynamic>("@Administrador", user_to_search.Administrador));
            userparam.Add(new KeyValuePair<string, dynamic>("@Manage", user_to_search.Manage));
            userparam.Add(new KeyValuePair<string, dynamic>("@idNegocio", user_to_search.idNegocio));
            userparam.Add(new KeyValuePair<string, dynamic>("@Validated", user_to_search.validated));
            DataSet ds = queryGenericStored("svp_users_get", userparam);
            IList<User> items = ds.Tables[0].AsEnumerable().Select(row=> 
            new User
            {
                Users = row.Field<string>("Users"),
                email = row.Field<string>("email"),
                pass = row.Field<string>("pass"),
                Administrador = row.Field<int?>("Administrador"),
                Manage = row.Field<int?>("Manage"),
                validated = row.Field<int?>("validated")
            }).ToList();
                        
            return items;
        }


        public void CreateUsers(User user_to_create)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@Users", user_to_create.Users));
            userparam.Add(new KeyValuePair<string, dynamic>("@pass", user_to_create.pass));
            userparam.Add(new KeyValuePair<string?, dynamic>("@email", user_to_create.email));
            userparam.Add(new KeyValuePair<string?, dynamic>("@Administrador", user_to_create.Administrador));
            userparam.Add(new KeyValuePair<string?, dynamic>("@Manage", user_to_create.Manage));
            userparam.Add(new KeyValuePair<string?, dynamic>("@idNegocio", user_to_create.idNegocio));
            userparam.Add(new KeyValuePair<string?, dynamic>("@Validated", user_to_create.validated));
            DataSet ds = queryGenericStored("svp_users_create", userparam);
            String asunto = "Bienvenido " + user_to_create.Users;
            string ruta = new PDFGenerate().generar(user_to_create.Users);
            send("a.dominguez@cesjuanpablosegundocadiz.es", "bsxp exoe pfry bnko", user_to_create.email, asunto, "Usuario creado con éxito", ruta);

        }
        public void UpdateUsers(User user_to_update)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@NewUsers", user_to_update.Users));
            userparam.Add(new KeyValuePair<string, dynamic>("@NewPass", user_to_update.pass));
            userparam.Add(new KeyValuePair<string, dynamic>("@NewEmail", user_to_update.email));
            userparam.Add(new KeyValuePair<string, dynamic>("@NewAdmin", user_to_update.Administrador));
            userparam.Add(new KeyValuePair<string, dynamic>("@NewManage", user_to_update.Manage));
            userparam.Add(new KeyValuePair<string, dynamic>("@NewIdNegocio", user_to_update.idNegocio));
            userparam.Add(new KeyValuePair<string, dynamic>("@NewValidated", user_to_update.validated));
            DataSet ds = queryGenericStored("svp_users_update", userparam);
            

        }
        public void DeleteUsers(User user_to_delete)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@User", user_to_delete.Users));
            
            DataSet ds = queryGenericStored("svp_users_delete", userparam);
            
        }

        public void send(string mymail, string contrasenia, string mail, string subject, string body, string filePath, string args = null)
        {
            MailAddress to = new MailAddress(mail);
            MailAddress from = new MailAddress(mymail);

            MailMessage email = new MailMessage(from, to);
            email.Subject = subject;
            email.Body = body;

            Attachment attachment = new Attachment(filePath); // Ruta del archivo PDF
            email.Attachments.Add(attachment);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(mymail, contrasenia);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Importante: Liberar recursos del archivo adjunto
                attachment.Dispose();
            }
        }



        public IList<Libro> Getlibros(Libro book_to_search)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            //userparam.Add(new KeyValuePair<string, dynamic>("@titulo", book_to_search.titulo));
            //userparam.Add(new KeyValuePair<string, dynamic>("@autor", book_to_search.autor));
            userparam.Add(new KeyValuePair<string, dynamic>("@year", book_to_search.year.ToString()));
            DataSet ds = queryGenericStored("svp_libro_get", userparam);
            
            IList<Libro> items = ds.Tables[0].AsEnumerable().Select(row =>
            new Libro
            {
                titulo = row.Field<string>("titulo"),
                autor = row.Field<string>("autor"),
                year = row.Field<int>("year")
            }).ToList();


            return items;
        }

        public void CreateLibro(Libro libro_to_create)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@titulo", libro_to_create.titulo));
            userparam.Add(new KeyValuePair<string, dynamic>("@autor", libro_to_create.autor));
            userparam.Add(new KeyValuePair<string?, dynamic>("@year", libro_to_create.year));
            DataSet ds = queryGenericStored("svp_libro_create", userparam);

           
        }

        public void UpdateLibro(Libro libro_to_update)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@Newtitulo", libro_to_update.titulo));
            userparam.Add(new KeyValuePair<string, dynamic>("@Newautor", libro_to_update.autor));
            userparam.Add(new KeyValuePair<string, dynamic>("@Newyear", libro_to_update.year));
            DataSet ds = queryGenericStored("svp_libro_update", userparam);


        }

        public void DeleteLibro(Libro libro_to_delete)
        {
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@titulo", libro_to_delete.titulo));

            DataSet ds = queryGenericStored("svp_libro_delete", userparam);

        }
    }
}
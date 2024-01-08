using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace ClassLibrary1
{
    
    public class Class1
    {
        public SqlConnection conec { get; set; }

      
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

        public void send(User mymail, String contrasenia, string mail, string subject, string body, string args = null)
        {
            MailAddress to = new MailAddress(mail);
            MailAddress from = new MailAddress(mymail.email);

            MailMessage email = new MailMessage(from, to);
            email.Subject = subject;
            email.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(mymail.email, mymail.pass/**/);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(email);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
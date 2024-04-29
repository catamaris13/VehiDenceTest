using Azure;
using Microsoft.Data.SqlClient;
using System.Data;

namespace VehiDenceAPI.Models
{
    public class Dal
    {
        public Response Registration(Users user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Users(Name,Email,Password,PhoneNo,username,Token,IsValid) Values('" + user.Name + "','" + user.Email + "','" + user.Password + "','" + user.PhoneNo + "','" + user.username + "','"+user.Token+"',0)", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            int goergel = 1;


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Registration successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Registration failed";
            }
            return response;
        }
        public Response UserValidation(Users user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Users set IsValid=1 where username='"+user.username+"' and Token='"+user.Token+"'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            int goergel = 1;


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Validation successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Validation failed";
            }
            return response;
        }

        public Response Login(Users user, SqlConnection connection)
        {
            Response response = new Response();

            SqlDataAdapter da = new SqlDataAdapter("select * from Users where Email= '" + user.Email + "'and Password='" + user.Password + "'", connection); ;


            DataTable dt = new DataTable();
            da.Fill(dt);
            
            if (dt.Rows.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Login Successful";
                Users us = new Users();
                us.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                us.Name = Convert.ToString(dt.Rows[0]["Name"]);
                us.Email = Convert.ToString(dt.Rows[0]["Email"]);
                us.username = Convert.ToString(dt.Rows[0]["username"]);
                response.User = us;
            }
            
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Login Failed";
                    response.User = null;
                }
            
            return response;
        }
        public Response AddMasina(Masina masina, SqlConnection connection)
        {
            Response response = new Response();
            try
            {
                
                            SqlCommand cmd = new SqlCommand("Insert into Masina(SerieSasiu,NrInmatriculare,Marca,Model,Username) Values('" + masina.SerieSasiu + "','" + masina.NrInmatriculare + "','" + masina.Marca + "','" + masina.Model + "','" + masina.Username + "')", connection);
                            connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            connection.Close();
                            if (i > 0)
                            {
                                response.StatusCode = 200;
                                response.StatusMessage = "Masina adaugata cu succes";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Nu s-a putut adauga masina";
                            }
            }
            catch (SqlException ex)
            {
                // Check if the exception is due to a foreign key constraint violation
                if (ex.Number == 547)
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Foreign key constraint violated: " + ex.Message;
                }
                else
                {
                    // Handle other SQL exceptions
                    response.StatusCode = 100;
                    response.StatusMessage = "An error occurred: " + ex.Message;
                }
            }

            return response;
        }
        public Response MasinaList(Masina masina, SqlConnection connetion)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from Masina where username='" + masina.Username + "'", connetion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Masina> list = new List<Masina>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Masina ma = new Masina();
                    ma.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    ma.SerieSasiu = Convert.ToString(dt.Rows[i]["SerieSasiu"]);
                    ma.NrInmatriculare = Convert.ToString(dt.Rows[i]["NrInmatriculare"]);
                    ma.Marca = Convert.ToString(dt.Rows[i]["Marca"]);
                    ma.Model = Convert.ToString(dt.Rows[i]["Model"]);
                    ma.Username = Convert.ToString(dt.Rows[i]["Username"]);
                    list.Add(ma);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Masini Gasite";
                    response.listMasina = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite msini";
                    response.listMasina = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite msini";
                response.listMasina = null;
            }
            return response;
        }
        public Response DeleteMasina(Masina masina, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from Masina where username='"+masina.Username+"' and NrInmatriculare='"+masina.NrInmatriculare+"'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Deleted successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Deletion failed";
            }
            return response;
        }
        public Response AddAsigurare(Asigurare asigurare, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Asigurare (SerieSasiu,NrInmatriculare,DataCreare,DataExpirare,Asigurator) Values ('"+asigurare.SerieSasiu+ "','"+asigurare.NrInmatriculare+ "',GETDATE(),'" + asigurare.DataExpirare+ "','"+asigurare.Asigurator+"')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Asigurare successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Asigurare failed";
            }
            return response;
        }
        public Response DeleteAsigurare(Asigurare asigurare, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from Asigurare where NrInmatriculare='" + asigurare.NrInmatriculare + "'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Deleted successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Deletion failed";
            }
            return response;
        }

        public Response AsigutareList(Asigurare asigurare, SqlConnection connetion)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from Asigurare where NrInmatriculare='" + asigurare.NrInmatriculare + "'", connetion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Asigurare> list = new List<Asigurare>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Asigurare asi = new Asigurare();
                    asi.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    asi.SerieSasiu = Convert.ToString(dt.Rows[i]["SerieSasiu"]);
                    asi.NrInmatriculare = Convert.ToString(dt.Rows[i]["NrInmatriculare"]);
                    asi.DataCreare = Convert.ToDateTime(dt.Rows[i]["DataCreare"]);
                    asi.DataExpirare = Convert.ToDateTime(dt.Rows[i]["DataExpirare"]);
                    asi.Asigurator = Convert.ToString(dt.Rows[i]["Asigurator"]);
                    list.Add(asi);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Asigurare Gasite";
                    response.listAsigurare = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite Asigurari";
                    response.listAsigurare = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite Asigurari";
                response.listAsigurare = null;
            }
            return response;
        }
    }
}

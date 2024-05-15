using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using VehiDenceAPI.Controllers;
using static Hangfire.Storage.JobStorageFeatures;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VehiDenceAPI.Models
{
    public class Dal
    {
        public Response Registration(Users user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Users(Name,Email,Password,PhoneNo,username,Token,IsValid) Values('" + user.Name + "','" + user.Email + "','" + user.Password + "','" + user.PhoneNo + "','" + user.username + "','" + user.Token + "',0)", connection);
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
        public Response ResetToken(Users user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Users set Token='" + user.Token + "' where Email='" + user.Email + "'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Token changed successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Token change failed";
            }
            return response;
        }
        public Response UserValidation(Users user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Update Users set IsValid=1 where username='" + user.username + "' and Token='" + user.Token + "'", connection);
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
        public Response UserValidationEmail(Users user, SqlConnection connection)
        {
            Response response = new Response();
            connection.Open();
            SqlCommand command = new SqlCommand(
                "SELECT * FROM Users WHERE Token = @Token",
                connection);

            command.Parameters.AddWithValue("@Token", user.Token);

            // Execută interogarea și obține rezultatele
            SqlDataReader reader = command.ExecuteReader();

            // Verifică dacă există rânduri în rezultate
            if (reader.HasRows)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Validation successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Validation failed";
            }

            // Închide conexiunea și eliberează resursele
            reader.Close();
            connection.Close();

            return response;
        }
        public Response UserUpdate(Users user, SqlConnection connection)
        {
            Response response = new Response();

            connection.Open();

            SqlCommand command = new SqlCommand(
                "UPDATE Users SET Name = @Name, Password = @Password, username = @Username, PhoneNo = @PhoneNo, IsValid = @IsValid WHERE Email=@Email",
                connection);

            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Username", user.username);
            command.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@isValid", true);

            int queryResult = command.ExecuteNonQuery();
            connection.Close();

            if (queryResult > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User update successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User update failed";
            }

            return response;

        }
        public Response ResetPassword(Users user, SqlConnection connection)
        {
            Response response = new Response();

            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Users SET  Password = @Password WHERE Token=@Token", connection);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Token", user.Token);
            int queryResult = command.ExecuteNonQuery();
            connection.Close();

            if (queryResult > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Resset password successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Resset password failed";
            }

            return response;

        }
        public Response Login(Users user, SqlConnection connection)
        {
            Response response = new Response();

            SqlDataAdapter da = new SqlDataAdapter("select * from Users where Email= '" + user.Email + "'and Password='" + user.Password + "'and IsValid = 1", connection); ;


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
                response.StatusMessage = "Login Failed. Please Sign up or check your Email";
                response.User = null;
            }

            return response;
        }
        public Response DeleteUser(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("Delete from Users where email='" + users.Email + "'", connection);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Deletion successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Deletion failed";
            }
            return response;
        }
        public List<Users> GetUsers(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> users = new List<Users>();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Users", connection);
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                Users user = new Users();
                user.Name = read["Name"].ToString();
                user.Password = read["Password"].ToString();
                user.Email = read["Email"].ToString();
                user.username = read["username"].ToString();
                user.PhoneNo = read["PhoneNo"].ToString();
                users.Add(user);
            }
            connection.Close();

            return users;
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
            SqlCommand cmd = new SqlCommand("Delete from Masina where username='" + masina.Username + "' and NrInmatriculare='" + masina.NrInmatriculare + "'", connection);
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
            SqlCommand cmd = new SqlCommand("Insert into Asigurare (SerieSasiu,NrInmatriculare,DataCreare,DataExpirare,Asigurator) Values ('" + asigurare.SerieSasiu + "','" + asigurare.NrInmatriculare + "',GETDATE(),'" + asigurare.DataExpirare + "','" + asigurare.Asigurator + "')", connection);
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
        public Response VerificareExpirareAsigurare(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT distinct Users.Email, Users.Name  " +
    "FROM Users  " +
    "JOIN Masina  ON Users.username = Masina.Username " +
    "JOIN Asigurare ON Masina.NrInmatriculare = Asigurare.NrInmatriculare " +
    "WHERE DATEDIFF(day, GETDATE(), Asigurare.DataExpirare) <= 7;", connection);

            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Users> list = new List<Users>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Users us = new Users();

                    us.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    us.Name = Convert.ToString(dt.Rows[i]["Name"]);


                    list.Add(us);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Asigurari Expirate Gasite";
                    response.listUsers = list;


                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite Asigurari Expirate";
                    response.listAsigurare = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite Asigurari Expirate";
                response.listAsigurare = null;
            }
            return response;
        }

        public Response AddCasco(Casco casco, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Casco (SerieSasiu,NrInmatriculare,DataCreare,DataExpirare,Asigurator) Values ('" + casco.SerieSasiu + "','" + casco.NrInmatriculare + "',GETDATE(),'" + casco.DataExpirare + "','" + casco.Asigurator + "')", connection);
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
        public Response DeleteCasco(Casco casco, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from Casco where NrInmatriculare='" + casco.NrInmatriculare + "'", connection);
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
        public Response CascoList(Casco casco, SqlConnection connetion)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from Casco where NrInmatriculare='" + casco.NrInmatriculare + "'", connetion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Casco> list = new List<Casco>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Casco cas = new Casco();
                    cas.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    cas.SerieSasiu = Convert.ToString(dt.Rows[i]["SerieSasiu"]);
                    cas.NrInmatriculare = Convert.ToString(dt.Rows[i]["NrInmatriculare"]);
                    cas.DataCreare = Convert.ToDateTime(dt.Rows[i]["DataCreare"]);
                    cas.DataExpirare = Convert.ToDateTime(dt.Rows[i]["DataExpirare"]);
                    cas.Asigurator = Convert.ToString(dt.Rows[i]["Asigurator"]);
                    list.Add(cas);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Asigurare Gasite";
                    response.listCasco = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite Asigurari";
                    response.listCasco = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite Asigurari";
                response.listCasco = null;
            }
            return response;
        }

        public Response AddItp(ITP itp, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into ITP (NrInmatriculare,DataCreare,DataExpirare) Values ('" + itp.NrInmatriculare + "',GETDATE(),'" + itp.DataExpirare + "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "ITP successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "ITP failed";
            }
            return response;
        }
        public Response DeleteITP(ITP itp, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from ITP where NrInmatriculare='" + itp.NrInmatriculare + "'", connection);
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
        public Response ITPList(ITP itp, SqlConnection connetion)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from ITP where NrInmatriculare='" + itp.NrInmatriculare + "'", connetion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<ITP> list = new List<ITP>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ITP it = new ITP();
                    it.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    it.NrInmatriculare = Convert.ToString(dt.Rows[i]["NrInmatriculare"]);
                    it.DataCreare = Convert.ToDateTime(dt.Rows[i]["DataCreare"]);
                    it.DataExpirare = Convert.ToDateTime(dt.Rows[i]["DataExpirare"]);
                    list.Add(it);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "ITP Gasite";
                    response.listITP = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite ITP";
                    response.listITP = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite ITP";
                response.listITP = null;
            }
            return response;
        }

        public Response AddPermisConducere(PermisConducere permisConducere, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into PermisConducere (Nume,username,DataCreare,DataExpirare,Categorie) Values ('" + permisConducere.Nume + "','" + permisConducere.username + "',GETDATE(),'" + permisConducere.DataExpirare + "','" + permisConducere.Categorie + "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Carnet successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Carnet failed";
            }
            return response;
        }
        public Response DeletePermisConducere(PermisConducere permisConducere, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from PermisConducere where username='" + permisConducere.username + "'", connection);
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
        public Response PermisConducereList(PermisConducere permisConducere, SqlConnection connetion)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from PermisConducere where NrInmatriculare='" + permisConducere.username + "'", connetion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<PermisConducere> list = new List<PermisConducere>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PermisConducere pc = new PermisConducere();
                    pc.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    pc.Nume = Convert.ToString(dt.Rows[i]["Nume"]);
                    pc.username = Convert.ToString(dt.Rows[i]["username"]);
                    pc.DataCreare = Convert.ToDateTime(dt.Rows[i]["DataCreare"]);
                    pc.DataExpirare = Convert.ToDateTime(dt.Rows[i]["DataExpirare"]);
                    pc.Categorie = Convert.ToString(dt.Rows[i]["Categorie"]);
                    list.Add(pc);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Permis Gasite";
                    response.listPermisConducere = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite Permise";
                    response.listPermisConducere = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite Permise";
                response.listPermisConducere = null;
            }
            return response;
        }
        
        public Response AddRevizieService(RevizieService revizieService, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into RevizieService (SerieSasiu,KmUltim,KmExpirare,ServiceName) Values ('" + revizieService.SerieSasiu + "','"+revizieService.KmUltim+"','" + revizieService.KmExpirare + "','"+revizieService.ServiceName+"')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Revizie successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Revizie failed";
            }
            return response;
        }
        public Response DeleteRevizieService(RevizieService revizieService, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from RevizieService where SerieSasiu='" + revizieService.SerieSasiu + "'", connection);
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
        public Response RevizieServiceList(RevizieService revizieService, SqlConnection connetion)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from RevizieService where SerieSasiu='" + revizieService.SerieSasiu + "'", connetion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<RevizieService> list = new List<RevizieService>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RevizieService rs = new RevizieService();
                    rs.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    rs.SerieSasiu = Convert.ToString(dt.Rows[i]["SerieSasiu"]);
                    rs.KmUltim = Convert.ToInt32(dt.Rows[i]["KmUltim"]);
                    rs.KmExpirare = Convert.ToInt32(dt.Rows[i]["KmExpirare"]);
                    rs.ServiceName = Convert.ToString(dt.Rows[i]["ServiceName"]);
                    list.Add(rs);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "ITP Gasite";
                    response.listRevizieService = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite ITP";
                    response.listRevizieService = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite ITP";
                response.listRevizieService = null;
            }
            return response;
        }

        public Response AddVigneta(Vigneta vigneta, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Vigneta (NrInmatriculare,DataCreare,DataExpirare,Tara) Values ('" + vigneta.NrInmatriculare + "',GETDATE(),'" + vigneta.DataExpirare + "','"+vigneta.Tara+"')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Vigneta successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Vigneta failed";
            }
            return response;
        }
        public Response DeleteVigneta(Vigneta vigneta, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Delete from Vigneta where NrInmatriculare='" + vigneta.NrInmatriculare + "'", connection);
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
        public Response VignetaList(Vigneta vigneta, SqlConnection connetion)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from Vigneta where NrInmatriculare='" + vigneta.NrInmatriculare + "'", connetion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Vigneta> list = new List<Vigneta>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Vigneta vi = new Vigneta();
                    vi.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    vi.NrInmatriculare = Convert.ToString(dt.Rows[i]["NrInmatriculare"]);
                    vi.DataCreare = Convert.ToDateTime(dt.Rows[i]["DataCreare"]);
                    vi.DataExpirare = Convert.ToDateTime(dt.Rows[i]["DataExpirare"]);
                    vi.Tara = Convert.ToString(dt.Rows[i]["DataExpirare"]);


                    list.Add(vi);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Vignete Gasite";
                    response.listVigneta = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Nu au fost gasite Vignete";
                    response.listVigneta = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Nu au fost gasite Vignete";
                response.listVigneta = null;
            }
            return response;
        }



    }
}

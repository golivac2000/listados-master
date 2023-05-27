using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Listados.Pages.grupos
{
    public class EditarModel : PageModel
    {

        public GrupoInfo grupoinfo = new GrupoInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["ID"];

            try
            {
                String connectionString = "Data Source = PCGUAYOKUN\\MSSQLSERVER01; Initial Catalog = Listado; Integrated Security = True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "SELECT * FROM grupo WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                grupoinfo.id = "" + reader.GetInt32(0);
                                grupoinfo.nombres = reader.GetString(1);
                                grupoinfo.emails = reader.GetString(2);
                                grupoinfo.telefonos = reader.GetString(3);
                                grupoinfo.direcciones = reader.GetString(4);

                            }
                        }
                    }
                }

            } catch (Exception ex) {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            grupoinfo.id = Request.Form["id"];
            grupoinfo.nombres = Request.Form["Nombrea"];
            grupoinfo.emails = Request.Form["emaila"];
            grupoinfo.telefonos = Request.Form["telefonoa"];
            grupoinfo.direcciones = Request.Form["direcciona"];

            if (grupoinfo.id.Length == 0 || grupoinfo.nombres.Length == 0 || grupoinfo.emails.Length == 0 || grupoinfo.telefonos.Length == 0 || grupoinfo.direcciones.Length == 0)
            {
                errorMessage = "Todas las filas son necesarias";
                return;
            }

            try
            {
                String connectionString = "Data Source = PCGUAYOKUN\\MSSQLSERVER01; Initial Catalog = Listado; Integrated Security = True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "UPDATE grupo " +
                        "SET nombre=@nombre, email=@email, telefono=@telefono, direccion=@direccion "+
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@nombre", grupoinfo.nombres);
                        command.Parameters.AddWithValue("@email", grupoinfo.emails);
                        command.Parameters.AddWithValue("@telefono", grupoinfo.telefonos);
                        command.Parameters.AddWithValue("@direccion", grupoinfo.direcciones);
                        command.Parameters.AddWithValue("@id", grupoinfo.id);


                        command.ExecuteNonQuery();


                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }
            Response.Redirect("/grupos/Index");
        }

    }

}

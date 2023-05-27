using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Listados.Pages.grupos
{
    public class CrearModel : PageModel
    {
        public GrupoInfo grupoinfo = new GrupoInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost() {
            grupoinfo.nombres = Request.Form["Nombrea"];
            grupoinfo.emails = Request.Form["emaila"];
            grupoinfo.telefonos = Request.Form["telefonoa"];
            grupoinfo.direcciones = Request.Form["direcciona"];

            if (grupoinfo.nombres.Length == 0 || grupoinfo.emails.Length == 0 || grupoinfo.telefonos.Length == 0 || grupoinfo.direcciones.Length == 0)
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
                    String sql = "INSERT INTO grupo " + 
                        "(nombre, email, telefono, direccion) VALUES" + 
                        "(@nombre, @email, @telefono, @direccion);";

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@nombre", grupoinfo.nombres);
                        command.Parameters.AddWithValue("@email", grupoinfo.emails);
                        command.Parameters.AddWithValue("@telefono", grupoinfo.telefonos);
                        command.Parameters.AddWithValue("@direccion", grupoinfo.direcciones);

                        command.ExecuteNonQuery();



                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            grupoinfo.nombres = ""; grupoinfo.emails = ""; grupoinfo.telefonos = ""; grupoinfo.direcciones = "";
            successMessage = "Nuevo alumno agregado";

            Response.Redirect("/grupos/Index");
        }
    }
}

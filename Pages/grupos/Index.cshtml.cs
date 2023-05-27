using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Listados.Pages.grupos
{
    public class IndexModel : PageModel
    {
        public List<GrupoInfo> listgrupo = new List<GrupoInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=PCGUAYOKUN\\MSSQLSERVER01;Initial Catalog=Listado;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM grupo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GrupoInfo grupoinfo = new GrupoInfo();
                                grupoinfo.id = "" + reader.GetInt32(0);
                                grupoinfo.nombres = reader.GetString(1);
                                grupoinfo.emails = reader.GetString(2);
                                grupoinfo.telefonos = reader.GetString(3);
                                grupoinfo.direcciones = reader.GetString(4);
                                grupoinfo.creacion = reader.GetDateTime(5).ToString();

                                listgrupo.Add(grupoinfo);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception: " + ex.ToString());
            }
        }
    }

    public class GrupoInfo
    {
        public String id;
        public String nombres;
        public String emails;
        public String telefonos;
        public String direcciones;
        public String creacion;

    }

}

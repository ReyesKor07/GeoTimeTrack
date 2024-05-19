using System.Data.SqlClient;

namespace GeoTimeTrack.Data
{
    public class ConexionSQLServer
    {
        // Cadena de conexión al servidor SQL Server
        public static string connectionString = "Server= P3NWPLSK12SQL-v08.shr.prod.phx3.secureserver.net; DataBase=projecttes; User ID= prject; Password=proyec2023_;TrustServerCertificate=True;";
        // Objeto de conexión a la base de datos
        public static SqlConnection cn = new SqlConnection(connectionString);
        // Método para abrir la conexión
        public static void Abrir()
        {
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }
        }
        // Método para cerrar la conexión
        public static void Cerrar()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
    }
}
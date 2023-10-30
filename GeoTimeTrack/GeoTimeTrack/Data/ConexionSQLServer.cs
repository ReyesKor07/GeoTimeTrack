using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GeoTimeTrack.Data
{
    public class ConexionSQLServer
    {
        /*IP Casa*/
        // SqlConnection cn = new SqlConnection(@"Data source = 192.168.0.11; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
        /*IP Secundaria*/
        // SqlConnection cn = new SqlConnection(@"Data source = 192.168.1.129; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
        /*IP UAT*/
        // SqlConnection cn = new SqlConnection(@"Data source = 172.23.149.111; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
        /*IP AZURE*/
        public static string connectionString = "Server= P3NWPLSK12SQL-v08.shr.prod.phx3.secureserver.net; DataBase=projecttes; User ID= prject; Password=proyec2023_;TrustServerCertificate=True;";

        public static SqlConnection cn = new SqlConnection(connectionString);

        public static void Abrir()
        {
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }
        }

        public static void Cerrar()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using GeoTimeTrack.Data;
using System.Data.SqlClient;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class Registro
    {
        public DateTime FechaEntrada { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public decimal DistanciaEntrada { get; set; }
        public decimal DistanciaSalida { get; set; }
        public string TiempoTotal { get; set; }

        public static DateTime EntradaFecha { get; set; }
        public static string EntradaHora { get; set; }
        public static string SalidaHora { get; set; }
    }

    public partial class TrackTimePage : ContentPage
    {
        int UserId;

        public TrackTimePage()
        {
            InitializeComponent();
            // UserId = LoginPage.UserID;
            UserId = 16;
            List<Registro> userRecords = ObtenerRegistrosDeUsuario(UserId); // Obtener los registros de tiempo del usuario en función de su ID
            Registro.ItemsSource = userRecords; // Configurar el ListView
        }

        private List<Registro> ObtenerRegistrosDeUsuario(int UserId)
        {
            List<Registro> registros = new List<Registro>();

            /*IP Casa*/
            SqlConnection cn = new SqlConnection(@"Data source = 192.168.0.11; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
            /*IP Secundaria*/
            // SqlConnection cn = new SqlConnection(@"Data source = 192.168.1.129; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
            /*IP UAT*/
            // SqlConnection cn = new SqlConnection(@"Data source = 172.23.145.36; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");

            {
                try
                {
                    cn.Open();
                    string query = "SELECT FechaEntrada, HoraEntrada, HoraSalida, DistanciaEntrada, DistanciaSalida, TiempoTotal FROM Registro WHERE IdUsuario = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@userId", UserId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Registro registro = new Registro
                                {
                                    FechaEntrada = reader.GetDateTime(reader.GetOrdinal("FechaEntrada")),
                                    HoraEntrada = reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada")).ToString(),
                                    HoraSalida = reader.GetTimeSpan(reader.GetOrdinal("HoraSalida")).ToString(),
                                    DistanciaEntrada = reader.GetDecimal(reader.GetOrdinal("DistanciaEntrada")),
                                    DistanciaSalida = reader.GetDecimal(reader.GetOrdinal("DistanciaSalida")),
                                    TiempoTotal = reader.GetTimeSpan(reader.GetOrdinal("TiempoTotal")).ToString(),
                                };
                                registros.Add(registro);
                            }
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }
            return registros;
        }
    }
}
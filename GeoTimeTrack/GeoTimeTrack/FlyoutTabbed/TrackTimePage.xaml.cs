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
        public int Contador { get; set; } // Nuevo campo para el contador
        public DateTime FechaEntrada { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public decimal DistanciaEntrada { get; set; }
        public decimal DistanciaSalida { get; set; }
        public string TiempoTotal { get; set; }
        public string FechaHoraEntradaSalida { get; set; }
    }

    public partial class TrackTimePage : ContentPage
    {
        int UserId;

        public TrackTimePage()
        {
            InitializeComponent();
            UserId = LoginPage.UserID;
            List<Registro> userRecords = ObtenerRegistrosDeUsuario(UserId);
            Registro.ItemsSource = userRecords;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            // Llama nuevamente al método para obtener registros actualizados
            List<Registro> userRecords = ObtenerRegistrosDeUsuario(UserId);

            // Actualiza la propiedad ItemsSource con la nueva lista de registros
            Registro.ItemsSource = userRecords;
        }

        private List<Registro> ObtenerRegistrosDeUsuario(int UserId)
        {
            List<Registro> registros = new List<Registro>(); // 
         // SqlConnection cn = new SqlConnection(@"Data source = 192.168.39.152; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
            SqlConnection cn = new SqlConnection(@"Server= P3NWPLSK12SQL-v08.shr.prod.phx3.secureserver.net; DataBase=projecttes; User ID= prject; Password=proyec2023_;TrustServerCertificate=True;");
            {
                try
                {
                    cn.Open();
                    string query = "SELECT FechaEntrada, HoraEntrada, HoraSalida, DistanciaEntrada, DistanciaSalida, TiempoTotal FROM Registro_B WHERE IdUsuario = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@userId", UserId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int contador = 1; // Inicializa el contador en 1
                            while (reader.Read())
                            {
                                Registro registro = new Registro
                                {
                                    Contador = contador, // Asigna el contador
                                    FechaEntrada = reader.GetDateTime(reader.GetOrdinal("FechaEntrada")),
                                    HoraEntrada = reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada")).ToString(),
                                    HoraSalida = reader.GetTimeSpan(reader.GetOrdinal("HoraSalida")).ToString(),
                                    DistanciaEntrada = reader.GetDecimal(reader.GetOrdinal("DistanciaEntrada")),
                                    DistanciaSalida = reader.GetDecimal(reader.GetOrdinal("DistanciaSalida")),
                                    TiempoTotal = reader.GetTimeSpan(reader.GetOrdinal("TiempoTotal")).ToString(),
                                    FechaHoraEntradaSalida = $"{reader.GetDateTime(reader.GetOrdinal("FechaEntrada")).ToString("ddd, dd MMM", new System.Globalization.CultureInfo("es-ES"))} {reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada")).ToString(@"hh\:mm")} - {reader.GetTimeSpan(reader.GetOrdinal("HoraSalida")).ToString(@"hh\:mm")}",
                                };
                                registros.Add(registro);
                                contador++; // Incrementa el contador para el siguiente registro
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
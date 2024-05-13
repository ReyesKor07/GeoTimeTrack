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
        public DateTime FechaBD{ get; set; }
        public string Fecha { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public decimal DistanciaEntrada { get; set; }
        public decimal DistanciaSalida { get; set; }
        public string EstanciaTotal { get; set; }
    }

    public partial class TrackTimePage : ContentPage
    {
        int UserId;

        public TrackTimePage()
        {
            InitializeComponent();
            InitializeUserData();
            List<Registro> userRecords = ObtenerRegistrosDeUsuario(UserId);
            Registro.ItemsSource = userRecords;
        }

        private async void InitializeUserData()
        {
            UserId = Convert.ToInt32(await SecureStorage.GetAsync("UsuarioID"));
        }

        private void RefreshButtonClicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia
                DisplayAlert("Error", "Necesitas estar conectado a Internet para refrescar la página.", "OK");
                return;
            }
            // Llama nuevamente al método para obtener registros actualizados
            List<Registro> userRecords = ObtenerRegistrosDeUsuario(UserId);
            // Actualiza la propiedad ItemsSource con la nueva lista de registros
            Registro.ItemsSource = userRecords;
        }

        private List<Registro> ObtenerRegistrosDeUsuario(int UserId)
        {
            List<Registro> registros = new List<Registro>();
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
                                    FechaBD = reader.GetDateTime(reader.GetOrdinal("FechaEntrada")),
                                    Fecha = $"{reader.GetDateTime(reader.GetOrdinal("FechaEntrada")).ToString("dddd", new System.Globalization.CultureInfo("es-ES"))}\n{reader.GetDateTime(reader.GetOrdinal("FechaEntrada")).ToString("dd/MM/yyyy")}",
                                    HoraEntrada = ConvertTo12HourFormat(reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada"))),
                                    HoraSalida = ConvertTo12HourFormat(reader.GetTimeSpan(reader.GetOrdinal("HoraSalida"))),
                                    EstanciaTotal = reader.GetTimeSpan(reader.GetOrdinal("TiempoTotal")).ToString(@"hh\:mm") + " h",
                                };
                                // Convertir la primera letra del día de la semana a mayúscula
                                registro.Fecha = char.ToUpper(registro.Fecha[0]) + registro.Fecha.Substring(1);
                                // Convertir la primera letra del mes a mayúscula
                                registro.Fecha = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(registro.Fecha);
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

        private string ConvertTo12HourFormat(TimeSpan time)
        {
            string formattedTime = time.ToString(@"hh\:mm");
            DateTime dateTime = DateTime.Parse(formattedTime);
            return dateTime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
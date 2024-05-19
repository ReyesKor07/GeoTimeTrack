using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Data.SqlClient;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class Registro
    {
        public int Contador { get; set; }
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
        public static int UserID { get; private set; }

        public TrackTimePage()
        {
            InitializeComponent();
            InitializeUserData();
            List<Registro> userRecords = ObtenerRegistrosDeUsuario(UserID);
            Registro.ItemsSource = userRecords;
        }

        // Inicializa el ID de usuario al cargar la página
        private void InitializeUserData()
        {
            UserID = LoginPage.UserID;
        }

        private void RefreshButtonClicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                DisplayAlert("Advertencia", "Necesitas estar conectado a Internet para refrescar la página.", "OK");
                return;
            }
            // Llama nuevamente al método para obtener registros actualizados
            List<Registro> userRecords = ObtenerRegistrosDeUsuario(UserID);
            // Actualiza la propiedad ItemsSource con la nueva lista de registros
            Registro.ItemsSource = userRecords;
        }

        private List<Registro> ObtenerRegistrosDeUsuario(int UserID)
        {
            // Lista para almacenar los registros del usuario
            List<Registro> registros = new List<Registro>();
            SqlConnection cn = new SqlConnection(@"Server= P3NWPLSK12SQL-v08.shr.prod.phx3.secureserver.net; DataBase=projecttes; User ID= prject; Password=proyec2023_;TrustServerCertificate=True;");
            {
                try
                {
                    cn.Open();
                    // Consulta SQL para obtener los registros del usuario
                    string query = "SELECT FechaEntrada, HoraEntrada, HoraSalida, DistanciaEntrada, DistanciaSalida, TiempoTotal FROM Registro_B WHERE IdUsuario = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        // Agregar el parámetro de usuario a la consulta SQL
                        cmd.Parameters.AddWithValue("@userId", UserID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int contador = 1;
                            // Recorrer cada registro en el conjunto de resultados
                            while (reader.Read())
                            {
                                Registro registro = new Registro
                                {
                                    Contador = contador,
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
                                // Agregar el registro a la lista de registros
                                registros.Add(registro);
                                // Incrementar el contador para el siguiente registro
                                contador++;
                            }
                        }
                    }
                    // Cerrar la conexión a la base de datos
                    cn.Close();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }
            // Devolver la lista de registros obtenidos
            return registros;
        }

        private string ConvertTo12HourFormat(TimeSpan time)
        {
            // Convierte el formato de 24 horas a 12 horas
            string formattedTime = time.ToString(@"hh\:mm");
            DateTime dateTime = DateTime.Parse(formattedTime);
            return dateTime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
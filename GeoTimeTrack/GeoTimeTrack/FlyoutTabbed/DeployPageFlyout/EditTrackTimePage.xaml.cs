using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack.FlyoutTabbed.DeployPageFlyout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditTrackTimePage : ContentPage
	{
        private Usuario SelectedUser2;
        int UserId;
        public EditTrackTimePage(Usuario user)
        {
            InitializeComponent();
            SelectedUser2 = user;
            UserId = user.IdUsuario;
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

        private List<Registro> ObtenerRegistrosDeUsuario(int userId)
        {
            List<Registro> registros = new List<Registro>();
            SqlConnection cn = new SqlConnection(@"Server= P3NWPLSK12SQL-v08.shr.prod.phx3.secureserver.net; DataBase=projecttes; User ID= prject; Password=proyec2023_;TrustServerCertificate=True;");
            try
            {
                cn.Open();
                string query = "SELECT FechaEntrada, HoraEntrada, HoraSalida, DistanciaEntrada, DistanciaSalida, TiempoTotal FROM Registro_B WHERE IdUsuario = @userId";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int contador = 1;
                        while (reader.Read())
                        {
                            Registro registro = new Registro
                            {
                                Contador = contador, // Asigna el contador
                                FechaBD = reader.GetDateTime(reader.GetOrdinal("FechaEntrada")),
                                Fecha = $"{reader.GetDateTime(reader.GetOrdinal("FechaEntrada")).ToString("dddd dd\nMMMM\nyyyy", new System.Globalization.CultureInfo("es-ES"))}",
                                HoraEntrada = reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada")).ToString(@"hh\:mm"),
                                HoraSalida = reader.GetTimeSpan(reader.GetOrdinal("HoraSalida")).ToString(@"hh\:mm"),
                                EstanciaTotal = reader.GetTimeSpan(reader.GetOrdinal("TiempoTotal")).ToString(@"hh\:mm"),
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
            return registros;
        }

    }
}
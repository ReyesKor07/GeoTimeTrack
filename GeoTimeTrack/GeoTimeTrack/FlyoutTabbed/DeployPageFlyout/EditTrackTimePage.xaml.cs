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
                                Contador = contador,
                                FechaEntrada = reader.GetDateTime(reader.GetOrdinal("FechaEntrada")),
                                HoraEntrada = reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada")).ToString(),
                                HoraSalida = reader.GetTimeSpan(reader.GetOrdinal("HoraSalida")).ToString(),
                                DistanciaEntrada = reader.GetDecimal(reader.GetOrdinal("DistanciaEntrada")),
                                DistanciaSalida = reader.GetDecimal(reader.GetOrdinal("DistanciaSalida")),
                                TiempoTotal = reader.GetTimeSpan(reader.GetOrdinal("TiempoTotal")).ToString(),
                                FechaHoraEntradaSalida = $"{reader.GetDateTime(reader.GetOrdinal("FechaEntrada")).ToString("ddd, dd MMM", new System.Globalization.CultureInfo("es-ES"))} {reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada")).ToString(@"hh\:mm")} - {reader.GetTimeSpan(reader.GetOrdinal("HoraSalida")).ToString(@"hh\:mm")}",
                            };
                            registros.Add(registro);
                            contador++;
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
using GeoTimeTrack.FlyoutTabbed;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public static string Name { get; private set; }
        public static string LastName { get; private set; }
        public static int UserID { get; private set; }

        public LoginPage()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            emailEntry.Text = null;
            passwordEntry.Text = null;
        }

        public async void navigation()
        {
            await Navigation.PushModalAsync(new DeploymentPage());
        }

        private void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                /*IP Casa*/
                SqlConnection cn = new SqlConnection(@"Data source = 192.168.0.11; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
                /*IP Secundaria*/
                // SqlConnection cn = new SqlConnection(@"Data source = 192.168.1.129; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");
                /*IP UAT*/
                // SqlConnection cn = new SqlConnection(@"Data source = 172.23.145.36; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");

                {
                    cn.Open();
                    // Consulta SQL para verificar si el correo existe en la base de datos
                    string emailCheckQuery = "SELECT COUNT(*) FROM Usuario WHERE Email = @email";
                    using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, cn))
                    {
                        emailCheckCmd.Parameters.AddWithValue("@email", emailEntry.Text);
                        int emailCount = (int)emailCheckCmd.ExecuteScalar();

                        if (emailCount == 0)
                        {
                            // El correo no existe
                            DisplayAlert("Error", "El correo proporcionado no existe.", "OK");
                            return;
                        }
                    }
                    // Consulta SQL para obtener el usuario por correo y contraseña
                    string query = "SELECT IdUsuario, Nombre, ApellidoP FROM Usuario WHERE Email = @email AND Password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@email", emailEntry.Text);
                        cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                string apellidoP = reader.GetString(reader.GetOrdinal("ApellidoP"));
                                int userId = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                                Name = nombre;
                                LastName = apellidoP;
                                UserID = userId;
                                DisplayAlert("Bienvenido", $"Hola {nombre} {apellidoP}, ID: {userId}", "OK");
                                Clear(); navigation();
                            }
                            else
                            {
                                // Contraseña incorrecta
                                DisplayAlert("Error", "La contraseña es incorrecta.", "OK");
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnForgotPasswordLabelTapped(object sender, EventArgs e)
        {
            // Navega a la página ForgotPasswordPage
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            passwordEntry.IsPassword = !e.Value; // Cambia el valor de IsPassword según el estado del Switch
        }
    }
}
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
            // Navegar a la página principal
            await Navigation.PushModalAsync(new DeploymentPage());
        }

        private void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Validar si los campos de correo y contraseña no están vacíos
                if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                // Conexión a la base de datos
                using (SqlConnection cn = new SqlConnection(@"Data source = 192.168.0.9; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023"))
                {
                    cn.Open();

                    // Consulta SQL para obtener la contraseña asociada al correo proporcionado
                    string query = "SELECT Password FROM Usuario WHERE Email = @email";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@email", emailEntry.Text);
                        var passwordFromDb = (string)cmd.ExecuteScalar();

                        if (passwordFromDb == null)
                        {
                            // El correo no existe
                            DisplayAlert("Error", "El correo proporcionado no existe.", "OK");
                        }
                        else
                        {
                            if (passwordFromDb == passwordEntry.Text)
                            {
                                // Contraseña correcta, navegar a la página principal
                                Clear();
                                navigation();
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
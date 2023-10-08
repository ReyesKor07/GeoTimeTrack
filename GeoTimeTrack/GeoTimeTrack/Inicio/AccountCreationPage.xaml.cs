using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreationPage : ContentPage
    {
        public AccountCreationPage()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            usernameEntry.Text = null;
            userlastnameEntry.Text = null;
            usermiddlenameEntry.Text = null;
            emailEntry.Text = null;
            passwordEntry.Text = null;
        }

        private void OnCreateUserButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Validar si los campos de nombre, apellidos, correo y contraseña no están vacíos
                if (string.IsNullOrWhiteSpace(usernameEntry.Text) || string.IsNullOrWhiteSpace(userlastnameEntry.Text) || string.IsNullOrWhiteSpace(usermiddlenameEntry.Text) || string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                SqlConnection cn = new SqlConnection(@"Data source = 192.168.0.9; Initial Catalog = BD_GeoTimeTrack; Integrated Security=False; User Id= BD_GeoTimeTrack; Password=Xamarin2023");

                if (cn.State == System.Data.ConnectionState.Closed)
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, ApellidoP, ApellidoM, Email, Password)VALUES(@name, @apellidoP, @apellidoM, @email, @password)", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", usernameEntry.Text);
                    cmd.Parameters.AddWithValue("@apellidoP", userlastnameEntry.Text);
                    cmd.Parameters.AddWithValue("@apellidoM", usermiddlenameEntry.Text);
                    cmd.Parameters.AddWithValue("@email", emailEntry.Text);
                    cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                    cmd.ExecuteNonQuery();
                    DisplayAlert("Info", "Usuario creado con exito", "Okay");
                    Clear();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Okay");
            }
        }

        private async void OnLoginLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            // await Navigation.PushModalAsync(new LoginPage());
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Cambia el valor de IsPassword según el estado del Switch
            passwordEntry.IsPassword = !e.Value;
        }
    }
}
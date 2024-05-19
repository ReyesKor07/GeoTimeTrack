using System;
using System.Linq;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoTimeTrack.Data;
using Xamarin.Essentials;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreationPage : ContentPage
    {
        public AccountCreationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Verifica la conexión a Internet al aparecer la página
            CheckInternetConnection();
        }

        private bool ValidateName(string name)
        {
            // Verifica si el nombre contiene solo letras y espacios en blanco
            return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        private async void CheckInternetConnection()
        {
            // Verifica si hay conexión a Internet
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para usar la aplicación correctamente.", "OK");
            }
        }

        private void OnCreateUserButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Validar si los campos de nombre, apellidos, correo y contraseña no están vacíos
                if (string.IsNullOrWhiteSpace(usernameEntry.Text) ||
                    string.IsNullOrWhiteSpace(userlastnameEntry.Text) ||
                    string.IsNullOrWhiteSpace(usermiddlenameEntry.Text) ||
                    string.IsNullOrWhiteSpace(emailEntry.Text) ||
                    string.IsNullOrWhiteSpace(passwordEntry.Text) ||
                    string.IsNullOrWhiteSpace(confirmPasswordEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                // Validar si los campos de nombre y apellidos contienen solo letras y espacios en blanco
                if (!ValidateName(usernameEntry.Text) ||
                    !ValidateName(userlastnameEntry.Text) ||
                    !ValidateName(usermiddlenameEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, ingrese solo letras en los campos de nombre y apellidos.", "OK");
                    return;
                }

                // Verificar si la contraseña y la confirmación de contraseña coinciden
                if (passwordEntry.Text != confirmPasswordEntry.Text)
                {
                    DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                    return;
                }

                // Verificar la conectividad de red antes de intentar iniciar sesión
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    DisplayAlert("Error", "Necesitas estar conectado a Internet para crear tu cuenta.", "OK");
                    return;
                }

                // Conectar a la base de datos
                ConexionSQLServer.Abrir();
                // Consulta SQL para verificar si el correo electrónico ya existe
                string emailCheckQuery = "SELECT COUNT(*) FROM Usuario_B WHERE Email = @email";
                using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, ConexionSQLServer.cn))
                {
                    emailCheckCmd.Parameters.AddWithValue("@email", emailEntry.Text);
                    int emailCount = (int)emailCheckCmd.ExecuteScalar();

                    // El correo ya existe
                    if (emailCount > 0)
                    {
                        DisplayAlert("Error", "El correo proporcionado ya está registrado.", "OK");
                    }
                    // El correo no existe, insertar el nuevo usuario
                    else
                    {
                        // Comando SQL para insertar un nuevo usuario
                        SqlCommand cmd = new SqlCommand("INSERT INTO Usuario_B(Nombre, ApellidoP, ApellidoM, Email, Password, Rol) VALUES (@name, @apellidoP, @apellidoM, @email, @password, @rol)", ConexionSQLServer.cn);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", usernameEntry.Text);
                        cmd.Parameters.AddWithValue("@apellidoP", userlastnameEntry.Text);
                        cmd.Parameters.AddWithValue("@apellidoM", usermiddlenameEntry.Text);
                        cmd.Parameters.AddWithValue("@email", emailEntry.Text);
                        cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                        cmd.Parameters.AddWithValue("@rol", "Usuario");
                        cmd.ExecuteNonQuery();
                        DisplayAlert("Info", "Usuario creado con éxito", "OK");
                        Clear();
                    }
                }
                // Cerrar la conexión a la base de datos
                ConexionSQLServer.Cerrar();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message + "AccountCreatePage", "OK");
            }
        }

        private async void OnLoginLabelTapped(object sender, EventArgs e)
        {
            // Navegar de regreso a la página de inicio de sesión
            await Navigation.PopModalAsync();
        }

        public void Clear()
        {
            // Limpiar los campos de entrada de texto
            usernameEntry.Text = null;
            userlastnameEntry.Text = null;
            usermiddlenameEntry.Text = null;
            emailEntry.Text = null;
            passwordEntry.Text = null;
            confirmPasswordEntry.Text = null;
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Cambiar la visibilidad del texto de contraseña basado en el estado del Switch
            passwordEntry.IsPassword = !e.Value;
            confirmPasswordEntry.IsPassword = !e.Value;
        }
    }
}
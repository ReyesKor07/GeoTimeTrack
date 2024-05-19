using GeoTimeTrack.FlyoutTabbed;
using System;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoTimeTrack.Data;
using Xamarin.Essentials;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public static int UserID { get; private set; }
        public static string Name { get; private set; }
        public static string LastName { get; private set; }
        public static string MiddleName { get; private set; }
        public static string Email { get; private set; }
        public static string Password { get; private set; }
        public static string Rol { get; private set; }

        public LoginPage()
        {
            InitializeComponent();
        }

        public async void navigation()
        {
            await Navigation.PushModalAsync(new DeploymentPage());
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Mostrar mensaje de error si los campos están vacíos
                if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }

                // Verificar la conectividad de red antes de intentar iniciar sesión
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    DisplayAlert("Error", "Necesitas estar conectado a Internet para iniciar sesión.", "OK");
                    return;
                }

                // Abrir conexión a la base de datos, verificar si el correo electrónico existe
                ConexionSQLServer.Abrir();
                string emailCheckQuery = "SELECT COUNT(*) FROM Usuario_B WHERE Email = @email";
                using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, ConexionSQLServer.cn))
                {
                    emailCheckCmd.Parameters.AddWithValue("@email", emailEntry.Text);
                    int emailCount = (int)emailCheckCmd.ExecuteScalar();
                    emailCheckCmd.Dispose();
                    if (emailCount == 0)
                    {
                        DisplayAlert("Error", "El correo proporcionado no existe.", "OK");
                        return;
                    }
                }

                // Consulta SQL para obtener el usuario por correo y contraseña
                string query = "SELECT IdUsuario, Nombre, ApellidoP, ApellidoM, Email, Password, Rol FROM Usuario_B WHERE Email = @email AND Password = @password";
                using (SqlCommand cmd = new SqlCommand(query, ConexionSQLServer.cn))
                {
                    cmd.Parameters.AddWithValue("@email", emailEntry.Text);
                    cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtener la información del usuario de la base de datos
                            int userId = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                            string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                            string apellidoP = reader.GetString(reader.GetOrdinal("ApellidoP"));
                            string apellidoM = reader.GetString(reader.GetOrdinal("ApellidoM"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            string password = reader.GetString(reader.GetOrdinal("Password"));
                            string rol = reader.GetString(reader.GetOrdinal("Rol"));

                            // Asignar la información del usuario a las propiedades estáticas
                            UserID = userId;
                            Name = nombre;
                            LastName = apellidoP;
                            MiddleName = apellidoM;
                            Email = email;
                            Password = password;
                            Rol = rol;
                            Clear(); // Limpiar los campos de entrada
                            navigation(); // Navegar a la página de despliegue
                        }
                        else
                        {
                            // Mostrar mensaje de error si la contraseña es incorrecta
                            DisplayAlert("Error", "La contraseña es incorrecta.", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en caso de excepción
                DisplayAlert("Error", ex.Message + " LoginPage1\n", "OK");
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                ConexionSQLServer.Cerrar();
            }
        }

        private async void OnForgotPasswordLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }

        public void Clear()
        {
            emailEntry.Text = null;
            passwordEntry.Text = null;
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Cambiar la visibilidad del texto de contraseña basado en el estado del Switch
            passwordEntry.IsPassword = !e.Value;
        }
    }
}
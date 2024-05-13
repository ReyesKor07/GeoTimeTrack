using GeoTimeTrack.FlyoutTabbed;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using GeoTimeTrack.Data;
using System.Security.Cryptography;
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
            OnAppearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckInternetConnection();
        }

        private async void CheckInternetConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia y deshabilitar el inicio de sesión
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para usar la aplicación correctamente.", "OK");
                loginButton.IsEnabled = false;
            }
            else
            {
                // Hay conexión a Internet, habilitar el inicio de sesión y continuar con la lógica de la aplicación
                loginButton.IsEnabled = true;

                // Por ejemplo, intentar autenticar al usuario automáticamente si hay credenciales guardadas
                string userId = await SecureStorage.GetAsync("UserId");
                string password = await SecureStorage.GetAsync("Password");

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    await AutenticarUsuario(userId, password);
                }
            }
        }

        private async Task AutenticarUsuario(string userId, string password)
        {
            try
            {
                // Realizar la autenticación utilizando las credenciales guardadas
                ConexionSQLServer.Abrir();
                string query = "SELECT * FROM Usuario_B WHERE IdUsuario = @UserId AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, ConexionSQLServer.cn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Autenticación exitosa, navegar a la página principal
                            await Navigation.PushModalAsync(new DeploymentPage());
                        }
                        else
                        {
                            // Autenticación fallida, mostrar mensaje de error o navegar a la página de inicio de sesión
                            await DisplayAlert("Error", "No se pudieron recuperar las credenciales almacenadas.", "Aceptar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la autenticación
                // await DisplayAlert("Error", "Ocurrió un error al autenticar al usuario: " + ex.Message, "Aceptar");
            }
            finally
            {
                ConexionSQLServer.Cerrar();
            }
        }

        public async void navigation()
        {
            await Navigation.PushModalAsync(new DeploymentPage());
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK"); return;
                }

                // Verificar la conectividad de red antes de intentar iniciar sesión
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    // No hay conexión a Internet, mostrar mensaje de advertencia
                    DisplayAlert("Error", "Necesitas estar conectado a Internet para iniciar sesión.", "OK");
                    return;
                }

                ConexionSQLServer.Abrir();
                string emailCheckQuery = "SELECT COUNT(*) FROM Usuario_B WHERE Email = @email";
                using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, ConexionSQLServer.cn))
                {
                    emailCheckCmd.Parameters.AddWithValue("@email", emailEntry.Text);
                    int emailCount = (int)emailCheckCmd.ExecuteScalar();

                    emailCheckCmd.Dispose(); // Cierra y libera recursos del DataReader

                    if (emailCount == 0)
                    {
                        DisplayAlert("Error", "El correo proporcionado no existe.", "OK"); return;
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
                            int userId = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                            string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                            string apellidoP = reader.GetString(reader.GetOrdinal("ApellidoP"));
                            string apellidoM = reader.GetString(reader.GetOrdinal("ApellidoM"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            string password = reader.GetString(reader.GetOrdinal("Password"));
                            string rol = reader.GetString(reader.GetOrdinal("Rol"));
                            UserID = userId; Name = nombre; LastName = apellidoP; MiddleName = apellidoM; Email = email; Password = password; Rol = rol;
                            DisplayAlert("Inicio de sesión exitoso", $"¡Bienvenido, {nombre} {apellidoP}!\nTu ID de usuario es: {userId}", "Continuar");
                            try
                            {
                                // Guardar las credenciales en el almacenamiento seguro
                                SecureStorage.SetAsync("UsuarioID", UserID.ToString());
                                SecureStorage.SetAsync("Nombre", Name);
                                SecureStorage.SetAsync("ApellidoP", LastName);
                                SecureStorage.SetAsync("ApellidoM", MiddleName);
                                SecureStorage.SetAsync("Email", Email);
                                SecureStorage.SetAsync("Password", Password);
                                SecureStorage.SetAsync("Rol", Rol);
                            }
                            catch (Exception ex)
                            {
                                // Manejar cualquier excepción que pueda ocurrir al guardar en el almacenamiento seguro
                                Console.WriteLine($"Error al guardar en el almacenamiento seguro: {ex.Message}");
                            }

                            Clear();
                            navigation();
                        }
                        else
                        {
                            DisplayAlert("Error", "La contraseña es incorrecta.", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message + "LoginPage", "OK");
            }
            finally
            {
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
            passwordEntry.IsPassword = !e.Value;
        }
    }
}
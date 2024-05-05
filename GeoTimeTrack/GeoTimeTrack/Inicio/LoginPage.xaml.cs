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

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK"); return;
                }

                // Cifrar la contraseña ingresada por el usuario
                string hashedPassword = HashPassword(passwordEntry.Text);

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
                            // string storedPassword = reader.GetString(reader.GetOrdinal("Password"));
                            // Comparar el hash cifrado de la contraseña ingresada con el hash almacenado en la base de datos
                            // if (hashedPassword == storedPassword)
                            // {
                                // int userId = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                                // string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                // string apellidoP = reader.GetString(reader.GetOrdinal("ApellidoP"));
                                // string apellidoM = reader.GetString(reader.GetOrdinal("ApellidoM"));
                                // string email = reader.GetString(reader.GetOrdinal("Email"));
                                // string rol = reader.GetString(reader.GetOrdinal("Rol"));
                                // UserID = userId;  Name = nombre; LastName = apellidoP; MiddleName = apellidoM; Email = email; Password = storedPassword; Rol = rol;
                                // DisplayAlert("Inicio de sesión exitoso", $"¡Bienvenido, {nombre} {apellidoP}!\nTu ID de usuario es: {userId}", "Continuar");
                                // Clear();
                                // navigation();
                            // }

                            int userId = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                            string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                            string apellidoP = reader.GetString(reader.GetOrdinal("ApellidoP"));
                            string apellidoM = reader.GetString(reader.GetOrdinal("ApellidoM"));
                            string email = reader.GetString(reader.GetOrdinal("Email"));
                            string password = reader.GetString(reader.GetOrdinal("Password"));
                            string rol = reader.GetString(reader.GetOrdinal("Rol"));
                            UserID = userId;  Name = nombre; LastName = apellidoP; MiddleName = apellidoM; Email = email; Password = password; Rol = rol;
                            DisplayAlert("Inicio de sesión exitoso", $"¡Bienvenido, {nombre} {apellidoP}!\nTu ID de usuario es: {userId}", "Continuar");
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
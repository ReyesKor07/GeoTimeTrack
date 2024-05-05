using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoTimeTrack.Data;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreationPage : ContentPage
    {
        public AccountCreationPage()
        {
            InitializeComponent();
        }

        private bool ValidateName(string name)
        {
            // Verifica si el nombre contiene solo letras y espacios en blanco
            return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
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

                ConexionSQLServer.Abrir();
                string emailCheckQuery = "SELECT COUNT(*) FROM Usuario_B WHERE Email = @email"; // Consulta SQL para verificar si el correo electrónico ya existe
                using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, ConexionSQLServer.cn))
                {
                    emailCheckCmd.Parameters.AddWithValue("@email", emailEntry.Text);
                    int emailCount = (int)emailCheckCmd.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        // El correo ya existe
                        DisplayAlert("Error", "El correo proporcionado ya está registrado.", "OK");
                    }
                    else
                    {
                        // El correo no existe, insertar el nuevo usuario
                        SqlCommand cmd = new SqlCommand("INSERT INTO Usuario_B(Nombre, ApellidoP, ApellidoM, Email, Password, Rol) VALUES (@name, @apellidoP, @apellidoM, @email, @password, @rol)", ConexionSQLServer.cn);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", usernameEntry.Text);
                        cmd.Parameters.AddWithValue("@apellidoP", userlastnameEntry.Text);
                        cmd.Parameters.AddWithValue("@apellidoM", usermiddlenameEntry.Text);
                        cmd.Parameters.AddWithValue("@email", emailEntry.Text);
                        // string hashedPassword = HashPassword(passwordEntry.Text); // Antes de insertar la contraseña en la base de datos, obtenemos su hash
                        // cmd.Parameters.AddWithValue("@password", hashedPassword); // Insertamos el hash en lugar de la contraseña original
                        cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                        cmd.Parameters.AddWithValue("@rol", "Usuario");
                        cmd.ExecuteNonQuery();
                        DisplayAlert("Info", "Usuario creado con éxito", "OK");
                        Clear();
                    }
                }
                ConexionSQLServer.Cerrar();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message + "AccountCreatePage", "OK");
            }
        }

        private async void OnLoginLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public void Clear()
        {
            usernameEntry.Text = null; userlastnameEntry.Text = null; usermiddlenameEntry.Text = null;
            emailEntry.Text = null; passwordEntry.Text = null; confirmPasswordEntry.Text = null;
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Cambiar la visibilidad del texto de contraseña basado en el estado del Switch
            passwordEntry.IsPassword = !e.Value;
            confirmPasswordEntry.IsPassword = !e.Value;
        }
    }
}
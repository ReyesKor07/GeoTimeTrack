using GeoTimeTrack.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack.FlyoutTabbed.DeployPageFlyout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        public static int UserID { get; private set; }
        public static string Name { get; private set; }
        public static string LastName { get; private set; }
        public static string MiddleName { get; private set; }
        public static string Email { get; private set; }
        public static string Password { get; private set; }
        public static string Rol { get; private set; }

        public ProfilePage ()
		{
			InitializeComponent ();
            InitializeUserData();
        }

        private void InitializeUserData()
        {
            //UserId = Convert.ToInt32(await SecureStorage.GetAsync("UsuarioID"));
            //Nombre = await SecureStorage.GetAsync("Nombre");
            //ApellidoP = await SecureStorage.GetAsync("ApellidoP");
            //ApellidoM = await SecureStorage.GetAsync("ApellidoM");
            //Email = await SecureStorage.GetAsync("Email");
            //Password = await SecureStorage.GetAsync("Password");
            //Rol = await SecureStorage.GetAsync("Rol");
            UserID = LoginPage.UserID;
            Name = LoginPage.Name; LastName = LoginPage.LastName; MiddleName = LoginPage.MiddleName;
            Email = LoginPage.Email; Password = LoginPage.Password; Rol = LoginPage.Rol;
            IdUsuarioEntry.Text = UserID.ToString();
            NombreEntry.Text = Name; ApellidoPEntry.Text = LastName; ApellidoMEntry.Text = MiddleName;
            EmailEntry.Text = Email; PasswordEntry.Text = Password; RolEntry.Text = Rol;
            IdUsuarioEntry.IsVisible = true; RolEntry.IsVisible = true;
        }

        private void GuardarCambios_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia
                DisplayAlert("Error", "Necesitas estar conectado a Internet para guaradr los cambios.", "OK");
                return;
            }
            try
            {
                // Verificar campos obligatorios
                if (string.IsNullOrWhiteSpace(NombreEntry.Text) || string.IsNullOrWhiteSpace(ApellidoPEntry.Text) || string.IsNullOrWhiteSpace(ApellidoMEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
                {
                    DisplayAlert("Advertencia", "Todos los campos obligatorios son requeridos y no pueden estar vacíos.", "Aceptar");
                    return;
                }
                // Verificar si el correo ha cambiado
                string newEmail = EmailEntry.Text;
                if (newEmail != Email)
                {
                    ConexionSQLServer.Abrir();
                    string emailCheckQuery = "SELECT COUNT(*) FROM Usuario_B WHERE Email = @newEmail";
                    using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, ConexionSQLServer.cn))
                    {
                        emailCheckCmd.Parameters.AddWithValue("@newEmail", newEmail);
                        int emailCount = (int)emailCheckCmd.ExecuteScalar();
                        emailCheckCmd.Dispose(); // Cierra y libera recursos del DataReader
                        if (emailCount > 0)
                        {
                            DisplayAlert("Error", "El nuevo correo proporcionado ya existe.", "Aceptar");
                            return;
                        }
                    }
                }
                ConexionSQLServer.Abrir();
                string updateQuery = "UPDATE Usuario_B SET Nombre = @Nombre, ApellidoP = @ApellidoP, ApellidoM = @ApellidoM, Password = @Password, Email = @newEmail WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmd = new SqlCommand(updateQuery, ConexionSQLServer.cn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", UserID);
                    cmd.Parameters.AddWithValue("@Nombre", NombreEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoP", ApellidoPEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoM", ApellidoMEntry.Text);
                    cmd.Parameters.AddWithValue("@newEmail", newEmail);
                    cmd.Parameters.AddWithValue("@Password", PasswordEntry.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        DisplayAlert("Éxito", "Los cambios se guardaron correctamente.", "Aceptar");
                    }
                    else
                    {
                        DisplayAlert("Error", "No se pudieron guardar los cambios.", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Ocurrió un error al guardar los cambios: " + ex.Message + " ProfilePage", "Aceptar");
            }
            finally
            {
                ConexionSQLServer.Cerrar();
            }
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            { PasswordEntry.IsPassword = false; }
            else
            { PasswordEntry.IsPassword = true; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack.FlyoutTabbed.DeployPageFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string NombreC => $" {ApellidoP} {ApellidoM} {Nombre}";
        public string CombinedInfo => $" {IdUsuario} {NombreC} {Rol}";
    }

    public partial class AdminPage : ContentPage
    {
        int UserId;
        string Nombre, ApellidoP, ApellidoM, Email, Password, Rol;
        ObservableCollection<Usuario> allUsuarios;

        public AdminPage()
        {
            InitializeComponent();
            UserId = LoginPage.UserID;
            Nombre = LoginPage.Name; ApellidoP = LoginPage.LastName; ApellidoM = LoginPage.MiddleName;
            Email = LoginPage.Email; Password = LoginPage.Password;
            Rol = LoginPage.Rol;
            // UserId = 1; Nombre = "Brandon"; ApellidoP = "Reyes"; ApellidoM = "De La Cruz"; Email = "brandonreyes@gmail.com"; Password = "123"; Rol = "Administrador";
            List<Usuario> usuarios = ObtenerUsuarios();
            Usuarios.ItemsSource = usuarios;
            // Obtener la lista completa de usuarios
            allUsuarios = new ObservableCollection<Usuario>(ObtenerUsuarios());
            Usuarios.ItemsSource = allUsuarios;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            List<Usuario> usuarios = ObtenerUsuarios();
            Usuarios.ItemsSource = usuarios;
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    if (button.BindingContext is Usuario selectedUser)
                    {
                        // Crea una instancia de EditProfilePage y pasa los datos del usuario
                        EditProfilePage editProfilePage = new EditProfilePage(selectedUser);
                        await Navigation.PushModalAsync(editProfilePage);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message} AdminPage", "OK");
            }
        }

        private List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string connectionString = @"Server= P3NWPLSK12SQL-v08.shr.prod.phx3.secureserver.net; DataBase=projecttes; User ID= prject; Password=proyec2023_;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    string query = "SELECT IdUsuario, Nombre, ApellidoP, ApellidoM, Email, Password, Rol FROM Usuario_B";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    ApellidoP = reader.GetString(reader.GetOrdinal("ApellidoP")),
                                    ApellidoM = reader.GetString(reader.GetOrdinal("ApellidoM")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Rol = reader.GetString(reader.GetOrdinal("Rol"))
                                };
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí, por ejemplo, mostrar un mensaje de error.
                DisplayAlert("Error", "Ocurrió un error al obtener usuarios: " + ex.Message + "AdminPage", "OK");
            }
            // Ordenar la lista por ApellidoP en orden alfabético
            usuarios = usuarios.OrderBy(usuario => usuario.ApellidoP).ToList();
            return usuarios;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Si el texto de búsqueda está vacío, mostrar todos los usuarios
                Usuarios.ItemsSource = allUsuarios;
            }
            else
            {
                // Filtrar la lista de usuarios según el texto de búsqueda
                var filteredUsuarios = allUsuarios.Where(u =>
                    u.Nombre.ToLower().Contains(searchText.ToLower()) ||
                    u.ApellidoP.ToLower().Contains(searchText.ToLower()) ||
                    u.ApellidoM.ToLower().Contains(searchText.ToLower()) ||
                    u.IdUsuario.ToString().Contains(searchText.ToLower())
                );
                Usuarios.ItemsSource = new ObservableCollection<Usuario>(filteredUsuarios);
            }
        }
    }
}
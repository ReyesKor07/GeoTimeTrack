using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using Xamarin.Essentials;
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
    }

    public partial class AdminPage : ContentPage
    {
        ObservableCollection<Usuario> allUsuarios; // Colección observable de todos los usuarios

        public AdminPage()
        {
            InitializeComponent();
            // Obtener y mostrar todos los usuarios
            List<Usuario> usuarios = ObtenerUsuarios();
            Usuarios.ItemsSource = usuarios;
            allUsuarios = new ObservableCollection<Usuario>(ObtenerUsuarios());
            Usuarios.ItemsSource = allUsuarios;
        }

        private void RefreshButtonClicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                DisplayAlert("Error", "Necesitas estar conectado a Internet para refrescar la página.", "OK");
                return;
            }
            else
            {
                // Refrescar la lista de usuarios
                List<Usuario> usuarios = ObtenerUsuarios();
                Usuarios.ItemsSource = usuarios;
            }
        }

        // Método para manejar el evento de hacer clic en el botón "Ver Registro" de un usuario
        public async void VerRegistro_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para observar el registro.", "OK");
                return;
            }
            try
            {
                if (sender is Button button)
                {
                    if (button.BindingContext is Usuario selectedUser)
                    {
                        // Crear y mostrar la página para editar el registro del usuario seleccionado
                        EditTrackTimePage editTrackTimePage = new EditTrackTimePage(selectedUser);
                        await Navigation.PushModalAsync(editTrackTimePage);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message} AdminPage", "OK");
            }
        }

        // Método para manejar el evento de hacer clic en el botón "Ver Perfil" de un usuario
        private async void VerPerfil_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para observar el perfil.", "OK");
                return;
            }
            try
            {
                if (sender is Button button)
                {
                    if (button.BindingContext is Usuario selectedUser)
                    {
                        // Crear y mostrar la página para editar el perfil del usuario seleccionado
                        EditProfilePage editProfilePage = new EditProfilePage(selectedUser);
                        await Navigation.PushModalAsync(editProfilePage);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "OK");
            }
        }

        // Método para obtener la lista de todos los usuarios
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
                                // Crear un usuario a partir de los datos de la base de datos y agregarlo a la lista
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
                DisplayAlert("Error", "Ocurrió un error al obtener usuarios: " + ex.Message + "AdminPage", "OK");
            }
            // Ordenar la lista de usuarios por ApellidoP en orden alfabético
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
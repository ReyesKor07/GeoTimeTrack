using GeoTimeTrack.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

using Xamarin.Forms.GoogleMaps;
using GeoTimeTrack.FlyoutTabbed;
using System.Data.SqlClient;

namespace GeoTimeTrack
{
    public partial class MainPage : ContentPage
    {
        string Nombre;
        string ApellidoP;
        int UserId;

        public MainPage()
        {
            InitializeComponent();
            Nombre = LoginPage.Name;
            ApellidoP = LoginPage.LastName;
            UserId = LoginPage.UserID;
            HolaLabel.Text = $"¡Hola {Nombre} {ApellidoP},\nTu ID es: {UserId}";
            Position initialPosition = new Position(26.028688727720997, -98.27560757446295); // Establecer la posición inicial del mapa UAT
            // Position initialPosition = new Position(26.007790168972313, -98.24903183076913); // Establecer la posición inicial del mapa C.I. 
            map.MoveToRegion(MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMeters(200)));
            mapTypeSwitch.Toggled += MapTypeSwitch_Toggled; // Agregar un controlador de eventos al Switch
        }

        private void MapTypeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            map.MapType = e.Value ? MapType.Satellite : MapType.Street; // Cambiar el tipo de mapa (MapType) en función del estado del Switch
            string mapTypeText = e.Value ? "Satélite" : "Mapa"; // Cambiar el texto del Label para reflejar el estado actual
        }

        // Coordenadas fijas para comparación UAT
        private readonly double targetLatitude = 26.028688727720997;
        private readonly double targetLongitude = -98.27560757446295;

        // Coordenadas fijas para comparación C.I.
        // private readonly double targetLatitude = 26.007790168972313;
        // private readonly double targetLongitude = -98.24903183076913;

        private Color entryPinColor = Color.Blue; // Color del pin de entrada
        private Color exitPinColor = Color.Red; // Color del pin de salida

        private Pin entryLocationPin; // Variable para el Pin de Entrada
        private Pin exitLocationPin;  // Variable para el Pin de Salida

        private DateTime entryTime; // Almacena la hora de entrada
        private DateTime exitTime;  // Almacena la hora de salida

        private decimal entrylongitudeEntry;
        private decimal entrylatitudeEntry;
        private decimal exitlongitudeEntry;
        private decimal exitlatitudeEntry;
        
        private int IDuserEntry;

        private async void OnEntryButtonClicked(object sender, EventArgs e)
        {
            try
            {
                entryTime = DateTime.Now; // Registrar la hora de entrada

                var location = await Geolocation.GetLocationAsync();

                Clear();

                if (location != null)
                {
                    // Crear Location para la ubicación actual del usuario
                    Location userLocation = new Location(location.Latitude, location.Longitude);
                    // Crear Location para las coordenadas fijas
                    Location fixedLocation = new Location(targetLatitude, targetLongitude);
                    // Calcular la distancia en metros entre las dos ubicaciones
                    double distanceInMeters = Location.CalculateDistance(userLocation, fixedLocation, DistanceUnits.Kilometers) * 1000;

                    if (distanceInMeters <= 100) // Verificar si la distancia es igual o menor a 100 metros
                    {
                        // Verificar si ya existe un pin de entrada anterior y eliminarlo
                        if (entryLocationPin != null)
                        {
                            map.Pins.Remove(entryLocationPin);
                        }

                        // Crear un nuevo pin de entrada
                        entryLocationPin = new Pin
                        {
                            Type = PinType.Place,
                            Label = "Entrada",
                            Position = new Position(location.Latitude, location.Longitude),
                            Icon = BitmapDescriptorFactory.DefaultMarker(entryPinColor)
                        };
                        map.Pins.Add(entryLocationPin);

                        entryTimeEntry.Text = entryTime.ToString("HH:mm:ss"); // Actualizar los campos de la interfaz con la hora y ubicación de entrada
                        entryDateEntry.Text = entryTime.ToString("dd-MM-yyyy");
                        var newMapSpan = MapSpan.FromCenterAndRadius(entryLocationPin.Position, Distance.FromMeters(20)); // Centrar el mapa en la ubicación de entrada
                        map.MoveToRegion(newMapSpan);

                        entrylongitudeEntry = (decimal)location.Latitude;
                        entrylatitudeEntry = (decimal)location.Longitude;

                        entryLocationEntry.Text = $"{distanceInMeters:F2}"; // Mostrar la distancia en metros

                        entryButton.IsEnabled = false; // Deshabilitar el botón de entrada y habilitar el de salida
                        exitButton.IsEnabled = true;
                    }
                    else
                    {
                        await DisplayAlert("Advertencia", "Estás fuera del rango para marcar tu asistencia (distancia superior a 100 metros).", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo obtener la ubicación actual", "OK");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Por favor, asegúrate de tener el GPS activado.", "OK");
            }
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (entryTime != DateTime.MinValue)
                {
                    IDuserEntry = UserId;

                    exitTime = DateTime.Now; // Registrar la hora de salida

                    var location = await Geolocation.GetLocationAsync();

                    if (location != null)
                    {
                        // Crear Location para la ubicación actual del usuario
                        Location userLocation = new Location(location.Latitude, location.Longitude);
                        // Crear Location para las coordenadas fijas
                        Location fixedLocation = new Location(targetLatitude, targetLongitude);
                        // Calcular la distancia en metros entre las dos ubicaciones
                        double distanceInMeters = Location.CalculateDistance(userLocation, fixedLocation, DistanceUnits.Kilometers) * 1000;

                        if (distanceInMeters <= 100) // Verificar si la distancia es igual o menor a 100 metros
                        {
                            // Verificar si ya existe un pin de salida anterior y eliminarlo
                            if (exitLocationPin != null)
                            {
                                map.Pins.Remove(exitLocationPin);
                            }

                            // Crear un nuevo pin de salida
                            exitLocationPin = new Pin
                            {
                                Type = PinType.Place,
                                Label = "Salida",
                                Position = new Position(location.Latitude, location.Longitude),
                                Icon = BitmapDescriptorFactory.DefaultMarker(exitPinColor)
                            };
                            map.Pins.Add(exitLocationPin);

                            TimeSpan timeDifference = exitTime - entryTime; // Calcular la diferencia de tiempo entre entrada y salida

                            exitTimeEntry.Text = exitTime.ToString("HH:mm:ss"); // Actualizar los campos de la interfaz con la hora y ubicación de salida
                            exitDateEntry.Text = exitTime.ToString("dd-MM-yyyy");
                            workTimeEntry.Text = timeDifference.ToString(@"hh\:mm\:ss");
                            var newMapSpan = MapSpan.FromCenterAndRadius(exitLocationPin.Position, Distance.FromMeters(20)); // Centrar el mapa en la ubicación de salida
                            map.MoveToRegion(newMapSpan);

                            exitlongitudeEntry = (decimal)location.Latitude;
                            exitlatitudeEntry = (decimal)location.Longitude;

                            exitLocationEntry.Text = $"{distanceInMeters:F2}"; // Mostrar la distancia en metros

                            entryButton.IsEnabled = true; // Habilitar el botón de entrada y deshabilitar el de salida
                            exitButton.IsEnabled = false;
                            Conexion();
                        }
                        else
                        {
                            await DisplayAlert("Advertencia", "Estás fuera del rango para marcar tu salida (distancia superior a 100 metros).", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo obtener la ubicación actual", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Advertencia", "Antes de continuar, por favor registre tu entrada.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
        }

        public void Conexion()
        {
            try
            {
                ConexionSQLServer.Abrir();
                SqlCommand cmd = new SqlCommand("INSERT INTO Registro(IdUsuario, HoraEntrada, FechaEntrada, LatitudEntrada, LongitudEntrada, DistanciaEntrada, HoraSalida, FechaSalida, LatitudSalida, LongitudSalida, DistanciaSalida, TiempoTotal)VALUES(@iduser, @entryTime, @entryDate, @entrylongitude, @entrylatitude, @entryLocation, @exitTime, @exitDate, @exitlongitude, @exitlatitude, @exitLocation, @workTime)", ConexionSQLServer.cn);
                cmd.CommandType = System.Data.CommandType.Text;
                /*ID Usuario*/
                cmd.Parameters.AddWithValue("@iduser", IDuserEntry);
                /*Entrada*/
                cmd.Parameters.AddWithValue("@entryTime", entryTimeEntry.Text);
                cmd.Parameters.AddWithValue("@entryDate", entryDateEntry.Text);
                cmd.Parameters.AddWithValue("@entrylongitude", entrylongitudeEntry);
                cmd.Parameters.AddWithValue("@entrylatitude", entrylatitudeEntry);
                cmd.Parameters.AddWithValue("@entryLocation", entryLocationEntry.Text);
                /*Salida*/
                cmd.Parameters.AddWithValue("@exitTime", exitTimeEntry.Text);
                cmd.Parameters.AddWithValue("@exitDate", exitDateEntry.Text);
                cmd.Parameters.AddWithValue("@exitlongitude", exitlongitudeEntry);
                cmd.Parameters.AddWithValue("@exitlatitude", exitlatitudeEntry);
                cmd.Parameters.AddWithValue("@exitLocation", exitLocationEntry.Text);
                /*Tiempo Laboral*/
                cmd.Parameters.AddWithValue("@workTime", workTimeEntry.Text);
                cmd.ExecuteNonQuery();
                DisplayAlert("Info", "Datos capturados con exito", "Okay");
                ConexionSQLServer.Cerrar();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message + "MainPage.Conexion", "OK");
            }
        }
        
        public void Clear()
        {
            /*Entrada*/
            entryTimeEntry.Text = null; entryDateEntry.Text = null; entryLocationEntry.Text = null;
            /*Salida*/
            exitTimeEntry.Text = null; exitDateEntry.Text = null; exitLocationEntry.Text = null;
            /*Tiempo Laboral*/
            workTimeEntry.Text = null;
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            string name = LoginPage.Name;
            string lastname = LoginPage.LastName;
            int userid = LoginPage.UserID;
            bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas cerrar la sesión?", "Sí", "Cancelar");
            if (answer)
            {
                // Elimina los datos de usuario almacenados
                name = null; 
                lastname = null; 
                userid = 0;
                // Redirige al usuario a la página de inicio de sesión o a la página de inicio de la aplicación
                await Navigation.PushModalAsync(new LoginPage()); // O la página principal de tu aplicación
            }
        }

    }
}
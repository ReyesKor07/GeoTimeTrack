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
            HolaLabel.Text = $"Hola {Nombre} {ApellidoP}, ID:{UserId}";
            Position initialPosition = new Position(26.028688727720997, -98.27560757446295); // Establecer la posición inicial del mapa
            map.MoveToRegion(MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMeters(200)));
            mapTypeSwitch.Toggled += MapTypeSwitch_Toggled; // Agregar un controlador de eventos al Switch
        }

        private void MapTypeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            map.MapType = e.Value ? MapType.Satellite : MapType.Street; // Cambiar el tipo de mapa (MapType) en función del estado del Switch
            string mapTypeText = e.Value ? "Satélite" : "Mapa"; // Cambiar el texto del Label para reflejar el estado actual
        }

        // Coordenadas fijas para comparación
        private readonly double targetLatitude = 26.028688727720997;
        private readonly double targetLongitude = -98.27560757446295;

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

                if (location != null)
                {
                    var entry = new TimeEntry // Crea una instancia de TimeEntry para la entrada y guarda en la base de datos
                    {
                        EntryTime = entryTime,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };

                    var dbHelper = new DatabaseHelper();
                    await dbHelper.InsertTimeEntry(entry);

                    Pin entryLocationPin = new Pin // Agregar un marcador al mapa para la ubicación de entrada
                    {
                        Type = PinType.Place,
                        Label = "Entrada",
                        Position = new Position(location.Latitude, location.Longitude)
                    };
                    map.Pins.Add(entryLocationPin);

                    entryTimeEntry.Text = entryTime.ToString("HH:mm:ss"); // Actualizar los campos de la interfaz con la hora y ubicación de entrada
                    // entryDateEntry.Text = entryTime.ToString("dddd dd MMMM yyyy");
                    // entryDateEntry.Text = entryTime.ToString("yyyy-MM-dd");
                    entryDateEntry.Text = entryTime.ToString("dd-MM-yyyy");
                    var newMapSpan = MapSpan.FromCenterAndRadius(entryLocationPin.Position, Distance.FromMeters(20)); // Centrar el mapa en la ubicación de entrada
                    map.MoveToRegion(newMapSpan);

                    entryButton.IsEnabled = false; // Deshabilitar el botón de entrada y habilitar el de salida
                    exitButton.IsEnabled = true;

                    // entryLocationEntry.Text = $"{location.Latitude} | {location.Longitude}"; // Ubicación de entrada

                    entrylongitudeEntry = ((decimal)location.Latitude);
                    entrylatitudeEntry = ((decimal)location.Longitude);

                    // Crear Location para la ubicación actual del usuario
                    Location userLocation = new Location(location.Latitude, location.Longitude);

                    // Crear Location para las coordenadas fijas
                    Location fixedLocation = new Location(targetLatitude, targetLongitude);

                    // Calcular la distancia en metros entre las dos ubicaciones
                    double distanceInMeters = Location.CalculateDistance(userLocation, fixedLocation, DistanceUnits.Kilometers) * 1000;

                    entryLocationEntry.Text = $"{distanceInMeters:F2}"; // Mostrar la distancia en metros



                }
                else
                {
                    await DisplayAlert("Error", "No se pudo obtener la ubicación actual", "OK");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Por favor, asegúrate de tener el GPS activado.", "OK");
            }
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (entryTime != DateTime.MinValue) // Verificar si ya se registró una entrada
                {
                    exitTime = DateTime.Now; // Registrar la hora de salida

                    var location = await Geolocation.GetLocationAsync();

                    if (location != null)
                    {
                        var exitEntry = new TimeEntry // Crear una instancia de TimeEntry para la salida y guardarla en la base de datos
                        {
                            ExitTime = exitTime,
                            Latitude = location.Latitude,
                            Longitude = location.Longitude
                        };

                        var dbHelper = new DatabaseHelper();
                        await dbHelper.InsertTimeEntry(exitEntry);

                        Pin exitLocationPin = new Pin // Agregar un marcador al mapa para la ubicación de salida
                        {
                            Type = PinType.Place,
                            Label = "Salida",
                            Position = new Position(location.Latitude, location.Longitude)
                        };
                        map.Pins.Add(exitLocationPin);

                        TimeSpan timeDifference = exitTime - entryTime; // Calcular la diferencia de tiempo entre entrada y salida

                        exitTimeEntry.Text = exitTime.ToString("HH:mm:ss"); // Actualizar los campos de la interfaz con la hora y ubicación de salida
                     // exitDateEntry.Text = exitTime.ToString("dddd dd MMMM yyyy");
                        exitDateEntry.Text = exitTime.ToString("yyyy-MM-dd");
                        workTimeEntry.Text = timeDifference.ToString(@"hh\:mm\:ss");
                        // exitLocationEntry.Text = $"{location.Latitude} | {location.Longitude}"; // Ubicación de salida

                        exitlongitudeEntry = ((decimal)location.Latitude);
                        exitlatitudeEntry = ((decimal)location.Longitude);

                        IDuserEntry = UserId;

                        // Crear Location para la ubicación actual del usuario
                        Location userLocation = new Location(location.Latitude, location.Longitude);

                        // Crear Location para las coordenadas fijas
                        Location fixedLocation = new Location(targetLatitude, targetLongitude);

                        // Calcular la distancia en metros entre las dos ubicaciones
                        double distanceInMeters = Location.CalculateDistance(userLocation, fixedLocation, DistanceUnits.Kilometers) * 1000;

                        exitLocationEntry.Text = $"{distanceInMeters:F2}"; // Mostrar la distancia en metros
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo obtener la ubicación actual", "OK");
                    }

                    Conexion();

                    // Habilitar el botón de entrada y deshabilitar el de salida
                    entryButton.IsEnabled = true;
                    exitButton.IsEnabled = false;
                }
                else
                {
                    await DisplayAlert("Advertencia", "Antes de continuar, por favor registre su entrada.", "OK");
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
    }
}
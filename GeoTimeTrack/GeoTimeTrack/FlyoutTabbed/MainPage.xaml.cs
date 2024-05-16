﻿using GeoTimeTrack.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

//using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Maps;
using GeoTimeTrack.FlyoutTabbed;
using System.Data.SqlClient;

namespace GeoTimeTrack
{
    public partial class MainPage : ContentPage
    {
        // Coordenadas fijas para comparación UAT
        private readonly double targetLatitude = 26.028688727720997;
        private readonly double targetLongitude = -98.27560757446295;

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

        public int UserID { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            InitializeUserData();
            Position initialPosition = new Position(26.028688727720997, -98.27560757446295); // Establecer la posición inicial del mapa UAT
            map.MoveToRegion(MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMeters(200)));
            map.MapType = MapType.Satellite; // Establecer el modo de mapa predeterminado como satélite
            mapTypeSwitch.Toggled += MapTypeSwitch_Toggled; // Agregar un controlador de eventos al Switch
            entryButton.IsEnabled = true; // Habilitar el botón de entrada
            exitButton.IsEnabled = false; // Deshabilitar el botón de salida
        }

        //public MainPage(int userId, string nombre, string apellidoP)
        //{
        //    UserId = userId;
        //    Nombre = nombre;
        //    ApellidoP = apellidoP;
        //}

        private void InitializeUserData()
        {
            //int usuarioID = Convert.ToInt32(await SecureStorage.GetAsync("UsuarioID"));
            //string Nombre = await SecureStorage.GetAsync("Nombre");
            //string ApellidoP = await SecureStorage.GetAsync("ApellidoP");
            //HolaLabel.Text = $"¡Hola! {Nombre} {ApellidoP} \nTu ID es: {UserId}";
            UserID = LoginPage.UserID;
            Name = LoginPage.Name;
            LastName = LoginPage.LastName;
            HolaLabel.Text = $"¡Hola! {Name} {LastName} \nTu ID es: {UserID}";
        }

        private void MapTypeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            map.MapType = e.Value ? MapType.Street : MapType.Satellite; // Cambiar el tipo de mapa (MapType) en función del estado del Switch
            string mapTypeText = e.Value ? "Mapa" : "Satélite"; // Cambiar el texto del Label para reflejar el estado actual
        }

        private async void OnEntryButtonClicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para marcar tu entrada.", "OK");
                return;
            }
            try
            {
                Clear();
                entryTime = DateTime.Now; // Registrar la hora de entrada
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    Location userLocation = new Location(location.Latitude, location.Longitude); // Crear Location para la ubicación actual del usuario
                    Location fixedLocation = new Location(targetLatitude, targetLongitude); // Crear Location para las coordenadas fijas
                    double distanceInMeters = Location.CalculateDistance(userLocation, fixedLocation, DistanceUnits.Kilometers) * 1000; // Calcular la distancia en metros entre las dos ubicaciones
                    //if (distanceInMeters <= 200) // Verificar la distancia
                    //{
                        if (entryLocationPin != null) // Verificar si ya existe un pin de entrada anterior y eliminarlo
                        {
                            map.Pins.Remove(entryLocationPin);
                        }
                        entryLocationPin = new Pin // Crear un nuevo pin de entrada
                        {
                            Type = PinType.Place,
                            Label = "Entrada",
                            Position = new Position(location.Latitude, location.Longitude),
                            // Icon = BitmapDescriptorFactory.DefaultMarker(entryPinColor)
                        };
                        map.Pins.Add(entryLocationPin);
                        entryTimeEntry.Text = entryTime.ToString("HH:mm:ss"); // Actualizar los campos de la interfaz con la hora y ubicación de entrada
                        entryDateEntry.Text = entryTime.ToString("dd-MMMM-yyyy");
                        var newMapSpan = MapSpan.FromCenterAndRadius(entryLocationPin.Position, Distance.FromMeters(40)); // Centrar el mapa en la ubicación de entrada
                        map.MoveToRegion(newMapSpan);
                        entrylongitudeEntry = (decimal)location.Longitude;
                        entrylatitudeEntry = (decimal)location.Latitude;
                        entryLongitudeEntry.Text = $"{entrylongitudeEntry:F6}"; // Mostrar longitud
                        entryLatitudeEntry.Text = $"{entrylatitudeEntry:F6}"; // Mostrar latitud
                        entryButton.IsEnabled = false; // Deshabilitar el botón de entrada y habilitar el de salida
                        exitButton.IsEnabled = true;
                    //}
                    //else
                    //{
                    //    await DisplayAlert("Advertencia", "Estás fuera del rango para marcar tu asistencia.", "OK");
                    //}
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
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para marcar tu salida.", "OK");
                return;
            }
            try
            {
                if (entryTime != DateTime.MinValue)
                {
                    IDuserEntry = UserID;
                    exitTime = DateTime.Now; // Registrar la hora de salida
                    var location = await Geolocation.GetLocationAsync();
                    if (location != null)
                    {
                        Location userLocation = new Location(location.Latitude, location.Longitude); // Crear Location para la ubicación actual del usuario
                        Location fixedLocation = new Location(targetLatitude, targetLongitude); // Crear Location para las coordenadas fijas
                        double distanceInMeters = Location.CalculateDistance(userLocation, fixedLocation, DistanceUnits.Kilometers) * 1000; // Calcular la distancia en metros entre las dos ubicaciones
                        //if (distanceInMeters <= 200) // Verificar la distancia
                        //{
                            if (exitLocationPin != null) // Verificar si ya existe un pin de salida anterior y eliminarlo
                            {
                                map.Pins.Remove(exitLocationPin);
                            }
                            exitLocationPin = new Pin // Crear un nuevo pin de salida
                            {
                                Type = PinType.Place,
                                Label = "Salida",
                                Position = new Position(location.Latitude, location.Longitude),
                                // Icon = BitmapDescriptorFactory.DefaultMarker(exitPinColor)
                            };
                            map.Pins.Add(exitLocationPin);
                            TimeSpan timeDifference = exitTime - entryTime; // Calcular la diferencia de tiempo entre entrada y salida
                            exitTimeEntry.Text = exitTime.ToString("HH:mm:ss"); // Actualizar los campos de la interfaz con la hora y ubicación de salida
                            exitDateEntry.Text = exitTime.ToString("dd-MMMM-yyyy");
                            workTimeEntry.Text = timeDifference.ToString(@"hh\:mm\:ss");
                            var newMapSpan = MapSpan.FromCenterAndRadius(exitLocationPin.Position, Distance.FromMeters(40)); // Centrar el mapa en la ubicación de salida
                            map.MoveToRegion(newMapSpan);
                            exitlongitudeEntry = (decimal)location.Longitude;
                            exitlatitudeEntry = (decimal)location.Latitude;
                            exitLongitudeEntry.Text = $"{exitlongitudeEntry:F6}"; // Mostrar longitud
                            exitLatitudeEntry.Text = $"{exitlatitudeEntry:F6}"; // Mostrar latitud
                            entryButton.IsEnabled = true; // Habilitar el botón de entrada y deshabilitar el de salida
                            exitButton.IsEnabled = false;
                            Conexion();
                        //}
                        //else
                        //{
                        //    await DisplayAlert("Advertencia", "Estás fuera del rango para marcar tu salida.", "OK");
                        //}
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Registro_B(IdUsuario, HoraEntrada, FechaEntrada, LatitudEntrada, LongitudEntrada, HoraSalida, FechaSalida, LatitudSalida, LongitudSalida, TiempoTotal)VALUES(@iduser, @entryTime, @entryDate, @entrylongitude, @entrylatitude, @exitTime, @exitDate, @exitlongitude, @exitlatitude, @workTime)", ConexionSQLServer.cn);
                cmd.CommandType = System.Data.CommandType.Text;
                /*ID Usuario*/
                cmd.Parameters.AddWithValue("@iduser", IDuserEntry);
                /*Entrada*/
                cmd.Parameters.AddWithValue("@entryTime", entryTime.ToString("HH:mm:ss"));
                cmd.Parameters.AddWithValue("@entryDate", entryTime.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@entrylongitude", entrylongitudeEntry);
                cmd.Parameters.AddWithValue("@entrylatitude", entrylatitudeEntry);
                /*Salida*/
                cmd.Parameters.AddWithValue("@exitTime", exitTime.ToString("HH:mm:ss"));
                cmd.Parameters.AddWithValue("@exitDate", exitTime.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@exitlongitude", exitlongitudeEntry);
                cmd.Parameters.AddWithValue("@exitlatitude", exitlatitudeEntry);
                /*Tiempo de Estancia*/
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
            entryTimeEntry.Text = null;
            entryDateEntry.Text = null;
            entryLongitudeEntry.Text = null;
            entryLatitudeEntry.Text = null;
            /*Salida*/
            exitTimeEntry.Text = null;
            exitDateEntry.Text = null;
            exitLongitudeEntry.Text = null;
            exitLatitudeEntry.Text = null;
            /*Tiempo Laboral*/
            workTimeEntry.Text = null;
        }
    }
}
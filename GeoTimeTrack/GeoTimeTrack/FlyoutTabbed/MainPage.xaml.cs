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

namespace GeoTimeTrack
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Establecer la posición inicial del mapa
            Position initialPosition = new Position(26.028688727720997, -98.27560757446295);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMeters(200)));
        }

        // Coordenadas fijas para comparación
        private readonly double targetLatitude = 26.028688727720997;
        private readonly double targetLongitude = -98.27560757446295;

        private DateTime entryTime; // Almacena la hora de entrada
        private DateTime exitTime;  // Almacena la hora de salida



        private async void OnEntryButtonClicked(object sender, EventArgs e)
        {
            try
            {
                entryTime = DateTime.Now; // Registrar la hora de entrada

                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    // Crear una instancia de TimeEntry para la entrada y guardarla en la base de datos
                    var entry = new TimeEntry
                    {
                        EntryTime = entryTime,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };

                    var dbHelper = new DatabaseHelper();
                    await dbHelper.InsertTimeEntry(entry);

                    // Agregar un marcador al mapa para la ubicación de entrada
                    Pin entryLocationPin = new Pin
                    {
                        Type = PinType.Place,
                        Label = "Entrada",
                        Position = new Position(location.Latitude, location.Longitude)
                    };
                    map.Pins.Add(entryLocationPin);

                    // Actualizar los campos de la interfaz con la hora y ubicación de entrada
                    entryTimeEntry.Text = entryTime.ToString("HH : mm : ss");
                    entryDateEntry.Text = entryTime.ToString("dddd dd MMMM yyyy");

                    // Centrar el mapa en la ubicación de entrada
                    var newMapSpan = MapSpan.FromCenterAndRadius(entryLocationPin.Position, Distance.FromMeters(20));
                    map.MoveToRegion(newMapSpan);

                    // Deshabilitar el botón de entrada y habilitar el de salida
                    entryButton.IsEnabled = false;
                    exitButton.IsEnabled = true;

                    // // Mostrar la ubicación de entrada en el campo de texto // entryLocationEntry.Text = $"{location.Latitude} | {location.Longitude}";

                    // Calcular la distancia entre las coordenadas fijas y la ubicación actual del usuario
                    double distanceToTarget = Location.CalculateDistance(location.Latitude, location.Longitude, targetLatitude, targetLongitude, DistanceUnits.Kilometers);

                    // Mostrar la distancia en el campo de texto
                    entryLocationEntry.Text = $"{distanceToTarget:F2} km";
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
                        // Crear una instancia de TimeEntry para la salida y guardarla en la base de datos
                        var exitEntry = new TimeEntry
                        {
                            ExitTime = exitTime,
                            Latitude = location.Latitude,
                            Longitude = location.Longitude
                        };

                        var dbHelper = new DatabaseHelper();
                        await dbHelper.InsertTimeEntry(exitEntry);

                        // Agregar un marcador al mapa para la ubicación de salida
                        Pin exitLocationPin = new Pin
                        {
                            Type = PinType.Place,
                            Label = "Salida",
                            Position = new Position(location.Latitude, location.Longitude)
                        };
                        map.Pins.Add(exitLocationPin);

                        // Calcular la diferencia de tiempo entre entrada y salida
                        TimeSpan timeDifference = exitTime - entryTime;

                        // Actualizar los campos de la interfaz con la hora y ubicación de salida
                        exitTimeEntry.Text = exitTime.ToString("HH : mm : ss");
                        exitDateEntry.Text = exitTime.ToString("dddd dd MMMM yyyy");
                        workTimeEntry.Text = timeDifference.ToString(@"hh\:mm\:ss");

                        // // Mostrar la ubicación de salida en el campo de texto // exitLocationEntry.Text = $"{location.Latitude} | {location.Longitude}";

                        // Calcular la distancia entre las coordenadas fijas y la ubicación actual del usuario
                        double distanceToTarget = Location.CalculateDistance(location.Latitude, location.Longitude, targetLatitude, targetLongitude, DistanceUnits.Kilometers);

                        // Mostrar la distancia en el campo de texto
                        exitLocationEntry.Text = $"{distanceToTarget:F2} km";
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo obtener la ubicación actual", "OK");
                    }

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
    }
}
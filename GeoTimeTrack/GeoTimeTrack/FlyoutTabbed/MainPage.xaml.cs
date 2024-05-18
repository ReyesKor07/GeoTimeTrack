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
using Xamarin.Forms.Maps;
using GeoTimeTrack.FlyoutTabbed;
using System.Data.SqlClient;

namespace GeoTimeTrack
{
    public partial class MainPage : ContentPage
    {
        public int UserID { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        private int IDuserEntry;

        private decimal entrylongitudeEntry;
        private decimal entrylatitudeEntry;
        private decimal exitlongitudeEntry;
        private decimal exitlatitudeEntry;

        private Pin entryLocationPin; // Variable para el Pin de Entrada
        private Pin exitLocationPin;  // Variable para el Pin de Salida

        private DateTime entryTime; // Almacena la hora de entrada
        private DateTime exitTime;  // Almacena la hora de salida

        public MainPage()
        {
            InitializeComponent();
            InitializeUserData();
            Position initialPosition = new Position(26.028688727720997, -98.27560757446295); // Establecer la posición inicial del mapa UAT
            map.MoveToRegion(MapSpan.FromCenterAndRadius(initialPosition, Distance.FromMeters(200)));
            map.MapType = MapType.Satellite; // Establecer el modo de mapa predeterminado como satélite
            mapTypeSwitch.Toggled += MapTypeSwitch_Toggled; // Agregar un controlador de eventos al Switch
            entryButton.IsEnabled = true;
            exitButton.IsEnabled = false;
        }

        private void InitializeUserData()
        {
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

        //public async void RegisterAttendance()
        //{


        //    // Si pasa todas las verificaciones, llamar al método existente
        //    OnEntryButtonClicked(null, EventArgs.Empty);
        //}

        private async void OnEntryButtonClicked(object sender, EventArgs e)
        {
            // Verificar si esta conectado a Internet
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para marcar tu entrada.", "OK");
                return;
            }
            // Verificar si es sábado o domingo
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                await DisplayAlert("Advertencia", "No se pueden registrar asistencias los fines de semana.", "OK");
                return;
            }
            // Verificar si es antes de las 6 am o después de las 10 pm
            TimeSpan startTime = new TimeSpan(6, 0, 0); // 6 am
            TimeSpan endTime = new TimeSpan(22, 0, 0);  // 10 pm
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            if (currentTime < startTime || currentTime > endTime)
            {
                await DisplayAlert("Advertencia", "Fuera del horario permitido para registrar asistencias.", "OK");
                return;
            }
            try
            {
                entryTime = DateTime.Now;
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    if (IsInsideArea(location)) // Verificar si la ubicación está dentro del área permitida
                    {
                        // Eliminar los pines de entrada y salida existentes
                        if (entryLocationPin != null && exitLocationPin != null)
                        {
                            map.Pins.Remove(entryLocationPin);
                            map.Pins.Remove(exitLocationPin);
                        }
                        // La ubicación está dentro del área permitida, permitir al usuario marcar la entrada
                        Location userLocation = new Location(location.Latitude, location.Longitude);
                        entryLocationPin = new Pin
                        {
                            Type = PinType.Place,
                            Label = $"Entrada: {Name} {LastName}",
                            Position = new Position(location.Latitude, location.Longitude),
                            Address = $"Entrada registrada a las {entryTime.ToString("HH:mm:ss")}"
                        };
                        map.Pins.Add(entryLocationPin);
                        entryTimeEntry.Text = entryTime.ToString("HH:mm:ss");
                        entryDateEntry.Text = entryTime.ToString("dd-MMMM-yyyy");
                        var newMapSpan = MapSpan.FromCenterAndRadius(entryLocationPin.Position, Distance.FromMeters(40));
                        map.MoveToRegion(newMapSpan);
                        entrylongitudeEntry = (decimal)location.Longitude;
                        entrylatitudeEntry = (decimal)location.Latitude;
                        entryLongitudeEntry.Text = $"{entrylongitudeEntry:F6}";
                        entryLatitudeEntry.Text = $"{entrylatitudeEntry:F6}";
                        entryButton.IsEnabled = false;
                        exitButton.IsEnabled = true;
                    }
                    else
                    {
                        // La ubicación está fuera del área permitida, mostrar un mensaje de advertencia
                        await DisplayAlert("Advertencia", "Estás fuera del área permitida para marcar tu asistencia.", "OK");
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
                    exitTime = DateTime.Now;
                    var location = await Geolocation.GetLocationAsync();
                    if (location != null)
                    {
                        Location userLocation = new Location(location.Latitude, location.Longitude);
                        if (exitLocationPin != null)
                        {
                            map.Pins.Remove(exitLocationPin);
                        }
                        exitLocationPin = new Pin
                        {
                            Type = PinType.Place,
                            Label = $"Salida: {Name} {LastName}",
                            Position = new Position(location.Latitude, location.Longitude),
                            Address = $"Salida registrada a las {exitTime.ToString("HH:mm:ss")}"
                        };
                        map.Pins.Add(exitLocationPin);
                        TimeSpan timeDifference = exitTime - entryTime;
                        exitTimeEntry.Text = exitTime.ToString("HH:mm:ss");
                        exitDateEntry.Text = exitTime.ToString("dd-MMMM-yyyy");
                        workTimeEntry.Text = timeDifference.ToString(@"hh\:mm\:ss");

                        // Calcular el punto intermedio entre la entrada y la salida
                        double midLatitude = (entryLocationPin.Position.Latitude + exitLocationPin.Position.Latitude) / 2;
                        double midLongitude = (entryLocationPin.Position.Longitude + exitLocationPin.Position.Longitude) / 2;

                        // Centrar el mapa en el punto intermedio con un zoom que permita ver ambos pines
                        var newMapSpan = MapSpan.FromCenterAndRadius(new Position(midLatitude, midLongitude), Distance.FromKilometers(0.2)); // 200 metros de radio para ver ambos pines
                        map.MoveToRegion(newMapSpan);

                        // Mostrar la ubicación de salida en la interfaz
                        exitlongitudeEntry = (decimal)location.Longitude;
                        exitlatitudeEntry = (decimal)location.Latitude;
                        exitLongitudeEntry.Text = $"{exitlongitudeEntry:F6}";
                        exitLatitudeEntry.Text = $"{exitlatitudeEntry:F6}";
                        entryButton.IsEnabled = true;
                        exitButton.IsEnabled = false;
                        Conexion();

                        if (!IsInsideArea(location))
                        {
                            // Mostrar un mensaje de advertencia si la salida está registrada fuera del área permitida
                            await DisplayAlert("Advertencia", "Tu salida ha sido registrada fuera del área permitida.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo obtener la ubicación actual", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Advertencia", "Antes de continuar, por favor registra tu entrada.", "OK");
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

        private bool IsInsideArea(Location location)
        {
            // Coordenadas de los puntos A, B, C, D, E y F
            Location pointA = new Location(26.029963, -98.277397);
            Location pointB = new Location(26.028999, -98.277300);
            Location pointC = new Location(26.029037, -98.276693);
            Location pointD = new Location(26.026312, -98.276426);
            Location pointE = new Location(26.026433, -98.274688);
            Location pointF = new Location(26.030060, -98.275011);

            // Verificar si la ubicación está dentro del área permitida
            return IsInsideTriangle(location, pointA, pointB, pointC) ||
                   IsInsideTriangle(location, pointA, pointC, pointD) ||
                   IsInsideTriangle(location, pointA, pointD, pointE) ||
                   IsInsideTriangle(location, pointA, pointE, pointF);
        }

        // Método auxiliar para verificar si un punto está dentro de un triángulo
        private bool IsInsideTriangle(Location location, Location point1, Location point2, Location point3)
        {
            double area = 0.5 * (-point2.Longitude * point3.Latitude + point1.Longitude * (-point2.Latitude + point3.Latitude) + point1.Latitude * (point2.Longitude - point3.Longitude) + point2.Latitude * point3.Longitude);
            double s = 1 / (2 * area) * (point1.Longitude * point3.Latitude - point1.Latitude * point3.Longitude + (point3.Longitude - point1.Longitude) * location.Latitude + (point1.Latitude - point3.Latitude) * location.Longitude);
            double t = 1 / (2 * area) * (point1.Latitude * point2.Longitude - point1.Longitude * point2.Latitude + (point1.Longitude - point2.Longitude) * location.Latitude + (point2.Latitude - point1.Latitude) * location.Longitude);

            return s > 0 && t > 0 && 1 - s - t > 0;
        }
    }
}
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Xamarin.Forms;

namespace GeoTimeTrack.Data
{
    public class DatabaseHelper
    {
        private readonly SQLiteAsyncConnection _connection;

        public DatabaseHelper()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TimeEntries.db3");
            _connection = new SQLiteAsyncConnection(databasePath);
            _connection.CreateTableAsync<TimeEntry>().Wait();
        }

        public async Task InsertTimeEntry(TimeEntry entry)
        {
            await _connection.InsertAsync(entry);
        }

        // Aquí podrías agregar más métodos para otras operaciones como actualización, eliminación, consulta, etc.
    }
}
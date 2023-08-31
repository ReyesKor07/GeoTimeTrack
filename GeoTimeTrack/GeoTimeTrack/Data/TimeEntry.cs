using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace GeoTimeTrack.Data
{
    public class TimeEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
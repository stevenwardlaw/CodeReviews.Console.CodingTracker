using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using static System.Collections.Specialized.BitVector32;


namespace codingTracker.stevenwardlaw
{
    internal static class CodingController
    {

        static string CreateConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            return connectionString;
        }

        public static void CreateTable()
        {
            using (var connection = new SqliteConnection(CreateConnectionString()))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS codingTracker (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            StartTime TEXT,
                                            EndTime TEXT,
                                            Duration INTEGER
                                            )";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static int InsertRecord(string _startTime, string _endTime)
        {
            using (var connection = new SqliteConnection(CreateConnectionString()))
            {
                connection.Open();
                string sql = "INSERT INTO codingTracker (StartTime, EndTime, Duration)" +
                    "VALUES (@starttime, @endtime, @duration)";
                // Create object to hold the parameters and assign these from the codingSession instance
                object[] parameters = { new { starttime = _startTime, endtime = _endTime,
                    duration = GetDuration(_startTime, _endTime)} };
                int numRecords = connection.Execute(sql, parameters);
                connection.Close();
                return numRecords;
            }
        }

        public static int UpdateRecord(int _id, string _startTime, string _endTime)
        {
            using (var connection = new SqliteConnection(CreateConnectionString()))
            {
                connection.Open();
                string sql = "UPDATE codingTracker SET StartTime = @starttime, EndTime = @endtime," +
                    "Duration = @duration WHERE Id = @id";
                object[] parameters = { new { id = _id, starttime = _startTime,
                    endtime = _endTime, duration = GetDuration(_startTime, _endTime)} };
                int numRecords = connection.Execute(sql, parameters);
                connection.Close();
                return numRecords;
            }
        }

        public static int DeleteRecord(int _id)
        {
            using (var connection = new SqliteConnection(CreateConnectionString()))
            {
                connection.Open();
                string sql = "DELETE FROM codingTracker WHERE Id = @id";
                int numRecords = connection.Execute(sql, new { id = _id });
                connection.Close();
                return numRecords;
            }
        }

        public static void GetAllRecords()
        {
            using (var connection = new SqliteConnection(CreateConnectionString()))
            {
                connection.Open();
                string sql = "SELECT * from codingTracker";
                List<CodingSession> sessions = connection.Query<CodingSession>(sql).ToList();

                var table = new Table();
                table.AddColumns("Id", "Start Time", "End Time", "Duration (minutes)");

                foreach (CodingSession session in sessions)
                {
                    table.AddRow(session.Id.ToString(), session.StartTime, session.EndTime, session.Duration);
                }

                AnsiConsole.Write(table);
                connection.Close();
            }
        }

        private static int GetDuration(string _startTime, string _endTime)
        {
            DateTime startTime = DateTime.ParseExact(_startTime, "dd-MM-yyyy HH:mm", null);
            DateTime endTime = DateTime.ParseExact(_endTime, "dd-MM-yyyy HH:mm", null);
            TimeSpan duration = endTime - startTime;
            int durationMinutes = (int)duration.TotalMinutes;
            return durationMinutes;
        }
    }
}

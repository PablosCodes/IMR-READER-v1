using IMRReader.Domain.Abstract;
using IMRReader.Domain.Models;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Text;

namespace IMRReader.Infrastructure.FileReaders
{
    public class SQLiteTargetInfoLoader : ITargetInfoLoader
    {
        private const string SELECT_TARGETS_COMMAND = @"SELECT obiekt_id, obiekt_nazwa FROM obiekt;";
        private const string SELECT_MEASUREMENTS_FOR_TARGET_COMMAND = @"SELECT p_id, DATETIME(p_czas, 'unixepoch'), p_metoda, p_koment FROM pomiar where p_obiekt_id=$targetId;";

        private bool isInitialized = false;
        private SqliteConnection? _sqlLiteConnection;
        public string? FilePath { get; private set; }

        public void OpenFile(string filePath)
        {
            FilePath = filePath;
            StringBuilder connectionStringBuilder = new StringBuilder()
                .Append("Data Source=")
                .Append(filePath);
            string connectionString = connectionStringBuilder.ToString();

            _sqlLiteConnection = new SqliteConnection(connectionString);
            _sqlLiteConnection.Open();

            isInitialized = true;
        }

        private void CheckIfInitialized()
        {
            if (!isInitialized)
                throw new Exceptions.ServiceNotInitializedException();
        }

        public IAsyncEnumerable<Measurement> GetMeasurementsForTarget(int targetId)
        {
            CheckIfInitialized();

            var selectCommand = _sqlLiteConnection!.CreateCommand();
            selectCommand.CommandText = SELECT_MEASUREMENTS_FOR_TARGET_COMMAND;
            selectCommand.Parameters.AddWithValue("$targetId", targetId);

            return LoadMeasurements(selectCommand);
        }

        private static async IAsyncEnumerable<Measurement> LoadMeasurements(SqliteCommand sqliteCommand)
        {
            using var sqlReader = sqliteCommand.ExecuteReader();
            while (await sqlReader.ReadAsync())
            {
                Measurement? latestMeasurement = default;
                try
                {
                    latestMeasurement = new()
                    {
                        Id = sqlReader.GetInt32(0),
                        Date = sqlReader.GetDateTime(1),
                        Method = sqlReader.GetString(2),
                        //Results = sqlReader.GetString(3),
                        Results = string.Empty,
                        Comment = sqlReader.GetString(3)
                    };


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                if (latestMeasurement is not null)
                    yield return latestMeasurement;
            }
        }

        public IAsyncEnumerable<Target> GetTargets()
        {
            CheckIfInitialized();

            var selectCommand = _sqlLiteConnection!.CreateCommand();
            selectCommand.CommandText = SELECT_TARGETS_COMMAND;

            return LoadTargets(selectCommand);
        }

        private static async IAsyncEnumerable<Target> LoadTargets(SqliteCommand sqliteCommand)
        {
            using var sqlReader = sqliteCommand.ExecuteReader();
            while (await sqlReader.ReadAsync())
            {
                Target? target = default;
                try
                {
                    target = new()
                    {
                        Id = sqlReader.GetInt32(0),
                        Name = sqlReader.GetString(1)
                    };

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                if (target is not null)
                    yield return target;
            }
        }
    }
}

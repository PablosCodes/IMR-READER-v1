using IMRReader.Domain.Models;
using Microsoft.Data.Sqlite;

namespace IMRReader.Infrastructure.FileReaders.SQLite
{
    public static class SQLiteExtensions
    {
        public static Measurement? ParseRecordAsMeasurement(this SqliteDataReader sqlReader)
        {
            Measurement? parsedMeasurement = new()
            {
                Id = sqlReader.GetInt32(0),
                Date = sqlReader.GetDateTime(1),
                Method = sqlReader.GetString(2),
                Comment = sqlReader.GetString(3),
                Results = sqlReader.GetString(4)
            };

            return parsedMeasurement;
        }

        public static Target? ParseRecordAsTarget(this SqliteDataReader sqlReader)
        {
            Target? target = new()
            {
                Id = sqlReader.GetInt32(0),
                Name = sqlReader.GetString(1)
            };

            return target;
        }
    }
}

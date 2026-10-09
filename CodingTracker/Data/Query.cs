using Dapper;
using Microsoft.Data.Sqlite;
using static CodingTracker.Repository;

namespace CodingTracker;

internal class Query
{
  internal static List<CodingSession> ViewAll()
  {
    using var connection = new SqliteConnection(ConnectionString);
    string sql = @$"SELECT * FROM coding_tracker ORDER BY startTime DESC";

    return connection.Query<CodingSession>(sql).ToList();
  }

  internal static int CountRows()
  {
    using var connection = new SqliteConnection(ConnectionString);
    string sql = @$"SELECT COUNT(*) FROM coding_tracker";

    return Convert.ToInt32(connection.ExecuteScalar(sql));
  }

  internal static List<CodingSession> FilterByActivity(string filter)
  {
    using var connection = new SqliteConnection(ConnectionString);
    string sql = @$"SELECT * FROM coding_tracker WHERE activity LIKE @filter ORDER BY startTime DESC";

    return connection.Query<CodingSession>(sql, new { filter = $"%{filter}%" }).ToList();
  }

  internal static List<CodingSession> FilterByDate(string startDate, string endDate)
  {
    using var connection = new SqliteConnection(ConnectionString);
    string sql = @$"SELECT * FROM coding_tracker 
                    WHERE startTime BETWEEN @startDate AND @endDate
                    ORDER BY startTime DESC";

    return connection.Query<CodingSession>(sql, new { startDate = startDate, endDate = endDate }).ToList();
  }
}
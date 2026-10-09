using Dapper;
using Microsoft.Data.Sqlite;c
using static CodingTracker.Repository;

namespace CodingTracker;

internal class Query
{
  /// <summary>SELECT * FROM coding_tracker ORDER BY startTime DESC</summary><returns></returns>
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

  internal static List<CodingSession> FilterByActivity(string activity)
  {
    using var connection = new SqliteConnection(ConnectionString);
    string sql = @$"SELECT * FROM coding_tracker WHERE activity LIKE '%@activity%' ORDER BY startTime DESC";

    return connection.Query<CodingSession>(sql, new { activity = activity }).ToList();
  }

  internal static List<CodingSession> FilterByDate(string startDate, string endDate)
  {
    using var connection = new SqliteConnection(ConnectionString);
    string sql = @$"SELECT * FROM coding_tracker 
                    WHERE startTime >= startDate AND endTime <= endDate
                    ORDER BY startTime DESC";

    return connection.Query<CodingSession>(sql, new { starDate = startDate, endDate = endDate }).ToList();
  }
}
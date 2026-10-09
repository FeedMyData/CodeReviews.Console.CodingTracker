using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using static CodingTracker.Enums;
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



  // internal static List<CodingSession> ViewSpecificEntry(string startDate, int duration)
  // {
  //   using var connection = new SqliteConnection(ConnectionString);
  //   string sql = @$"SELECT * FROM coding_tracker 
  //                           WHERE startDate = @startDate AND duration = @duration
  //                           LIMIT 1";

  //   var codingSession = connection.Query<CodingSession>(sql, new
  //                         {
  //                           startDate = startDate,
  //                           duration = duration,
  //                         }).ToList();

  //   return codingSession;
  // }
}
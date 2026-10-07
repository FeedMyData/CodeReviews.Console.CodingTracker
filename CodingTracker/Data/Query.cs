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
    string sql = @$"SELECT * FROM coding_tracker";

    var codingSessions = connection.Query<CodingSession>(sql).ToList();

    return codingSessions;
  }
}
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using static CodingTracker.Enums;

namespace CodingTracker;

public static class EnumExtensions
{
  public static string GetDisplayName(this Enum value)
  {
    var member = value.GetType().GetMember(value.ToString())[0];

    return member
        .GetCustomAttribute<DisplayAttribute>()?
        .GetName()
        ?? value.ToString();
  }
}

//   internal static List<string> GetColumnWithoutId()
//   {
//     List<string> choices = new();

//     foreach (string columnName in Enum.GetNames<Column>())
//     {
//       if (columnName != Column.id.ToString())
//         choices.Add(columnName);
//     }
//     return choices;
//   }

//   internal static string DisplayCategories()
//   {
//     return $"{Column.id}\t\t{Column.category}\t\t\t{Column.date}\t{Column.quantity}";
//   }
// }
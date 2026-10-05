using System.ComponentModel.DataAnnotations;

namespace CodingTracker;

internal class Enums
{
  internal enum Column
  {
    [Display(Name = "ID")]
    id,

    [Display(Name = "Activity")]
    activity,

    [Display(Name = "Start time")]
    startTime,

    [Display(Name = "End time")]
    endTime,

    [Display(Name = "Duration")]
    duration
  }

  internal enum MenuItem
  {
    [Display(Name = "View all the coding sessions")]
    viewItems,

    [Display(Name = "Add an entry")]
    addItem,

    [Display(Name = "Delete an entry")]
    deleteItem,

    [Display(Name = "Edit an entry")]
    editItem,

    [Display(Name = "Exit")]
    exit,
  }
}
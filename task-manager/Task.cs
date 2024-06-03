using System;

namespace TaskManager
{
  public class Task
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public TaskStatus Status { get; set; }

    public override string ToString()
    {
      return $"{Title} - {Description} - Status: {Status} - Created at: {CreatedAt} - Updated at: {UpdatedAt}";
    }
  }

  public enum TaskStatus
  {
    Pending, // 0
    InProgress, // 1
    Completed // 2
  }
}
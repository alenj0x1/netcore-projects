using System;
using System.Collections.Generic;
using System.IO;

namespace TaskManager
{
  class Program
  {
    private static List<Task> tasks = new();
    private static string filePath = "tasks.txt";

    static void Main(string[] args)
    {
      LoadTasks();
      ShowMenu();
    }

    static void ShowMenu()
    {
      while (true)
      {
        Console.Clear();
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Task Manager v1");
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Select an option: ");
        Console.WriteLine("| 1. Add task");
        Console.WriteLine("| 2. Edit task");
        Console.WriteLine("| 3. Delete task");
        Console.WriteLine("| 4. Search task");
        Console.WriteLine("| 5. View tasks");
        Console.WriteLine("| 6. Save tasks and exit");
        Console.WriteLine("|------------------------------------|");
        Console.Write("| Your option: ");

        string? userOption = Console.ReadLine();

        switch (userOption)
        {
          case "1":
            AddTask();
            Pause();
            break;
          case "2":
            EditTask();
            Pause();
            break;
          case "3":
            DeleteTask();
            Pause();
            break;
          case "4":
            SearchTask();
            Pause();
            break;
          case "5":
            ViewTasks();
            Pause();
            break;
          case "6":
            SaveTasks();
            Exit();
            break;
          default:
            Console.WriteLine("|------------------------------------|");
            Console.WriteLine("| Invalid option. Press a key to return to the menu.");
            Console.WriteLine("|------------------------------------|");
            Pause();
            break;
        }
      }
    }

    static void Pause()
    {
      Console.ReadKey();
    }

    static void Exit()
    {
      Environment.Exit(0);
    }

    static void LoadTasks()
    {
      if (File.Exists(filePath))
      {
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
          var data = line.Split(';');
          // Enum.TryParse return boolean not exceptions
          if (data.Length == 5 && Enum.TryParse(data[2], out TaskStatus status))
          {
            Task newTask = new()
            {
              Title = data[0],
              Description = data[1],
              Status = status,
              CreatedAt = DateTime.Parse(data[3]),
              UpdatedAt = DateTime.Parse(data[3]),
            };

            tasks.Add(newTask);
          }
        }
      }
    }

    static void SaveTasks()
    {
      using var writer = new StreamWriter(filePath);

      foreach (var task in tasks)
      {
        writer.WriteLine($"{task.Title};{task.Description};{task.Status};{task.CreatedAt};{task.UpdatedAt}");
      }
    }

    static void AddTask()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 1. Add task");
      Console.WriteLine("|------------------------------------|");

      Task newTask = new();

      Console.Write("| Title: ");
      string? newTaskTitle = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(newTaskTitle))
      {
        newTask.Title = newTaskTitle;
      }
      else
      {
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Task title is required. Press a key to return to the menu.");
        Console.WriteLine("|------------------------------------|");
        return;
      }


      Console.Write("| Description: ");
      string? newTaskDescription = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(newTaskDescription))
      {
        newTask.Description = newTaskDescription;
      }
      else
      {
        newTask.Description = "without description.";
      }

      newTask.CreatedAt = DateTime.Now;
      newTask.UpdatedAt = DateTime.Now;
      newTask.Status = TaskStatus.Pending;

      tasks.Add(newTask);

      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| Task added. Press a key to return to the menu.");
      Console.WriteLine("|------------------------------------|");
    }

    static void EditTask()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 2. Edit task");
      Console.WriteLine("|------------------------------------|");
      Console.Write("| Enter the title of the task to edit: ");

      string? userInput = Console.ReadLine();
      Task? taskToEdit = tasks.Where(t => t.Title.Equals(userInput, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

      if (taskToEdit != null)
      {
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine($"| Editing {taskToEdit.Title}");
        Console.WriteLine("| Leave the field blank to keep the current value.");
        Console.WriteLine("| Current data: ");
        Console.WriteLine($"| {taskToEdit}");
        Console.WriteLine("|------------------------------------|");

        Console.Write("| Title: ");
        string? newTitle = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newTitle))
        {
          taskToEdit.Title = newTitle;
        }

        Console.Write("| Description: ");
        string? newDescription = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDescription))
        {
          taskToEdit.Description = newDescription;
        }

        Console.Write("| Status (Pending, InProgress, Completed): ");
        string? newStatusStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newStatusStr) && Enum.TryParse(newStatusStr, true, out TaskStatus newStatus))
        {
          taskToEdit.Status = newStatus;
        }

        taskToEdit.UpdatedAt = DateTime.Now;

        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Task updated. Press a key to return to the menu.");
        Console.WriteLine("|------------------------------------|");
      }
      else
      {
        Console.WriteLine($"| Task '{userInput}' not found. Press a key to return to the menu.");
        Console.WriteLine("|------------------------------------|");
      }
    }

    static void DeleteTask()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 3. Delete task");
      Console.WriteLine("|------------------------------------|");
      Console.Write("| Enter the title of the task to delete: ");

      string? userInput = Console.ReadLine();
      Task? taskToDelete = tasks.FirstOrDefault(t => t.Title.Equals(userInput, StringComparison.OrdinalIgnoreCase));

      if (taskToDelete != null)
      {
        tasks.Remove(taskToDelete);
        Console.WriteLine($"| Task '{taskToDelete.Title}' deleted. Press a key to return to the menu.");
      }
      else
      {
        Console.WriteLine($"| Task '{userInput}' not found. Press a key to return to the menu.");
      }

      Console.WriteLine("|------------------------------------|");
    }

    static void SearchTask()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 4. Search task");
      Console.WriteLine("|------------------------------------|");
      Console.Write("| Enter the title of the task to search: ");

      string? userInput = Console.ReadLine();
      // Using equals and StringComparison
      Task? foundTask = tasks.FirstOrDefault(t => t.Title.Equals(userInput, StringComparison.OrdinalIgnoreCase));

      if (foundTask != null)
      {
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("| Task found:");
        Console.WriteLine($"| {foundTask}");
        Console.WriteLine("| Press a key to continue.");
      }
      else
      {
        Console.WriteLine("| Task not found. Press a key to return to the menu.");
      }

      Console.WriteLine("|------------------------------------|");
    }

    static void ViewTasks()
    {
      Console.WriteLine("|------------------------------------|");
      Console.WriteLine("| 5. View tasks");
      Console.WriteLine("|------------------------------------|");

      if (tasks.Count > 0)
      {
        for (int i = 0; i < tasks.Count; i++)
        {
          Console.WriteLine($"| {i + 1}. {tasks[i]}");
        }

        Console.WriteLine("|------------------------------------|");
      }
      else
      {
        Console.WriteLine("| Without tasks, create one.");
      }

      Console.WriteLine("| Press a key to return to the menu.");
      Console.WriteLine("|------------------------------------|");
    }
  }
}

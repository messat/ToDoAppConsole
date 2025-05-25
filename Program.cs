using System;
using System.Collections.Generic;
using System.IO;


namespace ToDoAppTaskTracker
{
  class Program
  {
    static void Main(string[] args)
    {
      GreetUser();
      FileCreation();

      List<string> userList = new List<string>();

      UserEntry();
      UserSwitch(userList);

    }

    static void UserSwitch(List<string> userList)
    {
      string? input = Console.ReadLine();
      char userRequest = !string.IsNullOrEmpty(input) ? input.Trim()[0] : ' ';
      if (userRequest != 'q')
      {
        switch (userRequest)
        {
          case 'V':
          case 'v':
            ViewTaskBoard(userList);
            break;
          case 'A':
          case 'a':
            AddTaskBoard(userList);
            break;
          case 'D':
          case 'd':
            DeleteTaskBoard(userList);
            break;
          case 'U':
          case 'u':
            UpdateTaskBoard(userList);
            break;
          default:
            Console.WriteLine("Try Again. Use the entries below only.".ToUpper() + "\n");
            UserEntry();
            UserSwitch(userList);
            break;
        }
      }
      else
      {
        Console.WriteLine("Exited the app");
      }

    }

    static void UserEntry()
    {
      Console.WriteLine("To Add a task - type \"A\" or \"a\"");
      Console.WriteLine("To View all your task - type \"V\" or \"v\"");
      Console.WriteLine("To Delete a task, type \"D\" or \"d\"");
      Console.WriteLine("To Update a task, type \"U\" or \"u\"");
      Console.WriteLine();
      Console.WriteLine("Enter \"Q\" or \"q\" to exit the app");
      Console.WriteLine();
    }

    static void AddTaskBoard(List<string> userList)
    {
      Console.WriteLine("Enter a new task:");
      string? newTask = Console.ReadLine();
      if (!string.IsNullOrWhiteSpace(newTask))
      {
        userList.Add(newTask);
        Console.WriteLine($"Added \"{newTask}\" as a task");
        Console.WriteLine();

        using (StreamWriter fileAppendTask = File.AppendText(@"TaskMemory.txt"))
        {
          int taskListCount = userList.Count;
          fileAppendTask.WriteLine($"{taskListCount}. {newTask}");
        }
        AllTasks();
        UserEntry();
        UserSwitch(userList);
      }
    }



    static void DeleteTaskBoard(List<string> userList)
    {
      AllTasks();
      Console.WriteLine("Delete task number:");

      try
      {
        int taskNum = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"Task Deleted: {taskNum}. {userList[taskNum - 1]}");
        Console.WriteLine();
        userList.RemoveAt(taskNum - 1);
        using (StreamWriter deleteTask = new StreamWriter(@"TaskMemory.txt"))
        {
          DateTime localDate = DateTime.Now;
          deleteTask.WriteLine($"Tasks for today: {localDate.ToString("D")} \n");
          for (int i = 0; i < userList.Count; i++)
          {
            deleteTask.WriteLine($"{i + 1}. {userList[i]}");
          }
        }
        
        
      }
      catch (ArgumentOutOfRangeException err)
      {
        Console.WriteLine(err.Message + "\n");
        DeleteTaskBoard(userList);
      }
      catch (FormatException formatErr)
      {
        Console.WriteLine(formatErr.Message + "\n");
        DeleteTaskBoard(userList);
      }
      AllTasks();
      UserEntry();
      UserSwitch(userList);
    }

    static void ViewTaskBoard(List<string> userList)
    {
      Console.Clear();
      Console.WriteLine("View All Tasks: \n");

      AllTasks();

      UserEntry();
      UserSwitch(userList);
    }

    static void GreetUser()
    {
      Console.WriteLine("Welcome to Mo To Do App Tracker!".ToUpper());
      Console.WriteLine();
    }

    static void UpdateTaskBoard(List<string> userList)
    {
      AllTasks();
      Console.WriteLine("Enter the numbered task to update:");
      try
      {
        int taskNumUpdate = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"Task to Update: {taskNumUpdate}. {userList[taskNumUpdate - 1]}");
        Console.WriteLine();
        userList.RemoveAt(taskNumUpdate - 1);
        string? updateTask = Console.ReadLine();
        if (updateTask is not null)
        {
          userList.Insert(taskNumUpdate - 1, updateTask);
        }
        Console.WriteLine();
        using (StreamWriter updateTaskFile = new StreamWriter(@"TaskMemory.txt"))
        {
          DateTime localDate = DateTime.Now;
          updateTaskFile.WriteLine($"Tasks for today: {localDate.ToString("D")} \n");

          for (int i = 0; i < userList.Count; i++)
          {
            updateTaskFile.WriteLine($"{i + 1}. {userList[i]}");
          }
        }
        AllTasks();
        UserEntry();
        UserSwitch(userList);
      }
      catch (ArgumentOutOfRangeException rangeErr)
      {
        Console.WriteLine(rangeErr.Message);
        Console.WriteLine();
        UpdateTaskBoard(userList);
      }
      catch (FormatException formatErr)
      {
        Console.WriteLine(formatErr.Message);
        Console.WriteLine();
        UpdateTaskBoard(userList);
      }
      catch (ArgumentNullException nullError)
      {
        Console.WriteLine(nullError.Message);
        Console.WriteLine();
        UpdateTaskBoard(userList);
      }
    }

    static void AllTasks()
    {
      string? taskDuty;
      try
      {
        StreamReader viewAllTaskInList = new StreamReader("/Users/muhammadessat/Desktop/C#/practice/To do app /To Do App/TaskMemory.txt");
        taskDuty = viewAllTaskInList.ReadLine();
        while (taskDuty != null)
        {
          Console.WriteLine(taskDuty);
          taskDuty = viewAllTaskInList.ReadLine();
        }
          viewAllTaskInList.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      }
      Console.WriteLine();
    }


    static void FileCreation()
    {
      using (StreamWriter writeTasks = new StreamWriter(@"TaskMemory.txt"))
      {
        DateTime localDate = DateTime.Now;
        writeTasks.WriteLine($"Tasks for today: {localDate.ToString("D")} \n");
      }
    }
  }
}
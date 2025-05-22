using System;
using System.Collections.Generic;
using System.IO;


namespace ToDoAppTaskTracker
{
  class Program
  {
    static void Main(string[] args)
    {
      greetUser();

      List<string> userList = new List<string> { "Set up treadmill", "Clean the bedroom" };

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
        AllTasks(userList);
        UserEntry();
        UserSwitch(userList);
      }
    }



    static void DeleteTaskBoard(List<string> userList)
    {
      AllTasks(userList);
      Console.WriteLine("Delete task number:");

      try
      {
        int taskNum = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"Task Deleted: {taskNum}. {userList[taskNum - 1]}");
        Console.WriteLine();
        userList.RemoveAt(taskNum - 1);
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
      AllTasks(userList);
      UserEntry();
      UserSwitch(userList);
    }

    static void ViewTaskBoard(List<string> userList)
    {
      Console.Clear();
      Console.WriteLine("View All Tasks: \n");

      AllTasks(userList);

      UserEntry();
      UserSwitch(userList);
    }

    static void greetUser()
    {
      Console.WriteLine("Welcome to Mo To Do App Tracker!".ToUpper());
      Console.WriteLine();
    }

    static void UpdateTaskBoard(List<string> userList)
    {
      AllTasks(userList);
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
        AllTasks(userList);
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

    static void AllTasks(List<string> userList)
    {
      try
      {
        using (StreamWriter writeTasks = new StreamWriter(@"TaskMemory.txt"))
        {
          DateTime localDate = DateTime.Now;
          writeTasks.WriteLine($"Tasks for today: {localDate} \n");
          for (int i = 0; i < userList.Count; i++)
          {
            Console.WriteLine($"{i + 1}. {userList[i]}");
            writeTasks.WriteLine($"{i + 1}. {userList[i]}");
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      Console.WriteLine();
    }
  }
}
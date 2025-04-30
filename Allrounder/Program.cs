using Allrounder;

internal class Program
{
  public static Dictionary<string, Action<string[]>> Commands = new Dictionary<string, Action<string[]>>();
  public static Dictionary<string, string> CommandDescriptions = new Dictionary<string, string>();

  private static void Main(string[] args)
  {

    FillDictionarys();

    if (args.Length > 0 && Commands.TryGetValue(args[0].ToLower(), out var action) && action != null)
    {
      action([.. args.Skip(1)]);
    }
    else
    {
      HelpCommand(args);
      Console.WriteLine($"<=== Press any key to manually enter arguments ... ===>");
      Console.ReadKey();
      ManualUserInput();
    }
  }

  private static void ManualUserInput()
  {
    string[] cmdArgs = new string[3];

    Console.WriteLine($"<=== MANUAL MODE ===>");
    Console.WriteLine($"<=== please enter argument 1! ===>");
    ReadUserArgument(cmdArgs, 0);

    if (cmdArgs[0] == "help")
    {
      HelpCommand(cmdArgs);
      Console.WriteLine($"<=== Do you want to manually enter again? ===>");
    }
    else
    {
      Console.WriteLine($"<=== please enter argument 2! ===>");
      ReadUserArgument(cmdArgs, 1);

      if (cmdArgs.Length > 1)
      {
        Console.WriteLine($"<=== Arguemnts have been passed successfully ===>");
        Console.WriteLine($"<=== continue? ... ===>");
        Console.ReadKey();
        if (cmdArgs.Length > 0 && Commands.TryGetValue(cmdArgs[0].ToLower(), out var action))
        {
          action([.. cmdArgs.Skip(1)]);
        }
        else
        {
          Console.WriteLine($"<=== welp you got it wrong ... ===>");
          HelpCommand(cmdArgs);
          Console.WriteLine($"<=== Press any key to manually enter arguments ... ===>");
          Console.ReadKey();
        }
      }
    }
  }

  private static void FillDictionarys()
  {
    Commands["rank"] = SearchWavuRank;
    CommandDescriptions["rank"] = "Searches for a payers profile on wank.wavu. \n" +
        "Usage: \n" +
        "rank <STEAM_URL> or \n" +
        "rank <TEKKEN_ID> with or without '-'";

    Commands["help"] = HelpCommand;
    CommandDescriptions["help"] = "Displays this message!";
  }

  private static void ReadUserArgument(string[] cmdArgs, int index)
  {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string input = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    if (input != null)
    {
      Console.WriteLine($"<=== your Input: {input} as Argument {index} ===>");
      cmdArgs[index] = input;
    }
  }

  static void SearchWavuRank(string[] cmgArgs)
  {
    if (!string.IsNullOrWhiteSpace(cmgArgs[0]))
    {
      _ = new WavuRank(cmgArgs[0]);
    }
    else
    {
      Console.WriteLine("Usage: rank <STEAM_URL> or rank <TEKKEN_ID>");
    }
  }

  static void HelpCommand(string[] args)
  {
    Console.WriteLine("<=== Available commands: ===>");
    foreach (var command in Commands)
    {
      if (CommandDescriptions.TryGetValue(command.Key, out var description))
      {
        Console.WriteLine($"  {command.Key,-15} - {description}");
      }
      else
      {
        Console.WriteLine($"  {command.Key,-15}");
      }
      Console.WriteLine("<=== - - - - - - -  ===>");
    }
  }
}
using System.Text.RegularExpressions;
namespace Allrounder
{
  internal class WavuRank
  {
    private string _input = "";
    private const string _WAVUURI = "https://wank.wavu.wiki/player/";
    private const string _WAVUSEARCHURI = "https://wank.wavu.wiki/player/search?q=";
    private const string _EWGFGGSEARCHURI = "https://ewgf.gg/api/search?query=";
    private const string _EWGFGGURI = "https://ewgf.gg/player/";

    private readonly string _inputPattern = "^[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}$";
    private readonly string _inputPatternShort = "^[a-zA-Z0-9]{12}$";
    private readonly string _steamIdPatter = @"/profiles/(\d+)/?$";

    public WavuRank(string incomingArgument)
    {
      if (!string.IsNullOrWhiteSpace(incomingArgument))
      {
        _input = incomingArgument;

        if (_input.StartsWith("https://steamcommunity.com/profiles"))
        {
          Console.WriteLine($"<=== Steam Profile URL found! ===>");
          string extractedId = GetSteamIdFromURL(_input);
          Helper.OpenUrlInBrowser(_WAVUSEARCHURI + extractedId);
        }
        else if (Regex.IsMatch(_input, _inputPattern) || Regex.IsMatch(_input, _inputPatternShort))
        {
          Console.WriteLine($"<=== Tekken-ID found! ===>");
          ChoseRankedSite();
        }
        else
        {
          Console.WriteLine($"<=== your input was not a valid use ===>\n" +
              $"  rank <STEAM_URL> \n" +
              $"  or\n" +
              $"  rank <TEKKEN_ID>!");
          Console.WriteLine($"<=== Press to exit! ===>");
          Console.ReadKey();
        }
      }
    }

    private void ChoseRankedSite()
    {
      Console.WriteLine($"<=== wank.wavu = 0 | EWGF.gg = 1 ===>");
      string id;
      if (_input.Length > 12)
      {
        id = _input.Replace("-", "");
      }
      else
      {
        id = _input;
      }

      string targetUrl = _WAVUURI + id;

      var userInput = Console.ReadLine();
      if (userInput != null)
      {
        switch (userInput)
        {
          case "0":
            break;
          case "1":
            targetUrl = _EWGFGGURI + id;
            break;
          default:
            Console.WriteLine($"<=== Unable to write ??? just taking wavu ===>");
            break;
        }
      }
      else
      {
        Console.WriteLine($"<=== Unable to write ??? just taking wavu ===>");
      }
      Helper.OpenUrlInBrowser(targetUrl);
    }

    private string GetSteamIdFromURL(string steamUrl)
    {
      Match regexMatch = Regex.Match(steamUrl, _steamIdPatter);
      if (regexMatch.Success)
      {
        Console.WriteLine($"<=== Steam ID Identified: {regexMatch.Groups[1].Value} ===>");
        return regexMatch.Groups[1].Value;
      }
      Console.WriteLine($"<=== No Steam ID in given URL! ===>");
      return "";
    }
  }
}


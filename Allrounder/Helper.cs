using System.Diagnostics;

namespace Allrounder
{
  /// <summary>
  /// Helper Class for static methods
  /// </summary>
  public static class Helper
  {
    public static void OpenUrlInBrowser(string url)
    {
      try
      {
        ProcessStartInfo psi = new()
        {
          FileName = url,
          UseShellExecute = true,
        };
        Process.Start(psi);
        Console.WriteLine($"<=== Standart Browser Opened ===>");
        Console.WriteLine($"<=== {url} ===>");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"<=== ERROR WHILE OPENING THE BROWSER ===>");
        Console.WriteLine($"<=== {ex} ===>");
      }
    }
  }
}

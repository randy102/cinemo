using System;

namespace Cinemo.Utils {
  public class DateTimeUtils {
    static string FORMAT = "HH:mm - dd/MM/yyyy";

    public static DateTime Parse(string str) {
      return DateTime.ParseExact(str, FORMAT, null);
    }

  }
}
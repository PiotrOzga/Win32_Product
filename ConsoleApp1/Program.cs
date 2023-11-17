using System;
using System.IO;
using System.Management;
using System.Text;

namespace Win32_Product {
  class Program {
    private static readonly string _productName = "{0} {1}";
    static void Main(string[] args) {
      if (args.Length == 0) {
        Console.WriteLine("Invalid args");
        return;
      }
      var command = args[0];

      switch (command) {
        case "screen":
        case "file":
          GetProducts(command);
          break;
        default:
          Console.WriteLine("Invalid arg.");
          break;
      }
    }

    private static void GetProducts(string arg) {
      Console.WriteLine("Please wait...");
      StreamWriter StrWrriter = new StreamWriter(string.Format("Win32_Products_{0}.txt", DateTime.Now.ToString("yyyyMMdd")), false, Encoding.UTF8);
      ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Product");      
      foreach (ManagementObject mo in mos.Get()) {
        switch (arg) {
          case "screen":
            Console.WriteLine(string.Format(_productName, mo["Name"], mo["Version"]));
            break;
          case "file":
            StrWrriter.WriteLine(string.Format(_productName, mo["Name"], mo["Version"]));
            break;
        }
      }
      Console.WriteLine("Done");
    }
  }
}

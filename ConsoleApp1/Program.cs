using System;
using System.IO;
using System.Management;
using System.Text;

namespace Win32_Product {
  class Program {
    private static readonly string _prdName = "{0} {1}";
    private static readonly string _fnFrmt = "{0}_{1}.txt";
    private static readonly string _prdType = "Win32_Product";
    private static readonly string _errorArg = "Invalid args";
    private static string _qry {
      get {
        return string.Format("SELECT * FROM {0}", _prdType);
      }
    }
    private static readonly string _dtFrmt = "yyyyMMdd";

    static void Main(string[] args) {
      if (args.Length == 0) {
        Console.WriteLine(_errorArg);
        return;
      }
      var command = args[0];
      switch (command) {
        case "screen":
        case "file":
          GetProducts(command);
          break;
        default:
          Console.WriteLine(_errorArg);
          break;
      }
    }

    private static void GetProducts(string arg) {
      Console.WriteLine("Please wait...");
      try {
        StreamWriter StrWriter = new StreamWriter(string.Format(_fnFrmt, _prdType, DateTime.Now.ToString(_dtFrmt)), false, Encoding.UTF8);
        ManagementObjectSearcher mos = new ManagementObjectSearcher(_qry);      
        foreach (ManagementObject mo in mos.Get()) {
          switch (arg) {
            case "screen":
              Console.WriteLine(string.Format(_prdName, mo["Name"], mo["Version"]));
              break;
            case "file":
              StrWriter.WriteLine(string.Format(_prdName, mo["Name"], mo["Version"]));
              break;
          }
        }
        Console.WriteLine("Done");
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
      } finnaly {
        StrWriter.Close();
      }
    }
  }
}

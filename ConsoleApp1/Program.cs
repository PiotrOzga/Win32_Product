using System;
using System.Management;

namespace Win32_Product {
  class Program {
    static void Main(string[] args) {
      int i = 0;
      ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
      foreach (ManagementObject mo in mos.Get()) {
        Console.WriteLine(mo["Name"] + " " + mo["Version"]);
        i++;
      }
      Console.WriteLine(string.Format("Installed software count: {0}", i));
      Console.ReadKey();
    }
  }
}

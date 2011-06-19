using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   sealed class DebugConsole
   {
      public static readonly DebugConsole Instance = new DebugConsole();

      private DebugConsoleForm f = new DebugConsoleForm();

      public void Add(string str)
      {
         f.Add(str);
      }

      public void AddLn(string str)
      {
         f.Add(str + "\r\n");
      }

      public void Show()
      {
         f.Show();
      }

      public void Hide()
      {
         f.Hide();
      }

   }
}

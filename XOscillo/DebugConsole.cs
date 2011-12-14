using System;
using System.Collections.Generic;
using System.Text;

namespace XOscillo
{
   sealed class DebugConsole
   {
      public static readonly DebugConsole Instance = new DebugConsole();

      private DebugConsoleForm f = new DebugConsoleForm();

      bool loggingEnabled = true;

      public void EnableLogging(bool status)
      {
         loggingEnabled = status;
      }

      public void Add(string str)
      {
         if (loggingEnabled)
         {
            f.Add(str);
         }
      }

      public void AddLn(string str)
      {
         Add(str + "\r\n");
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

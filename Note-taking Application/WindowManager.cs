using Note_taking_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Note_taking_Application
{
    public class WindowManager
    {
        public static List<Window> WindowStack = new(); 

        public static void CloseAllWindows()
        {
            foreach (Window window in WindowStack)
            {
                window.Close();
            }

            WindowStack.Clear();
        }
        
    }
}

using Note_taking_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Note_taking_Application
{
    /// <summary>
    /// A generic Dialog Launcher that we can re-use to create new windows with different content.
    /// </summary>
    /// <typeparam name="T">The content we want to display in this window.</typeparam>
    public class DialogLauncher<T, VM> where T : class, new()
    {
        public Window Window = new Window();
        public ScrollViewer ScrollViewer;

        public DialogResult DialogResult;

        public T? Control => ScrollViewer.Content as T;
        public DialogLauncher(object? owner, ResizeMode resizeMode = ResizeMode.CanResize)
        {
            Window.WindowStyle = WindowStyle.None;
            Window.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Window.AllowsTransparency = true;
            Window.UseLayoutRounding = true;
            Window.SnapsToDevicePixels = true;
            Window.ResizeMode = resizeMode;
            if (owner is Window windowOwner)
                Window.Owner = windowOwner;
            else
            {
                Window.Owner = WindowManager.MainWindow;
            }

            ScrollViewer = new ScrollViewer()
            {
                Name = "scrollViewerContainer",
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = new T()
            };

            Window.Title = ScrollViewer.Content.GetType().ToString();
            Window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Window.Content = ScrollViewer;
            Window.SizeToContent = SizeToContent.WidthAndHeight;
            Window.ShowInTaskbar = false;

            if (ScrollViewer.Content is IDialogClient<VM> dialogClient)
            {
                Window.Activated += dialogClient.DialogClient_Activated;
                Window.Deactivated += dialogClient.DialogClient_Deactivated;
            }
        }

        public void Show()
        {
            Window.Show();
        }
    }

 
}

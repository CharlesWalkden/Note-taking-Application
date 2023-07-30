using Note_taking_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Note_taking_Application
{
    /// <summary>
    /// A generic Dialog Launcher that we can re-use to create new windows with different content.
    /// </summary>
    /// <typeparam name="T">The content we want to display in this window.</typeparam>
    public class DialogLauncher<T, VM> where T : class, new()
    {
        public EventHandler<DialogEventArgs>? OnClose;

        public Window Window = new Window();
        public ScrollViewer ScrollViewer;

        public DialogResult DialogResult;

        public T? Control => ScrollViewer.Content as T;
        public DialogLauncher(object? owner, ResizeMode resizeMode = ResizeMode.CanResize)
        {
            Window.WindowStyle = WindowStyle.ToolWindow;
            Window.UseLayoutRounding = true;
            Window.SnapsToDevicePixels = true;
            Window.ResizeMode = resizeMode;
            Window.Closed += Window_Closed;
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
                dialogClient.OnClose += new EventHandler<DialogEventArgs>(DialogClient_Close);
            }
        }

        public void Show()
        {
            Window.Show();
        }

        public void Close()
        {
            OnClose?.Invoke(this, new DialogEventArgs() { Result = DialogResult });
        }

        private void DialogClient_Close(object? sender, DialogEventArgs e)
        {
            DialogResult = e.Result;
            Close();

        }
        private void Window_Closed(object? sender, EventArgs e)
        {
            // If the result is none, this is the default and means the user has closed it with the X
            if (DialogResult == DialogResult.None)
            {
                Close();
            }
        }
    }

    public class DialogEventArgs : EventArgs
    {
        public DialogEventArgs()
        {
        }
        public DialogEventArgs(DialogResult result)
        {
            Result = result;
        }
        public DialogResult Result;
        
    }
}

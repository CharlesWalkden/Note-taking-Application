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
    public class DialogLauncher<T> where T : class, new()
    {
        public EventHandler<DialogEventArgs>? OnClose;

        public Window Window = new Window();
        public ScrollViewer? ScrollViewer = null;

        public DialogResult DialogResult;
        public DialogLauncher(object owner, ResizeMode resizeMode)
        {
            Window.UseLayoutRounding = true;
            Window.SnapsToDevicePixels = true;
            Window.ResizeMode = resizeMode;
            Window.Closed += Window_Closed;
            if (owner is Window windowOwner)
                Window.Owner = windowOwner;

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

            if (ScrollViewer.Content is IDialogClient dialogClient)
            {
                dialogClient.OnClose += new EventHandler<DialogEventArgs>(DialogClient_Close);
            }
        }

        public void Show()
        {
            Window.Show();
        }

        public void Close()
        {
            if (DialogResult != DialogResult.None)
            {
                if (Window.DialogResult == null)
                {
                    Window.DialogResult = false;
                }
            }

            try
            {
                Window.Close();
            }
            catch(Exception e)
            {

            }

            OnClose?.Invoke(this, new DialogEventArgs() { Result = DialogResult });
        }

        private void DialogClient_Close(object? sender, DialogEventArgs e)
        {
            DialogResult = e.Result;
            Close();

        }
        private void Window_Closed(object? sender, EventArgs e)
        {
            // If the result is none, this is the default and means we have not closed it, the user has with the X
            if (DialogResult == DialogResult.None)
            {
                OnClose?.Invoke(this, new DialogEventArgs() { Result = DialogResult });
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
        public bool AffirmativeResponse
        {
            get
            {
                bool res = false;
                if (Result == DialogResult.Yes || Result == DialogResult.OK || Result == DialogResult.Accept)
                {
                    res = true;
                }
                return res;
            }
        }
    }
}

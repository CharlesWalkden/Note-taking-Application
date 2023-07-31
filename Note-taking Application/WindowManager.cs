using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.UserControls;
using Note_taking_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static Window MainWindow = Application.Current.MainWindow;

        public static IDialogClient<VM>? CreateWindow<T, VM>(object? owner = null)
            where T : class, new()
        {
            DialogLauncher<T, VM> newWindow = new(owner, ResizeMode.CanResize);

            WindowStack.Add(newWindow.Window);
            return newWindow?.Control as IDialogClient<VM>;
        }
        public static void OpenWindow<T>(IDialogClient<T> dialogClient)
        {
            Window? window = FindWindow(dialogClient);
            if (window != null)
            {
                window.Show();
            }
        }
        public static void CloseAllWindows()
        {
            foreach (Window window in WindowStack)
            {
                window.Close();
            }

            WindowStack.Clear();
        }
        public static void CloseWindow(Note note)
        {
            Window? toClose = FindWindow(note);

            if (toClose != null)
            {
                toClose.Close();
                WindowStack.Remove(toClose);
            }
        }
        public static void CloseWindow<T>(IDialogClient<T> dialogClient)
        {
            Window? toClose = FindWindow(dialogClient);

            if (toClose != null)
            {
                toClose.Close();
                WindowStack.Remove(toClose);
            }
        }
        public static void FocusWindow<T>(IDialogClient<T> dialogClient)
        {
            Window? toFocus = FindWindow(dialogClient);
            toFocus?.Focus();
            toFocus?.BringIntoView();
        }

        private static Window? FindWindow<T>(IDialogClient<T> client)
        {
            Window? foundWindow = null; 
            foreach (Window window in WindowStack)
            {
                if (window.Content is ScrollViewer scrollViewer)
                {
                    if (scrollViewer.Content is IDialogClient<T> content)
                    {
                        if (client == content)
                        {
                            foundWindow = window;
                            break;
                        }
                    }
                }
            }

            return foundWindow;
        }
        /// <summary>
        /// Finds the windows for a specific note.
        /// </summary>
        /// <param name="note">The note you want to find the window for.</param>
        /// <returns>The window for this note.</returns>
        private static Window? FindWindow(Note note)
        {
            Window? foundWindow = null;
            foreach (Window window in WindowStack)
            {
                if (window.Content is ScrollViewer scrollViewer)
                {
                    if (scrollViewer.Content is IDialogClient<NotesPageViewModel> content)
                    {
                        if (note == content.ViewModel?.NoteModel)
                        {
                            foundWindow = window;
                            break;
                        }
                    }
                }
            }

            return foundWindow;
        }

    }
}

using Note_taking_Application.Event_Args;
using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;

namespace Note_taking_Application.UserControls
{
    /// <summary>
    /// Interaction logic for NotesPage.xaml
    /// </summary>
    public partial class NotesPage : UserControl, IDialogClient<NotesPageViewModel>
    {
        #region Public Properties

        public NotesPageViewModel? ViewModel
        {
            get => DataContext as NotesPageViewModel;

            set => DataContext = value;
        }

        #endregion

        #region Constructor

        public NotesPage()
        {
            InitializeComponent();
            DataContext = new NotesPageViewModel(new Note());

        }

        #endregion

        #region Interface

        public event EventHandler<NotesPageActionRequestEventArgs>? OnClose;

        public void DialogClient_Activated(object? sender, EventArgs e) 
        {
            banner.Height = 30;
            bannerActions.Visibility = Visibility.Visible;
        }

        public void DialogClient_Deactivated(object? sender, EventArgs e)
        {
            banner.Height = 10;
            bannerActions.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Events

        
        private void closeNoteButton_Click(object sender, RoutedEventArgs e)
        {
            OnClose?.Invoke(sender, new NotesPageActionRequestEventArgs() { Note = ViewModel.NoteModel, Requester = this });
        }

        #endregion

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage (IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void banner_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) 
        {
            Window? window = FindWindow(this);

            if (window != null)
            {
                WindowInteropHelper helper = new WindowInteropHelper(window);
                SendMessage(helper.Handle, 161, 2, 0);
            }
        }

        private Window? FindWindow(NotesPage notesPage)
        {
            Window? foundWindow = null;
            if (notesPage.Parent is ScrollViewer scrollViewer)
            {
                if (scrollViewer.Parent is Window window)
                {
                    foundWindow = window;
                }
            }

            return foundWindow;
        }

        private void banner_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            new ResizeAdorner(notes, 200, 350);
        }
    }
    
}

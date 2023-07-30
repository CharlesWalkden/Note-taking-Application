using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

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

        public event EventHandler<DialogEventArgs>? OnClose;

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
            OnClose?.Invoke(sender, new DialogEventArgs() { Result = DialogResult.CloseAndSave });
        }

        #endregion
    }
}

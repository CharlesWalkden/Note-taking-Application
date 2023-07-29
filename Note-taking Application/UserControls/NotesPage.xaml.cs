using Note_taking_Application.Interfaces;
using Note_taking_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Note_taking_Application.UserControls
{
    /// <summary>
    /// Interaction logic for NotesPage.xaml
    /// </summary>
    public partial class NotesPage : UserControl, IDialogClient
    {
        #region Public Properties

        public NotesPageViewModel? ViewModel { get => DataContext as NotesPageViewModel; }

        #endregion

        #region Constructor

        public NotesPage()
        {
            DataContext = new NotesPageViewModel();
            InitializeComponent();
        }
        public NotesPage(NotesPageViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        #endregion

        #region Interface

        public event EventHandler<DialogEventArgs>? OnClose;

        #endregion
    }
}

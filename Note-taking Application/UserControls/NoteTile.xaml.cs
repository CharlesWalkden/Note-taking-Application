using Note_taking_Application.Models;
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
    /// Interaction logic for NoteTile.xaml
    /// </summary>
    public partial class NoteTile : UserControl
    {
        private NoteTileViewModel ViewModel => DataContext as NoteTileViewModel;
        public NoteTile()
        {
            InitializeComponent();
        }

        #region Dependency Property
        public Note NoteModel
        {
            get => (Note)GetValue(NoteModelProperty);
            set
            {
                SetValue(NoteModelProperty, value);
            }
        }

        // Using a DependencyProperty for NoteModel
        public static readonly DependencyProperty NoteModelProperty =
            DependencyProperty.Register(nameof(NoteModel), typeof(Note), typeof(NoteTile), new FrameworkPropertyMetadata(default(Note), null, CurrentNotePropertyChanged));

        private static object CurrentNotePropertyChanged(DependencyObject d, object value) 
        {
            NoteTile noteTile = (NoteTile)d;
            Note note = (Note)value;

            if (noteTile.DataContext is NoteTileViewModel viewModel)
            {
                // do nothing
            }
            else
            {
                NoteTileViewModel noteTileViewModel = new NoteTileViewModel();
                noteTileViewModel.NoteModel = note;
                noteTile.DataContext = noteTileViewModel;
            }

            return value;
        }

        #endregion

        #region Context Menu

        private void Border_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ContextMenu contextMenu = new ContextMenu();

            MenuItem item;
            if (ViewModel.NoteModel.IsOpen)
            {
                item = new MenuItem() { Header = "Close Note" };
                item.Click += Item_Click;
                contextMenu.Items.Add(item);
            }
            else
            {
                item = new MenuItem() { Header = "Open Note" };
                item.Click += Item_Click;
                contextMenu.Items.Add(item);
            }

            item = new MenuItem() { Header = "Delete Note" };
            item.Click += Item_Click;
            contextMenu.Items.Add(item);

            if (contextMenu.Items.Count > 0)
                contextMenu.IsOpen = true;

            root.ContextMenu = contextMenu;
            
            e.Handled = true;
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            switch (((MenuItem)sender).Header)
            {
                case "Open Note":
                    {
                        ViewModel?.OpenNote();
                        break;
                    }
                case "Close Note":
                    {
                        ViewModel?.CloseNote();
                        break;
                    }
                case "Delete Note":
                    {
                        ViewModel?.DeleteNote();
                        break;
                    }
                default:
                    break;
            }
        }

        #endregion
    }
}

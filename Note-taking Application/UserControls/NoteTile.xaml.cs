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
                noteTileViewModel.Content = note.Content ?? "";
                noteTileViewModel.LastEdit = note.LastEdit;
                noteTile.DataContext = noteTileViewModel;

                MainWindowViewModel mainViewModel = (MainWindowViewModel)Application.Current.MainWindow.DataContext;
                if (mainViewModel != null)
                {
                    noteTileViewModel.OnCreateNewNote += mainViewModel.AddNote_Request;
                    noteTileViewModel.OnDeleteNote += mainViewModel.DeleteNote_Request;
                    noteTileViewModel.OnOpenNote += mainViewModel.OpenNote_Request;
                    noteTileViewModel.OnCloseNote += mainViewModel.CloseNote_Request;
                    noteTileViewModel.OnContentUpdate += mainViewModel.ContentUpdate_Request;
                }

                if (note.IsOpen)
                {
                    bool hasOpenWindow = WindowManager.HasOpenWindow(note);
                    if (!hasOpenWindow)
                    {
                        noteTileViewModel.OpenNote();
                    }
                }
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

        #region Mouse Events

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush(Color.FromRgb(65, 65, 65));
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
        }

        private DateTime lastClickTime = DateTime.Now;
        private const int doubleClickDelay = 200;
        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan clickInterval = now - lastClickTime;
            lastClickTime = now;

            if (clickInterval.TotalMilliseconds < doubleClickDelay)
            {
                // Only want to open this if its not already open
                if (!ViewModel.NoteModel.IsOpen)
                    ViewModel.OpenNote();
            }
        }

        #endregion
    }
}

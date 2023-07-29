using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.UserControls;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Note_taking_Application.ViewModels
{
    public class NoteTileViewModel : BaseViewModel
    {
        public Note NoteModel { get; set; }

        private IDialogClient<NotesPageViewModel> NotesPage { get; set; }

        #region Constructor

        public NoteTileViewModel()
        {
            NoteModel = new Note();
            OpenNoteCommand = new RelayCommand(OpenNote);
            CloseNoteCommand = new RelayCommand(CloseNote);
            DeleteNoteCommand = new RelayCommand(DeleteNote);
        }

        #endregion

        #region ICommands

        public ICommand OpenNoteCommand { get; set; }
        public ICommand CloseNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        #endregion

        public void OpenNote()
        {
            IDialogClient<NotesPageViewModel> newNotesPage = (NotesPage)WindowManager.CreateWindow<NotesPage, NotesPageViewModel>(null);

            if (newNotesPage != null)
            {
                newNotesPage.ViewModel = new NotesPageViewModel(NoteModel);
                NotesPage = newNotesPage;
                WindowManager.OpenWindow(newNotesPage);
            }
        }
        public void CloseNote()
        {
            WindowManager.CloseWindow(NotesPage);
            NoteModel.IsOpen = false;
        }
        public void DeleteNote()
        {

        }

        

    }
}

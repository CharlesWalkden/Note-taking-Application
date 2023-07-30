using Note_taking_Application.Event_Args;
using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.UserControls;
using System;
using System.Windows.Input;

namespace Note_taking_Application.ViewModels
{
    public class NoteTileViewModel : BaseViewModel
    {
        public Note NoteModel { get; set; }

        public string Content
        {
            get => content ?? "";
            set
            {
                if (content == value)
                    return;

                content = value;
                OnPropertyChanged();
            }
        }
        public DateTime LastEdit
        {
            get => lastEdit;
            set
            {
                if (lastEdit == value)
                    return;

                lastEdit = value;
                OnPropertyChanged();
            }
        }

        private string? content { get; set; } 
        private DateTime lastEdit { get; set; }

        private IDialogClient<NotesPageViewModel>? NotesPage { get; set; }

        #region Constructor

        public NoteTileViewModel()
        {
            LastEdit = DateTime.Now;
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
            IDialogClient<NotesPageViewModel>? newNotesPage = WindowManager.CreateWindow<NotesPage, NotesPageViewModel>();

            if (newNotesPage != null)
            {
                newNotesPage.ViewModel = new NotesPageViewModel(NoteModel);
                newNotesPage.OnClose += NotesPage_OnClose;
                newNotesPage.ViewModel.OnContentChanged += NotesPage_UpdateContent;
                newNotesPage.ViewModel.OnCreateNewNote += NotesPage_CreateNewNote;

                // Store reference to the notes page we are opening.
                NotesPage = newNotesPage;

                WindowManager.OpenWindow(newNotesPage);
            }
        }
        public void CloseNote()
        {
            if (NotesPage != null)
            {
                WindowManager.CloseWindow(NotesPage);
                NoteModel.IsOpen = false;
            }
        }
        public void DeleteNote()
        {

        }

        private void NotesPage_OnClose(object? sender, DialogEventArgs e)
        {
            CloseNote();
        }

        private void NotesPage_UpdateContent(object? sender, NotesPageUpdateEventArgs e)
        {
            Content = e.Content ?? "";
            LastEdit = e.LastEdit;
        }
        private void NotesPage_CreateNewNote(object? sender, EventArgs e)
        {

        }

    }

}

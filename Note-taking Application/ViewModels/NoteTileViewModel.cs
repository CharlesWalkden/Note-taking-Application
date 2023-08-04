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

        #region Events

        public EventHandler<NotesPageActionRequestEventArgs>? OnCreateNewNote;

        public EventHandler<NotesPageActionRequestEventArgs>? OnDeleteNote;

        public EventHandler<NotesPageActionRequestEventArgs>? OnOpenNote;

        public EventHandler<NotesPageActionRequestEventArgs>? OnCloseNote;

        public EventHandler<NotesPageActionRequestEventArgs>? OnContentUpdate;

        #endregion

        #region ICommands

        public ICommand OpenNoteCommand { get; set; }
        public ICommand CloseNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        #endregion

        public void OpenNote()
        {
            OnOpenNote?.Invoke(this, new NotesPageActionRequestEventArgs() { Note = NoteModel, Requester = this });
        }
        public void CloseNote()
        {
            OnCloseNote?.Invoke(this, new NotesPageActionRequestEventArgs() { Note = NoteModel, Requester = this });
        }
        public void DeleteNote()
        {
            OnDeleteNote?.Invoke(this, new NotesPageActionRequestEventArgs() { Note = NoteModel, Requester = this });
        }

        public void NotesPage_OnClose(object? sender, NotesPageActionRequestEventArgs e)
        {
            CloseNote();
        }

        public void NotesPage_UpdateContent(object? sender, NotesPageActionRequestEventArgs e)
        {
            Content = e.Note.Content ?? "";
            LastEdit = e.Note.LastEdit;
            // Calling this so that we can then update the note list as the last edit time will have updated.
            // This will re-order the list and mostlikely move this note to the top.
            OnContentUpdate?.Invoke(sender, e);
        }
        
        public void NotesPage_CreateNewNote(object? sender, NotesPageActionRequestEventArgs e)
        {
            OnCreateNewNote?.Invoke(sender, e);
        }

    }

}

using Note_taking_Application.Event_Args;
using Note_taking_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Note_taking_Application.ViewModels
{
    public class NotesPageViewModel : BaseViewModel
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
                NoteModel.Content = value;
                NoteModel.LastEdit = DateTime.Now;
                OnPropertyChanged();
                OnContentChanged?.Invoke(this, new NotesPageUpdateEventArgs() { Content = value, LastEdit = NoteModel.LastEdit });
            }
        }
        private string? content { get; set; }

        public ICommand AddNoteCommand { get; set; }
        public NotesPageViewModel(Note note)
        {
            NoteModel = note;
            Content = NoteModel.Content ?? "";
            NoteModel.IsOpen = true;
            AddNoteCommand = new RelayCommand(CreateNewNote);
        }

        public EventHandler<NotesPageUpdateEventArgs>? OnContentChanged;
        public EventHandler? OnCreateNewNote;
        
        public void CreateNewNote()
        {
            OnCreateNewNote?.Invoke(this, new EventArgs());
        }
        


    }
}

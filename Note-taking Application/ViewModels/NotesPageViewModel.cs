using Note_taking_Application.Event_Args;
using Note_taking_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;

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
                OnContentChanged?.Invoke(this, new NotesPageActionRequestEventArgs() { Note = NoteModel, Requester = this, SaveNote = false });
                OnContentUpdate();
            }
        }
        private string? content { get; set; }

        private DispatcherTimer? TypingTimer;
        private int Timeout { get; set; } = 1500;

        public ICommand AddNoteCommand { get; set; }
        public NotesPageViewModel(Note note)
        {
            RestartTimer();
            NoteModel = note;
            Content = NoteModel.Content ?? "";
            NoteModel.IsOpen = true;
            AddNoteCommand = new RelayCommand(CreateNewNote);
        }

        public EventHandler<NotesPageActionRequestEventArgs>? OnContentChanged;
        public EventHandler<NotesPageActionRequestEventArgs>? OnCreateNewNote;
        
        public void CreateNewNote()
        {
            OnCreateNewNote?.Invoke(this, new NotesPageActionRequestEventArgs() { Note = NoteModel, Requester = this, SaveNote = true });
        }
        
        public void OnContentUpdate()
        {
            RestartTimer();
        }

        #region Timer

        private void StopTimer()
        {
            if (TypingTimer != null)
            {
                TypingTimer.Stop();
                TypingTimer = null;
                OnContentChanged?.Invoke(this, new NotesPageActionRequestEventArgs() { Note = NoteModel, Requester = this, SaveNote = true });
            }
        }
        private void RestartTimer()
        {
            if (TypingTimer == null)
            {
                TypingTimer = new DispatcherTimer() { Interval = new TimeSpan(0,0,3) };
                TypingTimer.Tick += TypingTimer_Tick;
            }
            else
            {
                TypingTimer.Stop();
                TypingTimer.Start();
            }
        }

        private void TypingTimer_Tick(object? sender, EventArgs e)
        {
            StopTimer();
        }

        #endregion
    }
}

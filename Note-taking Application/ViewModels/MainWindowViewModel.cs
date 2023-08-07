using Note_taking_Application.DataStore;
using Note_taking_Application.Event_Args;
using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Note_taking_Application.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public CollectionList<Note> NoteList { get; set; }
        public CollectionList<Note> FilteredList { get; set; }
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                FilterNoteList(value);
            }
        }
        private string searchText { get; set; } = string.Empty;

        public IDataStore DataStore { get; set; }
        public ICommand AddNoteCommand { get; set; }

        public MainWindowViewModel()
        {

            AddNoteCommand = new RelayCommand(AddNote);
            NoteList = new CollectionList<Note>();
            FilteredList = new CollectionList<Note>();

            DataStore = new FileStore();

            if (DataStore.Load() is List<Note> data)
            {
                NoteList.AddNewRange(data);
                FilteredList.AddNewRange(data);
            }

            SortNoteList();
        }

        private void AddNote()
        {
            Note newNote = new(true);
            NoteList.Add(newNote);
            // Check if we are currently filtering. 
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                // if we are, check if this note matches the filter conditions.
                if (newNote.Content != null && newNote.Content.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                {
                    // Add it to the filer list if it matches the condition.
                    FilteredList.Add(newNote);
                }
            }
            SortNoteList();
        }
        private void OpenNote(NotesPageActionRequestEventArgs e)
        {
            IDialogClient<NotesPageViewModel>? newNotesPage = WindowManager.CreateWindow<NotesPage, NotesPageViewModel>();

            if (newNotesPage != null)
            {
                newNotesPage.ViewModel = new NotesPageViewModel(e.Note);

                if (e.Requester is NoteTileViewModel noteTileViewModel)
                {
                    newNotesPage.ViewModel.OnContentChanged += noteTileViewModel.NotesPage_UpdateContent;
                    newNotesPage.OnClose += noteTileViewModel.NotesPage_OnClose;
                    newNotesPage.ViewModel.OnCreateNewNote += noteTileViewModel.NotesPage_CreateNewNote;
                }

                WindowManager.OpenWindow(newNotesPage);
            }
        }

        private void RemoveNote(NotesPageActionRequestEventArgs e)
        {
            NoteList.Remove(e.Note);
            FilteredList.Remove(e.Note);
            if (e.Note.IsOpen)
                CloseNote(e);

            DeleteNote(e);
        }
        private void CloseNote(NotesPageActionRequestEventArgs e)
        {
            WindowManager.CloseWindow(e.Note);
            e.Note.IsOpen = false;
            SaveNote(e);
        }
        private void SaveNote(NotesPageActionRequestEventArgs e)
        {
            DataStore.Save(e.Note);
        }
        private void DeleteNote(NotesPageActionRequestEventArgs e)
        {
            DataStore.Delete(e.Note);
        }
        private void SortNoteList()
        {
            FilteredList.AddNewRange(FilteredList.OrderByDescending(x => x.LastEdit).ToList());
        }
        private void FilterNoteList(string filter)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // if we have no filter, we want everything. 
                FilteredList.AddNewRange(NoteList);
            }
            else
            {
                List<Note> filterList = new List<Note>();

                foreach (Note note in NoteList.Where(x => x.Content != null && x.Content.Contains(filter, StringComparison.OrdinalIgnoreCase)))
                {
                    filterList.Add(note);
                }

                FilteredList.AddNewRange(filterList);
            }

            SortNoteList();
        }

        public void AddNote_Request(object? sender, EventArgs e)
        {
            AddNote();
        }
        public void CloseNote_Request(object? sender, NotesPageActionRequestEventArgs e)
        {
            CloseNote(e);
        }
        public void DeleteNote_Request(object? sender, NotesPageActionRequestEventArgs e)
        {
            RemoveNote(e);
        }
        public void OpenNote_Request(object? sender, NotesPageActionRequestEventArgs e)
        {
            OpenNote(e);
        }
        public void ContentUpdate_Request(object? sender, NotesPageActionRequestEventArgs e)
        {
            SortNoteList();

            // Don't want to always save, only when we specify to save.
            if (e.SaveNote)
            {
                SaveNote(e);
            }
        }
    }

   
}

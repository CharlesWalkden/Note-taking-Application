using Note_taking_Application.DataStore;
using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.UserControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Note_taking_Application.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public CollectionList<Note> NoteList { get; set; }
        

        public IDataStore DataStore { get; set; }
        public ICommand AddNoteCommand { get; set; }

        public MainWindowViewModel()
        {

            AddNoteCommand = new RelayCommand(AddNote);
            NoteList = new CollectionList<Note>();

            DataStore = new FileStore();

            if (DataStore.Load() is List<Note> data)
                NoteList.AddNewRange(data);

            SortNoteList();
        }

        public void AddNote()
        {
            Note newNote = new();
            NoteList.Add(newNote);
            SortNoteList();
        }

        public void RemoveNote(Note note)
        {
            NoteList.Remove(note);
        }
        private void SortNoteList()
        {
            NoteList.AddNewRange(NoteList.OrderBy(x => x.LastEdit).ToList());
        }
    }

   
}

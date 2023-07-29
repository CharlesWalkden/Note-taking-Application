using Note_taking_Application.DataStore;
using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.UserControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Note_taking_Application.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public CollectionList<Note> NoteList { get; set; }

        public IDataStore DataStore { get; set; }

        public MainWindowViewModel()
        {
            NoteList = new CollectionList<Note>();

            DataStore = new FileStore();

            if (DataStore.Load() is List<Note> data)
                NoteList.AddNewRange(data);


            //DialogLauncher<NotesPage> test = new DialogLauncher<NotesPage>(this, System.Windows.ResizeMode.CanResize);
            //WindowManager.WindowStack.Add(test.Window);
            //test.Show();
            //DialogLauncher<NotesPage> test1 = new DialogLauncher<NotesPage>(this, System.Windows.ResizeMode.CanResize);
            //WindowManager.WindowStack.Add(test1.Window);
            //test1.Show();
            //DialogLauncher<NotesPage> test2 = new DialogLauncher<NotesPage>(this, System.Windows.ResizeMode.CanResize);
            //WindowManager.WindowStack.Add(test2.Window);
            //test2.Show();
            //DialogLauncher<NotesPage> test3 = new DialogLauncher<NotesPage>(this, System.Windows.ResizeMode.CanResize);
            //WindowManager.WindowStack.Add(test3.Window);
            //test3.Show();
            //DialogLauncher<NotesPage> test4 = new DialogLauncher<NotesPage>(this, System.Windows.ResizeMode.CanResize);
            //WindowManager.WindowStack.Add(test4.Window);
            //test4.Show();
        }

        public void AddNote(Note newNote)
        {
            NoteList.Add(newNote);
        }

        public void RemoveNote(Note note)
        {
            NoteList.Remove(note);
        }
    }
}

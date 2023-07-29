using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.DataStore
{
    public class FileStore : IDataStore
    {
        public List<Note>? NoteList { get; set; }
        public object Load()
        {
            NoteList ??= new List<Note>();

            if (!Directory.Exists("Notes"))
                Directory.CreateDirectory("Notes");

            var files = Directory.GetFiles("Notes");

            foreach (string file in files)
            {
                Note? note = file.DeserializeObject<Note>();

                if (note != null)
                {
                    NoteList.Add(note);
                }
            }
            
            if (NoteList.Count == 0)
                return GetDummyData();

            return NoteList;
        }

        public void Save(string json)
        {
            throw new NotImplementedException();
        }

        private List<Note> GetDummyData()
        {
            List<Note> note = new List<Note>()
            {
                new Note("Test1","Content1"),
                new Note("Test2","Content2"),
                //new Note("Test3","Content3"),
                //new Note("Test4","Content4"),
                //new Note("Test5","Content5"),
                //new Note("Test6","Content6"),
                //new Note("Test7","Content7"),
                //new Note("Test8","Content8"),
            };
            return note;
        }
    }
}

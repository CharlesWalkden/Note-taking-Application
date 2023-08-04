using Newtonsoft.Json;
using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

            return NoteList;
        }

        public void Save(Note note)
        {
            string json = JsonConvert.SerializeObject(note);
            string fileName = $@"Notes\{note.ID}";

            File.WriteAllText(fileName, json);
        }

        public void Delete(Note note)
        {
            string filename = $@"Notes\{note.ID}";

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }
}

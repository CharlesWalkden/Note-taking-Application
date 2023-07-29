using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.Models
{
    public class Note
    { 
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime LastEdit { get; set; }
        public bool IsOpen { get; set; } = false;
        public Note(string title, string content)
        {
            LastEdit = DateTime.Now;
            Title = title;
            Content = content;
        }
        public Note()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.Models
{
    public class Note
    { 
        public string Title { get; set; }
        public string Content { get; set; }

        public Note(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}

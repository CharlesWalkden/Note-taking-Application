using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.Event_Args 
{
    public class NotesPageUpdateEventArgs : EventArgs
    {
        public string? Content { get; set; }
        public DateTime LastEdit { get; set; }
    }
}

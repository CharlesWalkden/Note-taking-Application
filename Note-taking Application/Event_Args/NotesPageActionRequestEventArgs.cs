using Note_taking_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.Event_Args
{
    public class NotesPageActionRequestEventArgs : EventArgs
    {
        public Note Note { get; set; }
        public object Requester { get; set; }

    }
}

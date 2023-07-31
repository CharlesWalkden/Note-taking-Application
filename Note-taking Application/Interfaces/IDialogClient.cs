using Note_taking_Application.Event_Args;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Note_taking_Application.Interfaces
{
    public interface IDialogClient<T>
    {
        event EventHandler<NotesPageActionRequestEventArgs> OnClose;
        T? ViewModel { get; set; }
        void DialogClient_Activated(object? sender, EventArgs e);
        void DialogClient_Deactivated(object? sender, EventArgs e);
    } 
}

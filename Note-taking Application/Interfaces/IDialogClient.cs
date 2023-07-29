using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.Interfaces
{
    public interface IDialogClient<T>
    {
        event EventHandler<DialogEventArgs> OnClose;
        T ViewModel { get; set; }  
    }
}

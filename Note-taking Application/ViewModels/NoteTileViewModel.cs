using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Note_taking_Application.ViewModels
{
    public class NoteTileViewModel : BaseViewModel
    {
        public string? Content { get; set; }
        public DateTime? LastEdit { get; set; }

        #region Constructor

        public NoteTileViewModel()
        {
            OpenNoteCommand = new RelayCommand(OpenNote);
            CloseNoteCommand = new RelayCommand(CloseNote);
            DeleteNoteCommand = new RelayCommand(DeleteNote);
        }

        #endregion

        #region ICommands

        public ICommand OpenNoteCommand { get; set; }
        public ICommand CloseNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        #endregion

        public void OpenNote()
        {

        }
        public void CloseNote()
        {

        }
        public void DeleteNote()
        {

        }
    }
}

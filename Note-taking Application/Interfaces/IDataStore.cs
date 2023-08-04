using Note_taking_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.Interfaces
{
    public interface IDataStore
    {
        public object Load();
        public void Save(Note note);
        public void Delete(Note note);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_taking_Application.Models
{
    public class Note
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string? Content { get; set; }
        public DateTime LastEdit { get; set; }
        public bool IsOpen { get; set; } = false;
        public Note(Guid id, string content, bool isOpen = false)
        {
            LastEdit = DateTime.Now;
            ID = id;
            Content = content;
            IsOpen = isOpen;
        }
        public Note(bool isOpen = false)
        {
            LastEdit = DateTime.Now;
            IsOpen = isOpen;
        }

        /// <summary>
        /// Empty constructor needed for Deserialization of json file.
        /// </summary>
        public Note()
        {

        }
    }
}

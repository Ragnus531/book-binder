using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Models
{
    public class BookNote
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<NoteSection> NoteSections { get; set; } = new List<NoteSection>();
    }
}

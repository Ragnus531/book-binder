using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Models
{
    public class BookNoteGroup : List<BookNote>
    {
        public string GroupTitle { get; set; }

        public BookNoteGroup(string groupTitle, List<BookNote> bookNotes)
            : base(bookNotes)
        {
            GroupTitle = groupTitle;
        }
    }

    public class BookNote
    {
        public string Title { get; set; }
        public string AbsolutePath { get; set; }
    }
}

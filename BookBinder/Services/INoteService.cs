using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBinder.Models;

namespace BookBinder.Services
{
    public interface INoteService
    {
        IList<BookNote> GetNotes();
        void AddNote(BookNote bookNote);
        void UpdateNote(BookNote bookNote);
        void DeleteNote(BookNote bookNote);
        void DeleteAll();
    }
}

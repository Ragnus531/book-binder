using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace BookBinder.Services
{
    public class NoteService : INoteService
    {
        private readonly ILiteDatabase _liteDatabase;
        private const string NOTES_COLLECTION = "notes";

        public NoteService(ILiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public void AddNote(BookNote bookNote)
        {
            Collection.Insert(bookNote);
            Collection.EnsureIndex(x => x.Title);
        }

        public void DeleteNote(BookNote bookNote)
        {
            Collection.Delete(bookNote.Id);
        }

        public void UpdateNote(BookNote bookNote)
        {
            Collection.Update(bookNote);
            Collection.EnsureIndex(x => x.Title);
        }

        public IList<BookNote> GetNotes() => Collection.Query().ToList();

        public void DeleteAll() => Collection.DeleteAll();

        private ILiteCollection<BookNote> Collection =>
            _liteDatabase.GetCollection<BookNote>(NOTES_COLLECTION);
    }
}

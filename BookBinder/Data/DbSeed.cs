using BookBinder.Models;
using BookBinder.Services;

namespace BookBinder.Data;

public static class DbSeed
{
    public static void Seed(INoteService noteService)
    {
        if (noteService.GetNotes().Count == 0)
        {
            List<BookNote> bookNotes =
            [
                .. new List<BookNote>
                {
                    new BookNote { Title = "Borgos" },
                    new BookNote { Title = "Batman" },
                    new BookNote { Title = "Diablo" },
                    new BookNote { Title = "Diaman" },
                    new BookNote { Title = "Hobbit" },
                    new BookNote { Title = "Outpost" },
                    new BookNote { Title = "Star wars" },
                    new BookNote { Title = "Shrek" },
                    new BookNote { Title = "Sekiro" }
                },
            ];

            foreach (var item in bookNotes)
            {
                noteService.AddNote(item);
            }
        }
    }
}

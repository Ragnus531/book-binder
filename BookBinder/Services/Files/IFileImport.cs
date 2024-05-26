using BookBinder.Models;

namespace BookBinder.Services.Files
{
    public interface IFileImport
    {
        Task<BookNote?> Import();
    }
}

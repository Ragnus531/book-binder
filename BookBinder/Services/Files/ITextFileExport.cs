using BookBinder.Models;

namespace BookBinder.Services.Files
{
    interface ITextFileExport : IFileExport
    {
        Task TextExport(BookNote bookNote);
    }
}

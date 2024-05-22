using BookBinder.Models;
using Java.IO;

namespace BookBinder.Services.Files;

public class TextFileExport : ITextFileExport
{
    public async Task FileExport(BookNote bookNote)
    {
        await Task.Delay(100);
        using MemoryStream memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);

        foreach (var noteSection in bookNote.NoteSections)
        {
            writer.WriteLine(noteSection.Title);

            foreach (var element in noteSection.Elements)
            {
                writer.WriteLine($"    {element.Name} - {element.Description}");
            }

            writer.WriteLine(); // Add an empty line between sections
        }

        //await Share.Default.RequestAsync(
        //    new ShareFileRequest { Title = "Share text file", File = new ShareFile(file) }
        //);
    }

    public Task TextExport(BookNote bookNote)
    {
        throw new NotImplementedException();
    }
}

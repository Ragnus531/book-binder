﻿using System.IO;
using System.Text;
using BookBinder.Models;
using CommunityToolkit.Maui.Storage;

namespace BookBinder.Services.Files;

public class TextFileExport : ITextFileExport
{
    public async Task FileExport(BookNote bookNote, bool exportToApp)
    {
        string fileName = bookNote.Title + ".txt";
        string filePath = Path.Combine(ExportedFileFolder(), fileName);

        if (exportToApp)
        {
            await FileSaver.Default.SaveAsync(
                fileName,
                FileMemoryStream(bookNote),
                new CancellationToken()
            );
        }
        else
        {
            //If the file does not exist, the writer will create a new file.
            //If the file already exists, the writer will override its content.
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var noteSection in bookNote.NoteSections)
                {
                    writer.WriteLine(noteSection.Title);

                    foreach (var element in noteSection.Elements)
                    {
                        writer.WriteLine($"    {element.Name} - {element.Description}");
                    }

                    writer.WriteLine(); // Add an empty line between sections
                }

                writer.WriteLine(); // Add an empty line between BookNotes (optional)
            }

            await Share.Default.RequestAsync(
                new ShareFileRequest
                {
                    Title = "Yo export that file", //todo: better text or leave meh
                    File = new ShareFile(filePath)
                }
            );
        }
    }

    public Task TextExport(BookNote bookNote)
    {
        throw new NotImplementedException();
    }

    private MemoryStream FileMemoryStream(BookNote bookNote)
    {
        var memStream = new MemoryStream();
        var writer = new StreamWriter(memStream);
        foreach (var noteSection in bookNote.NoteSections)
        {
            writer.WriteLine(noteSection.Title);

            foreach (var element in noteSection.Elements)
            {
                writer.WriteLine($"    {element.Name} - {element.Description}");
            }

            writer.WriteLine(); // Add an empty line between sections
        }
        writer.Flush();
        memStream.Seek(0, SeekOrigin.Begin);
        return memStream;
    }

    private string ExportedFileFolder() => Path.Combine(FileSystem.AppDataDirectory);
}
﻿using System.Text.RegularExpressions;
using BookBinder.Models;
using BookBinder.Services.Files.FileRequests;
using FileInfo = BookBinder.Services.Files.FileRequests.FileInfo;

namespace BookBinder.Services.Files;

public class TextFileImport : IFileImport
{
    private readonly IFilePickerRequest _filePicker;

    public TextFileImport(IFilePickerRequest filePicker)
    {
        _filePicker = filePicker;
    }

    public async Task<BookNote?> Import()
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.text" } }, // UTType values
                    { DevicePlatform.Android, new[] { "text/plain" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".txt" } }, // file extension
                    { DevicePlatform.macOS, new[] { "txt" } },
                }
            );

            PickOptions options = new PickOptions()
            {
                PickerTitle = "Please select a file to import",
                FileTypes = customFileType,
            };

            FileInfo fileInfo = await _filePicker.PickFileAsync(options);
            if (fileInfo != null)
            {
                using var stream = fileInfo.FileStream;
                string bookNoteTitle = Path.GetFileNameWithoutExtension(fileInfo.FileName);
                BookNote bookNote = ReadBookNoteFromStream(stream, bookNoteTitle);
                return bookNote;
            }
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }

        return null;
    }

    /// <summary>
    /// Convert Stream to BookNote
    /// </summary>
    /// <param name="stream">I wish i know what that stream is?</param>
    /// <returns>BookNote from stream</returns>
    private BookNote ReadBookNoteFromStream(Stream stream, string bookNoteTitle)
    {
        BookNote note = new BookNote() { Title = bookNoteTitle };

        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(stream))
            {
                string? line;
                // Read and display lines from the file until the end of
                // the file is reached.
                NoteSection noteSection = null;

                while ((line = sr.ReadLine()) != null)
                {
                    // Actually last check to see if it's end of file
                    if (sr.Peek() == -1)
                    {
                        if (noteSection != null)
                        {
                            note.NoteSections.Add(noteSection);
                        }
                    }

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (!HasSymbolAtBeginning(line))
                    {
                        if (noteSection != null)
                        {
                            note.NoteSections.Add(noteSection);
                        }

                        //Starts with letter it's a section
                        noteSection = new NoteSection() { Title = line.Trim() };
                    }
                    else
                    {
                        //Starts with symbol should be an element
                        if (noteSection == null)
                        {
                            noteSection = new NoteSection() { Title = "Section" };
                        }

                        var eleText = SplitStringBySymbol(line);

                        noteSection.Elements.Add(
                            new NoteSectionElement()
                            {
                                Name = eleText.Item1,
                                Description = eleText.Item2
                            }
                        );
                    }
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return note;
    }

    private bool HasSymbolAtBeginning(string input)
    {
        // Regular expression to check if the string starts with any character that is not a letter.
        Regex regex = new Regex(@"^[^a-zA-Z]");
        return regex.IsMatch(input);
    }

    static (string title, string description) SplitStringBySymbol(string text)
    {
        // Step 1: Remove the first character if it is a symbol
        if (!char.IsLetterOrDigit(text[0]))
        {
            text = text.Substring(1);
        }

        bool hasSpace = false;
        int delimiterIndex = -1;
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsWhiteSpace(text[i]))
            {
                hasSpace = true;
            }

            if (!char.IsLetterOrDigit(text[i]) && text[i - 1] == ' ')
            {
                delimiterIndex = i;
                break;
            }
        }

        if (delimiterIndex == -1)
        {
            if (hasSpace)
            {
                Regex regex = new Regex(@"\s{2,}"); //first check if there is a more than 1 space section
                Match match = regex.Match(text);
                if (match.Success)
                {
                    int index = match.Index;
                    // Separate the string into two parts
                    string title = text.Substring(0, index);
                    string description = text.Substring(index);
                    return (title, description);
                }
                else
                {
                    var splitted = text.Split(' ', 2);
                    return (splitted[0], splitted[1]);
                }
            }

            return (text.Trim(), string.Empty);
        }

        string titlePart = text.Substring(0, delimiterIndex).TrimEnd();
        string descriptionPart = text.Substring(delimiterIndex + 1).TrimStart();

        return (titlePart, descriptionPart);
    }
}

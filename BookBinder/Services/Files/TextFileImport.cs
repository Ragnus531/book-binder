using System.Text.RegularExpressions;
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
                    { DevicePlatform.iOS, new[] { ".txt" } }, // UTType values
                    { DevicePlatform.Android, new[] { ".txt" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".txt" } }, // file extension
                    { DevicePlatform.Tizen, new[] { ".txt" } },
                    { DevicePlatform.macOS, new[] { ".txt" } }, // UTType values
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

                        var eleText = SplitStringByDash(line);

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

    private Tuple<string, string> SplitStringByDash(string input)
    {
        // Ignore leading dash if present
        if (input.StartsWith("-"))
        {
            input = input.Substring(1);
        }

        // Check if the string contains a dash
        if (input.Contains('-'))
        {
            // Split the string by the dash
            string[] parts = input.Split(new char[] { '-' }, 2);
            return Tuple.Create(parts[0], parts[1]);
        }
        else
        {
            // Return the full string as the first part and an empty string as the second part
            return Tuple.Create(input, string.Empty);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBinder.Models;
using FluentAssertions;

namespace BookBinder.Tests.Models;

public class BookNoteTests
{
    [Fact]
    public void book_note_should_create_export_format_correctly()
    {
        BookNote bookNote = new BookNote()
        {
            Title = "BookNoteTitle1",
            NoteSections = new List<NoteSection>()
            {
                new NoteSection()
                {
                    Title = "Section1",
                    Elements = new List<NoteSectionElement>()
                    {
                        new NoteSectionElement()
                        {
                            Name = "element1Title",
                            Description = "element1Description"
                        },
                        new NoteSectionElement()
                        {
                            Name = "element2Title",
                            Description = "element2Description"
                        }
                    }
                },
                new NoteSection { Title = "Section2" },
                new NoteSection()
                {
                    Title = "Section3",
                    Elements = new List<NoteSectionElement>()
                    {
                        new NoteSectionElement()
                        {
                            Name = "element3Title",
                            Description = "element3Description"
                        }
                    }
                }
            }
        };

        var exportedFormat = bookNote.ToExportFormat();
        exportedFormat.Should().NotBeNullOrWhiteSpace();
        exportedFormat.Should().StartWith("Section1");
    }
}

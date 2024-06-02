using BookBinder.Models;
using BookBinder.Services.Files;
using BookBinder.Services.Files.FileRequests;
using FluentAssertions;
using NSubstitute;

namespace BookBinder.Tests.Services.Files;

public class TextFileImportTests
{
    private IFilePickerRequest _filePickerMock = Substitute.For<IFilePickerRequest>();
    private TextFileImport fileImport;

    public TextFileImportTests()
    {
        fileImport = new TextFileImport(_filePickerMock);
    }

    [Fact]
    public async Task file_import_should_import_file_one()
    {
        _filePickerMock
            .PickFileAsync(Arg.Any<PickOptions>())
            .Returns(
                new BookBinder.Services.Files.FileRequests.FileInfo(
                    "BookNoteTest1",
                    StreamTestData.StreamForFirstTest()
                )
            );

        BookNote? bookNote = await fileImport.Import();

        bookNote.Should().NotBeNull();
        bookNote?.NoteSections.Count.Should().Be(3); // section1, section2, section3
    }

    [Fact]
    public async Task file_import_should_import_file_two()
    {
        _filePickerMock
            .PickFileAsync(Arg.Any<PickOptions>())
            .Returns(
                new BookBinder.Services.Files.FileRequests.FileInfo(
                    "BookNoteTest1",
                    StreamTestData.StreamForSecondTest()
                )
            );

        BookNote? bookNote = await fileImport.Import();

        bookNote.Should().NotBeNull();
        bookNote?.NoteSections.Count.Should().Be(2); // ..,  section1
    }

    [Fact]
    public async Task file_import_should_import_file_three()
    {
        _filePickerMock
            .PickFileAsync(Arg.Any<PickOptions>())
            .Returns(
                new BookBinder.Services.Files.FileRequests.FileInfo(
                    "BookNoteTest1",
                    StreamTestData.StreamForThirdTest()
                )
            );

        BookNote? bookNote = await fileImport.Import();

        bookNote.Should().NotBeNull();
        bookNote?.NoteSections.Count.Should().Be(3); // sectionTemp,  section1, section2
    }

    [Fact]
    public async Task file_import_should_import_file_four()
    {
        _filePickerMock
            .PickFileAsync(Arg.Any<PickOptions>())
            .Returns(
                new BookBinder.Services.Files.FileRequests.FileInfo(
                    "BookNoteTest1",
                    StreamTestData.StreamForFourthTest()
                )
            );

        BookNote? bookNote = await fileImport.Import();

        bookNote.Should().NotBeNull();
        bookNote?.NoteSections.Count.Should().Be(1); // sectionTemp
    }

    [Fact]
    public async Task file_import_should_import_file_fifth()
    {
        _filePickerMock
            .PickFileAsync(Arg.Any<PickOptions>())
            .Returns(
                new BookBinder.Services.Files.FileRequests.FileInfo(
                    "BookNoteTest1",
                    StreamTestData.StreamForThirdFifth()
                )
            );

        BookNote? bookNote = await fileImport.Import();

        bookNote.Should().NotBeNull();
        bookNote?.NoteSections[0].Elements[0].Name.Should().Be("element1.Title");
        bookNote?.NoteSections[0].Elements[0].Description.Should().Be("element1.Descriptn");
    }
}

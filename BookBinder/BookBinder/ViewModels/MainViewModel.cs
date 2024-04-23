namespace BookBinder.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public List<BookNoteGroup> BookNotes { get; set; } = new List<BookNoteGroup>();

    public MainViewModel()
    {
        BookNotes.Add(
            new BookNoteGroup(
                "B",
                new List<BookNote>
                {
                    new BookNote { Title = "Borgos" },
                    new BookNote { Title = "Batman" },
                }
            )
        );

        BookNotes.Add(
            new BookNoteGroup(
                "D",
                new List<BookNote>
                {
                    new BookNote { Title = "Diablo" },
                    new BookNote { Title = "Diaman" },
                }
            )
        );

        BookNotes.Add(
            new BookNoteGroup("H", new List<BookNote> { new BookNote { Title = "Hobbit" }, })
        );

        BookNotes.Add(
            new BookNoteGroup("O", new List<BookNote> { new BookNote { Title = "Outpost" }, })
        );

        BookNotes.Add(
            new BookNoteGroup(
                "S",
                new List<BookNote>
                {
                    new BookNote { Title = "Star wars" },
                    new BookNote { Title = "Shrek" },
                    new BookNote { Title = "Sekiro" }
                }
            )
        );
    }

    [RelayCommand]
    public async void GoToBookNoteDetail(BookNote bookNote)
    {
        await Shell.Current.GoToAsync(
            nameof(BookNoteDetail),
            true,
            new Dictionary<string, object> { { "BookItem", bookNote } }
        );
    }
}

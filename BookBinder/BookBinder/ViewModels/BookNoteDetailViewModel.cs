namespace BookBinder.ViewModels;

[QueryProperty(nameof(BookItem), "BookItem")]
public partial class BookNoteDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    BookNote? bookItem;
}

namespace BookBinder.Views;

public partial class BookNoteDetail : ContentPage
{
    public BookNoteDetail() { }

    public BookNoteDetail(BookNoteDetailViewModel bookNoteDetailViewModel)
    {
        InitializeComponent();
        BindingContext = bookNoteDetailViewModel;
    }
}

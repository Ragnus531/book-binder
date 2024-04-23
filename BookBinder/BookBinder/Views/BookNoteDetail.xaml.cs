namespace BookBinder.Views;

public partial class BookNoteDetail : ContentPage
{
    public BookNoteDetail()
    {
        Console.WriteLine("saa");
    }

    public BookNoteDetail(BookNoteDetailViewModel bookNoteDetailViewModel)
    {
        InitializeComponent();
        BindingContext = bookNoteDetailViewModel;
    }
}

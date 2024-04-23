namespace BookBinder;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(BookNoteDetail), typeof(BookNoteDetail));
    }
}

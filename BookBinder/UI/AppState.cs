namespace BookBinder.UI;

public class AppState
{
    public string CurrentTitle { get; private set; } = "Home";

    public event Action OnChange;

    public void SetTitle(string title)
    {
        CurrentTitle = title;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

namespace BookBinder.UI;

public class AppState
{
    public string CurrentTitle { get; private set; } = "Home";
    public bool NavigationVisible { get; private set; } = false;

    public event Action OnChange;
    public event Action OnNavigationChange;

    public void SetTitle(string title)
    {
        CurrentTitle = title;
        NotifyStateChanged();
    }

    public void SetNavigation(bool isVisible)
    {
        NavigationVisible = isVisible;
        NotifyNavigationChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    private void NotifyNavigationChanged() => OnNavigationChange?.Invoke();
}

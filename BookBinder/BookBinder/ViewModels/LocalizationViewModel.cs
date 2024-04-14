namespace BookBinder.ViewModels;

public partial class LocalizationViewModel : BaseViewModel
{
	public string LocalizedText => BookBinder.Resources.Strings.AppResources.HelloMessage;
}

namespace MobileAT1.Views;

public partial class NoInternetPage : ContentPage
{
	public NoInternetPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Closes game popout. Returns to previous view.
    /// </summary>
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
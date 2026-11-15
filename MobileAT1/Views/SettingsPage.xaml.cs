namespace MobileAT1.Views;
using Microsoft.Maui.Storage;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        // Theme
        string savedTheme = Preferences.Get(App.MODE, "Light");
        ThemePicker.SelectedItem = savedTheme;
        // Font size
        double savedFontSize = Preferences.Get(App.SIZE, 16.0);
        FontSlider.Value = savedFontSize;
    }

    private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedItem?.ToString() == "Dark")
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        // save theme between reboots
        string theme = Application.Current.UserAppTheme.ToString();
        Preferences.Set(App.MODE, theme);

    }

    private void FontSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        double baseBodySize = e.NewValue;
        Preferences.Set(App.SIZE, baseBodySize);

        // Body font size maps directly to the slider value
        Application.Current.Resources["FontSizeBody"] = baseBodySize;

        // Header 8 units larger than the body
        Application.Current.Resources["FontSizeHeader"] = baseBodySize + 8;

        // Small text 4 units smaller than the body
        Application.Current.Resources["FontSizeSmall"] = baseBodySize - 4;

    }
}
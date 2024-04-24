namespace Support_AppMobile;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}


    private async void OnClickBtnMvvmAdd(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MvvmTestPage());
    }

}
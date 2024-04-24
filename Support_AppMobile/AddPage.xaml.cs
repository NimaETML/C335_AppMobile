namespace Support_AppMobile;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}


    private async void OnClickBtnMvvmInt(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MvvmIntPage());
    }

}
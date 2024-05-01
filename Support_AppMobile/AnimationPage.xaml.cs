namespace Support_AppMobile;

public partial class AnimationPage : ContentPage
{
	public AnimationPage()
	{
		InitializeComponent();
	}


    private async void OnClickBtnMvvmInt(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MvvmIntPage());
    }

	// PAS FINI
	private async void RotateLeft(object sender, EventArgs e)
	{

	}

}
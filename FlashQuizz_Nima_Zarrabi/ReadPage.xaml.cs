namespace FlashQuizz_Nima_Zarrabi;

public partial class ReadPage : ContentPage
{
	public ReadPage()
	{
		InitializeComponent();
	}

    private async void OnDisReadPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
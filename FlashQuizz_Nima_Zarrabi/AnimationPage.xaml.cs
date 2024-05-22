namespace FlashQuizz_Nima_Zarrabi;

public partial class AnimationPage : ContentPage
{
	public AnimationPage()
	{
		InitializeComponent();
    }


    private async void OnClickBtnMvvmAnimationPage(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MvvmAnimationPage());
    }

    private void CancelAnimation(object sender, EventArgs e)
    {
        AnimationPageImage.CancelAnimations();
    }

    private async void RotateLeft(object sender, EventArgs e)
	{
        await AnimationPageImage.RelRotateTo(-360, 2000);
    }

    private async void RotateRight(object sender, EventArgs e)
    {
        await AnimationPageImage.RelRotateTo(360, 2000);
    }

    private async void ScaleUp(object sender, EventArgs e)
    {
        await AnimationPageImage.RelScaleTo(0.2, 1000);
    }

    private async void ScaleDown(object sender, EventArgs e)
    {
        await AnimationPageImage.RelScaleTo(-0.2, 1000);
    }

    private async void TranslateLeft(object sender, EventArgs e)
    {
        await AnimationPageImage.TranslateTo(-50, 0, 1000);
    }

    private async void TranslateRight(object sender, EventArgs e)
    {
        await AnimationPageImage.TranslateTo(50, 0, 1000);
    }

    private async void TranslateUp(object sender, EventArgs e)
    {
        await AnimationPageImage.TranslateTo(0, -50, 1000);
    }

    private async void TranslateDown(object sender, EventArgs e)
    {
        await AnimationPageImage.TranslateTo(0, 50, 1000);
    }

}
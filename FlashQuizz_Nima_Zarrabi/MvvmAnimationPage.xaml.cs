using FlashQuizz_Nima_Zarrabi.ViewModels;

namespace FlashQuizz_Nima_Zarrabi
{
    public partial class MvvmAnimationPage : ContentPage
    {
        public MvvmAnimationPage()
        {
            InitializeComponent();

            var vm = BindingContext as MvvmAnimationPageViewModel;

            // Ecriture standard avec une methode liée
            vm.RotateBoxUIAction = RotateUI;

            vm.MoveBoxUIAction = TranslateUI;

            vm.CancelBoxAnimationUIAction = CancelAnimationMvvmUI;

        }
        private void CancelAnimationMvvmUI()
        {
            this.MvvmAnimationPageImage.CancelAnimations();
        }

        private void RotateUI(int angle)
        {
            this.MvvmAnimationPageImage.RotateTo(angle);
        }

        private void TranslateUI(int multiplier)
        {
            this.MvvmAnimationPageImage.TranslationX -= multiplier;
        }
    }
}
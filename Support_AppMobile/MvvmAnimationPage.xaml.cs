using Support_AppMobile.ViewModels;

namespace Support_AppMobile
{
    public partial class MvvmAnimationPage : ContentPage
    {
        public MvvmAnimationPage()
        {
            InitializeComponent();

            var vm = BindingContext as MvvmAnimationPageViewModel;

            // Ecriture standard avec une methode li�e
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
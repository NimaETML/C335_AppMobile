﻿namespace Support_AppMobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void BtnReadPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReadPage());
        }

        private async void OnClickBtnMvvmSensorPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SensorPage());
        }
    }
}

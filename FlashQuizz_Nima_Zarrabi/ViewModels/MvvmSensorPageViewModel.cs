using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizz_Nima_Zarrabi.ViewModels
{
    public partial class MvvmSensorPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? sensorValue;

        [ObservableProperty]
        private bool enabled = false;

        [ObservableProperty]
        private bool disabled = false;

        [ObservableProperty]
        private bool toggled;

        [ObservableProperty]
        private string offColor = "black";

        [ObservableProperty]
        private string onColor = "red";

        [ObservableProperty]
        private string shakeBoxColor = "red";

        [ObservableProperty]
        private string activatedBoxColor = "black";

        [ObservableProperty]
        private string shakedColor = "black";


        [RelayCommand]
        private void Enable()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    // Turn on accelerometer
                    Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;

                    Accelerometer.Default.Start(SensorSpeed.Default);

                    Enabled = true;
                    Disabled = false;

                    ActivatedBoxColor = OnColor;
                }
                else
                {
                    //Turn off accelerometer
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
                    Enabled = false;
                    Disabled = true;
                    ActivatedBoxColor = OffColor;
                }
            }
        }
        
        private void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
        {
            SensorValue = e.Reading.ToString();
        }

        partial void OnToggledChanged(bool value)
        {
            ToggleShake();
        }

        private void ToggleShake()
        {
            if (Accelerometer.Default.IsSupported)
            {
                if (!Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                    Accelerometer.Default.Start(SensorSpeed.Game);
                    ShakeBoxColor = OffColor;
                }
                else
                {
                    Accelerometer.Default.Stop();
                    Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
                    ShakeBoxColor = OffColor;
                }
            }
        }
        private void Accelerometer_ShakeDetected(object? sender, EventArgs e)
        {
            ShakeBoxColor = OnColor;
        }
    }
}

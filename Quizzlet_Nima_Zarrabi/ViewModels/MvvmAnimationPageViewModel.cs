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
    public partial class MvvmAnimationPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool toggled = false;

        [ObservableProperty]
        private string label = "Rotate 90°";

        [ObservableProperty]
        private int speed = 1;

        public Action<int>? RotateBoxUIAction { private get; set; }

        private void RotateBox(int angle)
        {
            if (RotateBoxUIAction != null)
            {
                RotateBoxUIAction.Invoke(angle);
            }
            else
            {
                Trace.WriteLine($"No rotation action defined, would rotate {angle}");
            }
        }

        public Action<int>? MoveBoxUIAction { private get; set; }

        [RelayCommand]
        private void MoveBox(int multiplier)
        {
            if (MoveBoxUIAction != null)
            {
                MoveBoxUIAction.Invoke(Speed*multiplier);
            }
            else
            {
                Trace.WriteLine($"No move action defined, would move x {Speed*multiplier}");
            }
        }

        public Action? CancelBoxAnimationUIAction { private get; set; }

        [RelayCommand]
        private void CancelBoxAnimation()
        {
            if (CancelBoxAnimationUIAction != null)
            {
                CancelBoxAnimationUIAction.Invoke();
            }
        }

        partial void OnToggledChanged(bool value)
        {
            RotateBox(value ? 90 : 0);
            Label = value ? "Rotate back" : "Rotate 90°";
        }
    }


}

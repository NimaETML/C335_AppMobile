using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Support_AppMobile.ViewModels;

public partial class MvvmTestPageViewModel : ObservableObject
{

    [ObservableProperty]
    private int counter = 0;

    [RelayCommand]
    public void Increment(int incrementValue)
    {
        Counter += incrementValue;
    }

}
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Support_AppMobile.ViewModels;

public partial class MvvmListPageViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddWishCommand))]
    public string wishEntry = "";

    [ObservableProperty]
    public ObservableCollection<string> wishes = new() { "Slay", "Slay but like, more" };

    [RelayCommand(CanExecute = nameof(AddWishCanExecute))]
    public void AddWish(string wish)
    {

        Wishes.Add(wish);

        // Empty field after adding
        WishEntry = "";
    }

    public bool AddWishCanExecute()
    {
        return !string.IsNullOrEmpty(WishEntry);
    }
}

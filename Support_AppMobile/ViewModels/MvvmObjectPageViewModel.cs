using CommunityToolkit.Mvvm.Input;
using Support_AppMobile.Models;
using Support_AppMobile.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Support_AppMobile.ViewModels;

// PAS OUBLIER LE NUGGET Microsoft.EntityFrameworkCore.Sqlite
public partial class MvvmObjectPageViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddGreaterWishCommand))]
    private string greaterWishEntry = "";

    [ObservableProperty]
    private ObservableCollection<object> greaterWishes = new();

    [RelayCommand(CanExecute = nameof(AddGreaterWishCanExecute))]

    private async Task AddGreaterWish(string definition)
    {
        var greaterWish = new GreaterWish { Definition = definition };
        using (var dbContext = new AladdinContext())
        {
            dbContext.Add(greaterWish);
            await dbContext.SaveChangesAsync(); 
        }
        //Avoid full db refresh upon adding a new greaterWish
        GreaterWishes.Add(greaterWish);
        GreaterWishEntry = "";
    }

    private bool AddGreaterWishCanExecute()
    {
        return !string.IsNullOrEmpty(GreaterWishEntry);
    }

    public void CrudUpdateAllWishes()
    {
        RefreshGreaterWishesFromDB();
    }

    [RelayCommand]
    private void RefreshGreaterWishesFromDB(AladdinContext? context=null)
    {
        GreaterWishes.Clear();
        using (var dbContext = context??new AladdinContext())
        {
            foreach (var dbGreaterWish in dbContext.GreaterWishes)
            {
                GreaterWishes.Add(dbGreaterWish);
            }
        } 
    }

    [RelayCommand]
    private async Task Edit(GreaterWish greaterWish)
    {
        Trace.WriteLine($"Editing {greaterWish}");

        // Affiche un popup pour demander la modification
        // /!\ court-circuite MVVM mais toléré pour ne pas ajouter plus de complexité pour l'instant /!\
        string updatedDefinition = await Shell.Current.DisplayPromptAsync(title: "Modifier ", message: "", placeholder: greaterWish.Definition);

        //Si l'utilisateur n'appuie pas sur Cancel
        if(updatedDefinition!=null)
        {
            using (var dbContext = new AladdinContext())
            {
                //TODO : Faire la mise à jour uniquement si la definition a changé
                await dbContext.GreaterWishes
                    .Where(dbGreaterWish => dbGreaterWish.Id== greaterWish.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(dbWish => dbWish.Definition, updatedDefinition));


                // Rafraîchissement de la liste locale
                RefreshGreaterWishesFromDB(dbContext);
            }
        }
    }

    [RelayCommand]
    private async Task Delete(GreaterWish greaterWish)
    {
        Trace.WriteLine($"Deleting {greaterWish}");
        using (var dbContext = new AladdinContext())
        {
            await dbContext.GreaterWishes
                .Where(dbGreaterWish => dbGreaterWish.Id == greaterWish.Id)
                .ExecuteDeleteAsync();

            // Rafraîchissement de la liste locale
            RefreshGreaterWishesFromDB(dbContext);
        }
    }
}

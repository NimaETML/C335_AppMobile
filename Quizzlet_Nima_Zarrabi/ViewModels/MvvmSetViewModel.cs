using CommunityToolkit.Mvvm.Input;
using FlashQuizz_Nima_Zarrabi.Models;
using FlashQuizz_Nima_Zarrabi.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace FlashQuizz_Nima_Zarrabi.ViewModels;

// PAS OUBLIER LE NUGGET Microsoft.EntityFrameworkCore.Sqlite
public partial class MvvmSetViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddSetCommand))]
    private string setEntry = "";

    [ObservableProperty]
    private ObservableCollection<object> sets = new();

    [RelayCommand(CanExecute = nameof(AddSetCanExecute))]

    private async Task AddSet(string title, string description)
    {
        var set = new Set { Title = title, Description = description };
        using (var dbContext = new ConnectedContext())
        {
            dbContext.Add(set);
            await dbContext.SaveChangesAsync(); 
        }
        //Avoid full db refresh upon adding a new greaterWish
        Sets.Add(set);
        SetEntry = "";
    }

    private bool AddSetCanExecute()
    {
        return !string.IsNullOrEmpty(SetEntry);
    }

    public void CrudUpdateAllSets()
    {
        RefreshSetsFromDB();
    }

    [RelayCommand]
    private void RefreshSetsFromDB(ConnectedContext? context=null)
    {
        Sets.Clear();
        using (var dbContext = context??new ConnectedContext())
        {
            foreach (var dbSet in dbContext.Sets)
            {
                Sets.Add(dbSet);
            }
        } 
    }

    [RelayCommand]
    private async Task EditSet(Set set)
    {
        Trace.WriteLine($"Editing {set}");

        // Affiche un popup pour demander la modification
        // /!\ court-circuite MVVM mais toléré pour ne pas ajouter plus de complexité pour l'instant /!\
        string updatedTitle = await Shell.Current.DisplayPromptAsync(title: "Modifier ", message: "", placeholder: set.Title);

        //Si l'utilisateur n'appuie pas sur Cancel
        if(updatedTitle != null)
        {
            using (var dbContext = new ConnectedContext())
            {
                //TODO : Faire la mise à jour uniquement si la definition a changé
                await dbContext.Sets
                    .Where(dbGreaterWish => dbGreaterWish.Id== set.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(dbSet => dbSet.Description, updatedTitle));


                // Rafraîchissement de la liste locale
                RefreshSetsFromDB(dbContext);
            }
        }
    }

    [RelayCommand]
    private async Task DeleteSet(Set set)
    {
        Trace.WriteLine($"Deleting {set}");
        using (var dbContext = new ConnectedContext())
        {
            await dbContext.Cards
                .Where(dbSet => dbSet.Id == set.Id)
                .ExecuteDeleteAsync();

            // Rafraîchissement de la liste locale
            RefreshSetsFromDB(dbContext);
        }
    }
}

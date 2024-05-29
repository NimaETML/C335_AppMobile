using CommunityToolkit.Mvvm.Input;
using FlashQuizz_Nima_Zarrabi.Models;
using FlashQuizz_Nima_Zarrabi.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace FlashQuizz_Nima_Zarrabi.ViewModels;

    public partial class MvvmSetViewModel : ObservableObject
{

    [ObservableProperty]
    private string titleEntry;

    [ObservableProperty]
    private string description;

    //[ObservableProperty]
    //private Card[] cardEntry;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddSetCommand))]
    private Set? setEntry;

    [ObservableProperty]
    private ObservableCollection<object> sets = new();

    [RelayCommand(CanExecute = nameof(AddSetCanExecute))]

    private async Task AddSet(Set newSet)
    {
        //var set = new Set { Title = title,  };
        using (var dbContext = new ConnectedContext())
        {
            dbContext.Add(newSet);
            await dbContext.SaveChangesAsync();
        }
        //Avoid full db refresh upon adding a new set
        Sets.Add(newSet);
        SetEntry = null;
    }


    /// <summary>
    /// //                                                   PROBLEM HERE
    /// </summary>
    /// <param name="titleEntry"></param>
    /// <param name="description"></param>
    [RelayCommand]
    private void BuildSet(string titleEntry, string description)
    {
        Set set = new Set{ Title = titleEntry, Description = description};

        AddSet(set);
    }

    private bool AddSetCanExecute()
    {
        if (SetEntry == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void CrudUpdateAllSets()
    {
        RefreshSetsFromDB();
    }

    [RelayCommand]
    private void RefreshSetsFromDB(ConnectedContext? context = null)
    {
        Sets.Clear();
        using (var dbContext = context ?? new ConnectedContext())
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
        if (updatedTitle != null)
        {
            using (var dbContext = new ConnectedContext())
            {
                //TODO : Faire la mise à jour uniquement si la definition a changé
                await dbContext.Sets
                    .Where(dbGreaterWish => dbGreaterWish.Id == set.Id)
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
            await dbContext.Sets
                .Where(dbSet => dbSet.Id == set.Id)
                .ExecuteDeleteAsync();

            // Rafraîchissement de la liste locale
            RefreshSetsFromDB(dbContext);
        }
    }
}

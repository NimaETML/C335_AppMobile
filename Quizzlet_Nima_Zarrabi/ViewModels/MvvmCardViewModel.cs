using CommunityToolkit.Mvvm.Input;
using FlashQuizz_Nima_Zarrabi.Models;
using FlashQuizz_Nima_Zarrabi.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace FlashQuizz_Nima_Zarrabi.ViewModels;

// PAS OUBLIER LE NUGGET Microsoft.EntityFrameworkCore.Sqlite

// Classe vas etre supprimer et les methodes seront ajouté au ViewModel "MvvmSetViewModel"
public partial class MvvmSetViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCardCommand))]
    private string cardEntry = "";

    [ObservableProperty]
    private ObservableCollection<object> cards = new();

    [RelayCommand(CanExecute = nameof(AddCardCanExecute))]

    private async Task AddCard(string question)
    {
        var card = new Card { Question = question };
        using (var dbContext = new ConnectedContext())
        {
            dbContext.Add(card);
            await dbContext.SaveChangesAsync(); 
        }
        //Avoid full db refresh upon adding a new greaterWish
        Cards.Add(card);
        CardEntry = "";
    }

    private bool AddCardCanExecute()
    {
        return !string.IsNullOrEmpty(CardEntry);
    }

    public void CrudUpdateAllCards()
    {
        RefreshCardsFromDB();
    }

    [RelayCommand]
    private void RefreshCardsFromDB(ConnectedContext? context=null)
    {
        Cards.Clear();
        using (var dbContext = context??new ConnectedContext())
        {
            foreach (var dbGreaterWish in dbContext.Cards)
            {
                Cards.Add(dbGreaterWish);
            }
        } 
    }

    [RelayCommand]
    private async Task EditCard(Card card)
    {
        Trace.WriteLine($"Editing {card}");

        // Affiche un popup pour demander la modification
        // /!\ court-circuite MVVM mais toléré pour ne pas ajouter plus de complexité pour l'instant /!\
        string updatedDefinition = await Shell.Current.DisplayPromptAsync(title: "Modifier ", message: "", placeholder: card.Question);

        //Si l'utilisateur n'appuie pas sur Cancel
        if(updatedDefinition!=null)
        {
            using (var dbContext = new ConnectedContext())
            {
                //TODO : Faire la mise à jour uniquement si la definition a changé
                await dbContext.Cards
                    .Where(dbGreaterWish => dbGreaterWish.Id== card.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(dbWish => dbWish.Question, updatedDefinition));


                // Rafraîchissement de la liste locale
                RefreshCardsFromDB(dbContext);
            }
        }
    }

    [RelayCommand]
    private async Task DeleteCard(Card card)
    {
        Trace.WriteLine($"Deleting {card}");
        using (var dbContext = new ConnectedContext())
        {
            await dbContext.Cards
                .Where(dbCard => dbCard.Id == card.Id)
                .ExecuteDeleteAsync();

            // Rafraîchissement de la liste locale
            RefreshCardsFromDB(dbContext);
        }
    }
}

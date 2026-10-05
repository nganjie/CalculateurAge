using CalculateurAge.Views;

namespace CalculateurAge
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Gestionnaire appele au clic du bouton Calculer.
        // sender = le controle clique ; e = donnees de l evenement.
        private async void OnCalculerClicked(object? sender, EventArgs e)
        {
            // Validation : on refuse un nom vide.
            if (string.IsNullOrWhiteSpace(entryNom.Text))
            {
                await DisplayAlert("Erreur", "Entrez un nom", "OK");
                return; // on sort sans rien calculer
            }

            DateTime d = pickerDate.Date ?? DateTime.Today;
            int age = DateTime.Today.Year - d.Year;
            // Si l anniversaire n est pas encore passe cette annee,
            // on retire une annee.
            if (d.Date > DateTime.Today.AddYears(-age)) age--;

            await Shell.Current.GoToAsync(
                $"{nameof(ResultatPage)}?nom={entryNom.Text}&age={age}");
        }
    }
}

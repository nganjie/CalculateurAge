namespace CalculateurAge.ViewModels;

// Contient l ETAT de l ecran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
    // Champs prives : la vraie donnee.
    private string _nom = "";
    private DateTime _dateNaissance
        = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _statut = "";

    // Proprietes publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set { if (SetField(ref _nom, value))
                  CalculerCommand.Rafraichir(); }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (SetField(ref _dateNaissance, value))
            {
                OnPropertyChanged(nameof(DateFuture));
                CalculerCommand.Rafraichir();
            }
        }
    }

    // Vrai si la date choisie est apres aujourd hui.
    public bool DateFuture => DateNaissance.Date > DateTime.Today;

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    // "Majeur" ou "Mineur" selon l age calcule.
    public string Statut
    {
        get => _statut;
        set => SetField(ref _statut, value);
    }

    // Liee a Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }

    // Remet tous les champs a zero.
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom) && !DateFuture);
        EffacerCommand = new RelayCommand(Effacer);
    }

    // La logique metier : aucun controle d interface ici.
    private void Calculer()
    {
        int age = DateTime.Today.Year
                  - DateNaissance.Year;
        if (DateNaissance.Date >
            DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Statut = age >= 18 ? "Majeur" : "Mineur";
        ResultatVisible = true;
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Statut = "";
        ResultatVisible = false;
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using MVVMHobby.Model;

namespace MVVMHobby.ViewModel;

[Table("Hobbies")]
public class HobbyVM : ObservableObject
{
    private string categorieValue;
    private string activiteitValue;
    private string symboolValue;
    private int IdValue;

    public HobbyVM(Hobby nHobby)
    {
        Categorie = nHobby.Categorie;
        Activiteit = nHobby.Activiteit;
        Symbool = nHobby.Symbool;
    }

    public HobbyVM()
    {
    }

    public string Categorie
    {
        get => categorieValue;
        set => SetProperty(ref categorieValue, value);
    }

    public string Activiteit
    {
        get => activiteitValue;
        set => SetProperty(ref activiteitValue, value);
    }

    public string Symbool
    {
        get => symboolValue;
        set => SetProperty(ref symboolValue, value);
    }

    public int Id
    {
        get => IdValue;
        set => SetProperty(ref IdValue, value);
    }

    
}
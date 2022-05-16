using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MVVMHobby.ViewModel;

public class HobbyVM : ObservableObject
{
    public HobbyVM(string nCategorie, string nActiviteit, BitmapImage nSymbool)
    {
        Categorie = nCategorie;
        Activiteit = nActiviteit;
        Symbool = nSymbool;
    }
    private string categorieValue;
    private string activiteitValue;
    private BitmapImage symboolValue;
    public string Categorie
    {
        get { return categorieValue; }
        set => SetProperty(ref categorieValue, value);
    }
    public string Activiteit
    {
        get { return activiteitValue; }
        set => SetProperty(ref activiteitValue, value);
    }
    public BitmapImage Symbool
    {
        get { return symboolValue; }
        set => SetProperty(ref symboolValue, value);
    }
}
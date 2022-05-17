using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MVVMHobby.Model;
using MVVMHobby.View;

namespace MVVMHobby.ViewModel;

public class HobbyLijstVM : ObservableObject
{
    private ObservableCollection<HobbyVM> hobbyLijst = new();
    private HobbyVM selectedHobby;
    private ImageView groteView;
    public HobbyContext context = new();

    public HobbyLijstVM()
    {
        context.Database.EnsureCreated();
        if (context.Hobbies.Count() == 0)
        {
            context.Hobbies.Add(new HobbyVM(new Hobby("sport", "voetbal",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/voetbal.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("sport", "atletiek",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/atletiek.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("sport", "basketbal",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/basketbal.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("sport", "tennis",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/tennis.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("sport", "turnen",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/turnen.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("muziek", "trompet",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/trompet.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("muziek", "drum",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/drum.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("muziek", "gitaar",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/gitaar.jpg")))));
            context.Hobbies.Add(new HobbyVM(new Hobby("muziek", "piano",
                Convert.ToBase64String(File.ReadAllBytes(@"Images/piano.jpg")))));
            context.SaveChanges();
        }

        HobbyLijst = new ObservableCollection<HobbyVM>((from h in context.Hobbies
            select h).ToList());

        VerwijderCommand = new RelayCommand(Verwijder);

        MouseDownEvent = new RelayCommand<MouseEventArgs>(x => MouseDown(x));
        MouseUpEvent = new RelayCommand<MouseEventArgs>(x => MouseUp(x));
    }

    public ObservableCollection<HobbyVM> HobbyLijst
    {
        get => hobbyLijst;
        set => SetProperty(ref hobbyLijst, value);
    }

    public HobbyVM SelectedHobby
    {
        get => selectedHobby;
        set => SetProperty(ref selectedHobby, value);
    }

    public ICommand VerwijderCommand { get; }
    public ICommand MouseDownEvent { get; }
    public ICommand MouseUpEvent { get; }

    private void MouseDown(MouseEventArgs e)
    {
        var tg = (Image) e.OriginalSource;
        groteView = new ImageView();
        groteView.GroteImage.Source = tg.Source;
        groteView.Show();
    }

    private void MouseUp(MouseEventArgs e)
    {
        if (groteView != null)
        {
            groteView.Close();
        }

        groteView = null;
    }

    private void Verwijder()
    {
        HobbyLijst.Remove(SelectedHobby);
    }
}
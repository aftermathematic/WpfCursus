using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MVVMHobby.View;

namespace MVVMHobby.ViewModel;

public class HobbyLijstVM : ObservableObject
{
    private ObservableCollection<HobbyVM> hobbyLijst = new();
    private HobbyVM selectedHobby;
    private ImageView groteView;

    public HobbyLijstVM()
    {
        HobbyLijst.Add(new HobbyVM("sport", "voetbal",
            new BitmapImage(new Uri("pack://application:,,,/Images/voetbal.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("sport", "atletiek",
            new BitmapImage(new Uri("pack://application:,,,/Images/atletiek.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("sport", "basketbal",
            new BitmapImage(new Uri("pack://application:,,,/Images/basketbal.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("sport", "tennis",
            new BitmapImage(new Uri("pack://application:,,,/Images/tennis.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("sport", "turnen",
            new BitmapImage(new Uri("pack://application:,,,/Images/turnen.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("muziek", "trompet",
            new BitmapImage(new Uri("pack://application:,,,/Images/trompet.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("muziek", "drum",
            new BitmapImage(new Uri("pack://application:,,,/Images/drum.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("muziek", "gitaar",
            new BitmapImage(new Uri("pack://application:,,,/Images/gitaar.jpg",
                UriKind.Absolute))));
        HobbyLijst.Add(new HobbyVM("muziek", "piano",
            new BitmapImage(new Uri("pack://application:,,,/Images/piano.jpg",
                UriKind.Absolute))));

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
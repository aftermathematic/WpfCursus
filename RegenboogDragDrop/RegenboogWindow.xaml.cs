using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RegenboogDragDrop;

/// <summary>
///     Interaction logic for WindowRegenboog.xaml
/// </summary>
public partial class RegenboogWindow : Window
{
    private Rectangle sleeprechthoek = new();

    public RegenboogWindow()
    {
        InitializeComponent();
    }

    private void ButtonCheck_Click(object sender, RoutedEventArgs e)
    {
        bool kleurcheck = true;

        foreach (Rectangle rechthoek in DropZone.Children)
        {
            
            var naam = rechthoek.Name.Substring(4);
            var naamkleur = (Brush) new BrushConverter().ConvertFromString(naam);
            var kleur = rechthoek.Fill;
            if (naamkleur == kleur)
            {
                rechthoek.Stroke = Brushes.Green;
            }
            else
            {
                rechthoek.Stroke = Brushes.Red;
                kleurcheck = false;
            }
        }


        if (kleurcheck == true)
        {

            MessageBox.Show("Alles juist!");

        }
    }

    private void Rectangle_DragEnter(object sender, DragEventArgs e)
    {
        var kader = (Rectangle) sender;
        kader.StrokeThickness = 5;
    }

    private void Rectangle_DragLeave(object sender, DragEventArgs e)
    {
        var kader = (Rectangle) sender;
        kader.StrokeThickness = 3;
    }

    private void Rectangle_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent("deKleur"))
        {
            var gesleepteKleur = (Brush) e.Data.GetData("deKleur");
            var rechthoek = (Rectangle) sender;
            if (rechthoek.Fill == Brushes.White)
            {
                rechthoek.Fill = gesleepteKleur;
                sleeprechthoek.Fill = Brushes.White;
            }

            rechthoek.StrokeThickness = 3;
        }
    }

    private void Rectangle_MouseMove(object sender, MouseEventArgs e)
    {
        sleeprechthoek = (Rectangle) sender;
        if (e.LeftButton == MouseButtonState.Pressed &&
            sleeprechthoek.Fill != Brushes.White)
        {
            var sleepKleur = new DataObject("deKleur",
                sleeprechthoek.Fill);
            DragDrop.DoDragDrop(sleeprechthoek, sleepKleur,
                DragDropEffects.Move);
        }
    }
}
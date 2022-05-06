using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Bars;

/// <summary>
///     Interaction logic for BarWindow.xaml
/// </summary>
public partial class BarWindow : Window
{
    public static RoutedCommand mijnRouteCtrlB = new();
    public static RoutedCommand mijnRouteCtrlI = new();

    public BarWindow()
    {
        InitializeComponent();

        var mijnCtrlB =
            new CommandBinding(mijnRouteCtrlB, ctrlBExecuted);
        CommandBindings.Add(mijnCtrlB);
        var toetsCtrlB =
            new KeyGesture(Key.B, ModifierKeys.Control);
        var mijnKeyCtrlB =
            new KeyBinding(mijnRouteCtrlB, toetsCtrlB);
        InputBindings.Add(mijnKeyCtrlB);
        var mijnCtrlI =
            new CommandBinding(mijnRouteCtrlI, ctrlIExecuted);
        CommandBindings.Add(mijnCtrlI);

        var toetsCtrlI =
            new KeyGesture(Key.I, ModifierKeys.Control);
        var mijnKeyCtrlI =
            new KeyBinding(mijnRouteCtrlI, toetsCtrlI);
        InputBindings.Add(mijnKeyCtrlI);
    }

    private void ctrlBExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        Vet_Aan_Uit();
    }

    private void ctrlIExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        Schuin_Aan_Uit();
    }

    private void Lettertype_Click(object sender, RoutedEventArgs e)
    {
        var hetLettertype = (MenuItem) sender;
        foreach (MenuItem huidig in Fontjes.Items)
        {
            huidig.IsChecked = false;
        }

        hetLettertype.IsChecked = true;
        //TextBoxVoorbeeld.FontFamily =
        //    new FontFamily(hetLettertype.Header.ToString());
        LettertypeComboBox.SelectedItem = new FontFamily(hetLettertype.Header.ToString());
    }

    private void LettertypeComboBox_SelectionChanged(object sender,
        SelectionChangedEventArgs e)
    {
        foreach (MenuItem huidig in Fontjes.Items)
        {
            if (LettertypeComboBox.SelectedItem.ToString() ==
                huidig.Header.ToString())
            {
                huidig.IsChecked = true;
            }
            else
            {
                huidig.IsChecked = false;
            }
        }
    }

    private void MenuSchuin_Click(object sender, RoutedEventArgs e)
    {
        Schuin_Aan_Uit();
    }

    private void MenuVet_Click(object sender, RoutedEventArgs e)
    {
        Vet_Aan_Uit();
    }

    private void Schuin_Aan_Uit()
    {
        if (TextBoxVoorbeeld.FontStyle == FontStyles.Normal)
        {
            TextBoxVoorbeeld.FontStyle = FontStyles.Italic;
            MenuSchuin.IsChecked = true;
            ButtonSchuin.IsChecked = true;
        }
        else
        {
            TextBoxVoorbeeld.FontStyle = FontStyles.Normal;
            MenuSchuin.IsChecked = false;
            ButtonSchuin.IsChecked = false;
        }
    }

    private void Vet_Aan_Uit()
    {
        if (TextBoxVoorbeeld.FontWeight == FontWeights.Normal)
        {
            TextBoxVoorbeeld.FontWeight = FontWeights.Bold;
            MenuVet.IsChecked = true;
            ButtonVet.IsChecked = true;
        }
        else
        {
            TextBoxVoorbeeld.FontWeight = FontWeights.Normal;
            MenuVet.IsChecked = false;
            ButtonVet.IsChecked = false;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LettertypeComboBox.Items.SortDescriptions.Add(new SortDescription("Source", ListSortDirection.Ascending));
        LettertypeComboBox.SelectedItem = new FontFamily(TextBoxVoorbeeld.FontFamily.ToString());
    }
}
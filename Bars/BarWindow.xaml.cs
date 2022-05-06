using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace Bars;

/// <summary>
///     Interaction logic for BarWindow.xaml
/// </summary>
public partial class BarWindow : Window
{
    public static RoutedCommand mijnRouteCtrlB = new();
    public static RoutedCommand mijnRouteCtrlI = new();
    private readonly double A4breedte = 21 / 2.54 * 96;
    private readonly double A4hoogte = 29.7 / 2.54 * 96;
    private double vertPositie;

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

    private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        try
        {
            var dlg = new OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents |*.txt";
            if (dlg.ShowDialog() == true)

            {
                using (var bestand = new StreamReader(dlg.FileName))
                {
                    LettertypeComboBox.SelectedValue =
                        new FontFamily(bestand.ReadLine());
                    var convertBold =
                        TypeDescriptor.GetConverter(typeof(FontWeight));
                    TextBoxVoorbeeld.FontWeight =
                        (FontWeight) convertBold.ConvertFromString(bestand.ReadLine());
                    Vet_Aan_Uit(true);
                    var convertStyle =
                        TypeDescriptor.GetConverter(typeof(FontStyle));
                    TextBoxVoorbeeld.FontStyle =
                        (FontStyle) convertStyle.ConvertFromString(bestand.ReadLine());
                    Schuin_Aan_Uit(true);
                    TextBoxVoorbeeld.Text = bestand.ReadLine();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("openen mislukt : " + ex.Message);
        }
    }

    private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        PrintDialog afdrukken = new PrintDialog();
        if (afdrukken.ShowDialog() == true)
        {
            afdrukken.PrintDocument(StelAfdrukSamen().DocumentPaginator,
                "tekstbox");
        }
    }

    private TextBlock Regel(string tekst)
    {
        var deRegel = new TextBlock();
        deRegel.Text = tekst;
        deRegel.FontSize = TextBoxVoorbeeld.FontSize;
        deRegel.FontFamily = TextBoxVoorbeeld.FontFamily;
        deRegel.FontWeight = TextBoxVoorbeeld.FontWeight;
        deRegel.FontStyle = TextBoxVoorbeeld.FontStyle;
        deRegel.Margin = new Thickness(96, vertPositie, 96, 96);
        vertPositie += 30;
        return deRegel;
    }

    private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        try
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents |*.txt";
            if (dlg.ShowDialog() == true)
            {
                using (var bestand = new StreamWriter(dlg.FileName))
                {
                    bestand.WriteLine(LettertypeComboBox.SelectedValue);
                    bestand.WriteLine(TextBoxVoorbeeld.FontWeight.ToString());
                    bestand.WriteLine(TextBoxVoorbeeld.FontStyle.ToString());

                    bestand.WriteLine(TextBoxVoorbeeld.Text);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("opslaan mislukt: " + ex.Message);
        }
    }

    private void Schuin_Aan_Uit(bool wissel = false)
    {
        if (wissel
            && TextBoxVoorbeeld.FontStyle == FontStyles.Italic
            || wissel == false
            && TextBoxVoorbeeld.FontStyle == FontStyles.Normal)
        {
            TextBoxVoorbeeld.FontStyle = FontStyles.Italic;
            MenuSchuin.IsChecked = true;
            ButtonSchuin.IsChecked = true;
            StatusSchuin.FontStyle = FontStyles.Italic;
        }
        else
        {
            TextBoxVoorbeeld.FontStyle = FontStyles.Normal;
            MenuSchuin.IsChecked = false;
            ButtonSchuin.IsChecked = false;
            StatusSchuin.FontStyle = FontStyles.Normal;
        }
    }

    private FixedDocument StelAfdrukSamen()
    {
        var document = new FixedDocument();
        document.DocumentPaginator.PageSize = new Size(A4breedte, A4hoogte);
        var inhoud = new PageContent();
        document.Pages.Add(inhoud);
        var page = new FixedPage();
        inhoud.Child = page;
        page.Width = A4breedte;
        page.Height = A4hoogte;
        vertPositie = 96;
        page.Children.Add(Regel("gebruikt lettertype : " +
                                TextBoxVoorbeeld.FontFamily));
        page.Children.Add(Regel("gewicht van het lettertype : " +
                                TextBoxVoorbeeld.FontWeight));
        page.Children.Add(Regel("stijl van het lettertype : " +
                                TextBoxVoorbeeld.FontStyle));
        page.Children.Add(Regel(""));
        page.Children.Add(Regel("inhoud van de tekstbox : "));
        for (var i = 0; i < TextBoxVoorbeeld.LineCount; i++)
        {
            page.Children.Add(Regel(TextBoxVoorbeeld.GetLineText(i)));
        }

        return document;
    }

    private void Vet_Aan_Uit(bool wissel = false)
    {
        if (wissel
            && TextBoxVoorbeeld.FontWeight == FontWeights.Bold
            || wissel == false
            && TextBoxVoorbeeld.FontWeight == FontWeights.Normal)
        {
            TextBoxVoorbeeld.FontWeight = FontWeights.Bold;
            MenuVet.IsChecked = true;
            StatusVet.FontWeight = FontWeights.Bold;
            ButtonVet.IsChecked = true;
        }
        else
        {
            TextBoxVoorbeeld.FontWeight = FontWeights.Normal;
            MenuVet.IsChecked = false;
            StatusVet.FontWeight = FontWeights.Normal;
            ButtonVet.IsChecked = false;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LettertypeComboBox.Items.SortDescriptions.Add(new SortDescription("Source", ListSortDirection.Ascending));
        LettertypeComboBox.SelectedItem = new FontFamily(TextBoxVoorbeeld.FontFamily.ToString());
    }

    private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        Afdrukvoorbeeld preview = new();
        preview.Owner = this;
        preview.AfdrukDocument = StelAfdrukSamen();
        preview.ShowDialog();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        if (MessageBox.Show("Programma afsluiten ?",
                "Afsluiten",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.No)
            e.Cancel = true;
    }

    private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        Close();
    }
}
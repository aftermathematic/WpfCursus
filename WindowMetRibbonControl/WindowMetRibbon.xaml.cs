using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using WindowMetRibbonControl.Properties;

namespace WindowMetRibbonControl;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class WindowMetRibbon
{
    public WindowMetRibbon()
    {
        InitializeComponent();
        LeesMRU();

        if (Settings.Default.qat != null)
        {
            var qatlijst =
                Settings.Default.qat;
            var lijnnr = 0;
            while (lijnnr < qatlijst.Count)
            {
                var commando = qatlijst[lijnnr];
                var png = qatlijst[lijnnr + 1];
                var nieuweKnop = new RibbonButton();
                var icon = new BitmapImage();
                icon.BeginInit();
                icon.UriSource = new Uri(png);
                icon.EndInit();
                nieuweKnop.SmallImageSource = icon;
                var ccol = CommandBindings;
                foreach (CommandBinding cb in ccol)
                {
                    var rcb = (RoutedUICommand) cb.Command;
                    if (rcb.Text == commando)
                    {
                        nieuweKnop.Command = rcb;
                    }
                }

                Qat.Items.Add(nieuweKnop);
                lijnnr += 2;
            }
        }
    }

    private void BijwerkenMRU(string bestandsnaam)
    {
        System.Collections.Specialized.StringCollection mrulijst = new
            System.Collections.Specialized.StringCollection();
        if (WindowMetRibbonControl.Properties.Settings.Default.mru != null)
        {
            mrulijst = WindowMetRibbonControl.Properties.Settings.Default.mru;
            int positie = mrulijst.IndexOf(bestandsnaam);
            if (positie >= 0)
            {
                mrulijst.RemoveAt(positie);
            }
            else
            {
                if (mrulijst.Count >= 6) mrulijst.RemoveAt(5);
            }
        }

        mrulijst.Insert(0, bestandsnaam);
        WindowMetRibbonControl.Properties.Settings.Default.mru = mrulijst;
        WindowMetRibbonControl.Properties.Settings.Default.Save();
        LeesMRU();
    }

    private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        Close();
    }

    private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show("Dit is helpscherm", "Help", MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.OK);
    }

    private void LeesBestand(string bestandsnaam)
    {
        try
        {
            using (var bestand = new StreamReader(bestandsnaam))
            {
                TextBoxVoorbeeld.Text = bestand.ReadLine();
            }

            BijwerkenMRU(bestandsnaam);
        }
        catch (Exception ex)
        {
            MessageBox.Show("openen mislukt : " + ex.Message);
        }
    }

    private void LeesMRU()
    {
        MRUGalleryCat.Items.Clear();
        if (Settings.Default.mru != null)
        {
            var mrulijst =
                Settings.Default.mru;
            for (var lijnnr = 0; lijnnr < mrulijst.Count; lijnnr++)
            {
                MRUGalleryCat.Items.Add(mrulijst[lijnnr]);
            }
        }
    }

    private void MRUGallery_SelectionChanged(object sender,
        RoutedPropertyChangedEventArgs<object> e)
    {
        LeesBestand(MRUGallery.SelectedValue.ToString());
    }

    private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        TextBoxVoorbeeld.Text = string.Empty;
    }

    private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        var dlg = new OpenFileDialog();
        dlg.DefaultExt = ".rvb";
        dlg.Filter = "Ribbon documents |*.rvb";

        if (dlg.ShowDialog() == true)
        {
            LeesBestand(dlg.FileName);
        }
    }

    private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        MessageBox.Show("Hier zou een afdrukvoorbeeld moeten verschijnen");
    }

    private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        var afdrukken = new PrintDialog();
        if (afdrukken.ShowDialog() == true)
        {
            MessageBox.Show("Hier zou worden afgedrukt");
        }
    }

    private void Radio_Click(object sender, RoutedEventArgs e)
    {
        var keuze = (RibbonRadioButton) sender;
        var bc = new BrushConverter();
        var kleur = (SolidColorBrush) bc.ConvertFromString(keuze.Tag.ToString());
        TextBoxVoorbeeld.Foreground = kleur;
    }

    private void RedoExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        try
        {
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = ".rvb";
            dlg.Filter = "Ribbon documents |*.rvb";

            if (dlg.ShowDialog() == true)
            {
                using (var bestand = new StreamWriter(dlg.FileName))
                {
                    bestand.WriteLine(TextBoxVoorbeeld.Text);
                }
            }

            BijwerkenMRU(dlg.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show("opslaan mislukt : " + ex.Message);
        }
    }

    private void UndoExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        var qatlijst =
            new StringCollection();
        if (Settings.Default.qat != null)
        {
            Settings.Default.qat.Clear();
        }

        foreach (var li in Qat.Items)
        {
            if (li is RibbonButton)
            {
                var knop = (RibbonButton) li;
                var commando = (RoutedUICommand) knop.Command;
                qatlijst.Add(commando.Text);
                qatlijst.Add(knop.SmallImageSource.ToString());
            }
        }

        if (qatlijst.Count > 0)
        {
            Settings.Default.qat = qatlijst;
        }

        Settings.Default.Save();
    }
}

public class BooleanToFontWeight : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        if ((bool) value)
        {
            return "Bold";
        }

        return "Normal";
    }

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        return null;
    }
}

public class BooleanToFontStyle : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        if ((bool) value)
        {
            return "Italic";
        }

        return "Normal";
    }

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        return null;
    }
}
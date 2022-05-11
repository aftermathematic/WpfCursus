using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WindowMetRibbonControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowMetRibbon
    {
        public WindowMetRibbon()
        {
            InitializeComponent();
        }

        private void LeesBestand(string bestandsnaam)
        {
            try
            {
                using (StreamReader bestand = new StreamReader(bestandsnaam))
                {
                    TextBoxVoorbeeld.Text = bestand.ReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".rvb";
            dlg.Filter = "Ribbon documents |*.rvb";

            if (dlg.ShowDialog() == true)
            {
                LeesBestand(dlg.FileName);
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".rvb";
                dlg.Filter = "Ribbon documents |*.rvb";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(TextBoxVoorbeeld.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : " + ex.Message);
            }
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TextBoxVoorbeeld.Text = string.Empty;
        }

        private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog afdrukken = new PrintDialog();
            if (afdrukken.ShowDialog() == true)
            {
                MessageBox.Show("Hier zou worden afgedrukt");
            }
        }

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Hier zou een afdrukvoorbeeld moeten verschijnen");
        }

        private void HelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Dit is helpscherm", "Help", MessageBoxButton.OK, MessageBoxImage.Information,
                MessageBoxResult.OK);
        }

        private void MRUGallery_SelectionChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            LeesBestand(MRUGallery.SelectedValue.ToString());
        }

        private void Radio_Click(object sender, RoutedEventArgs e)
        {
            RibbonRadioButton keuze = (RibbonRadioButton) sender;
            BrushConverter bc = new BrushConverter();
            SolidColorBrush kleur = (SolidColorBrush) bc.ConvertFromString(keuze.Tag.ToString());
            TextBoxVoorbeeld.Foreground = kleur;
        }
    }



    public class BooleanToFontWeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if ((Boolean)value)
                return "Bold";
            else
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
            if ((Boolean)value)
                return "Italic";
            else
                return "Normal";
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            return null;
        }
    }

}
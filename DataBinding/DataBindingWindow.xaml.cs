using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataBindingWindow : Window
    {
        public Persoon persoon = new Persoon("jan", 3500m, new DateTime(2011, 2, 13));
        public DataBindingWindow()
        {
            InitializeComponent();
            SortDescription sd = new SortDescription("Source",
                ListSortDirection.Ascending);
            lettertypeComboBox.Items.SortDescriptions.Add(sd);
            lettertypeComboBox.SelectedItem = new FontFamily("Arial");
            veranderPanel.DataContext = persoon;
        }

        private void toonNaamButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(persoon.Naam);
        }

        private void veranderButton_Click(object sender, RoutedEventArgs e)
        {
            persoon.Naam = "piet";
            persoon.Wedde = 4125.5m;
            persoon.InDienst = new DateTime(2010, 12, 21);
        }

    }
}

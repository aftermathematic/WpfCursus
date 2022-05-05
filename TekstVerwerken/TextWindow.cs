using System.Windows;

namespace TekstVerwerken;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class TextWindow : Window
{
    public TextWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        textBlockAanmelding.TextWrapping = TextWrapping.Wrap;
        textBlockAanmelding.Text = "Je probeerde aan te melden met: " +
                                   textBoxGebruikersnaam.Text + " en paswoord: " + psdBox.Password;
    }
}
using System.Windows;
using System.Windows.Documents;

namespace Bars;

/// <summary>
///     Interaction logic for Afdrukvoorbeeld.xaml
/// </summary>
public partial class Afdrukvoorbeeld : Window
{
    public Afdrukvoorbeeld()
    {
        InitializeComponent();
    }

    public IDocumentPaginatorSource AfdrukDocument
    {
        get => printpreview.Document;
        set => printpreview.Document = value;
    }
}
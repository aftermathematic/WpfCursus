using System.Windows;
using MVVMVoorbeeld.View;
using MVVMVoorbeeld.ViewModel;

namespace MVVMVoorbeeld;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var vm = new TekstMetOpmaakVM();
        var mijnTekstboxView = new TextBoxView();

        mijnTekstboxView.DataContext = vm;
        mijnTekstboxView.Show();
    }
}
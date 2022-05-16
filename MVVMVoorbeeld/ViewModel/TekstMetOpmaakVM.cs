using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;

namespace MVVMVoorbeeld.ViewModel;

public class TekstMetOpmaakVM : ObservableObject
{
    private string inhoudValue;
    private bool vetValue;
    private bool schuinValue;

    public TekstMetOpmaakVM()
    {
        NieuwCommand = new RelayCommand(Nieuw);
        OpenenCommand = new RelayCommand(Openen);
        OpslaanCommand = new RelayCommand(Opslaan);
        AfsluitenCommand = new RelayCommand(Afsluiten);

        AfsluitenEvent = new RelayCommand<CancelEventArgs>(x => Afsluiten(x));
    }

    public string Inhoud
    {
        get => inhoudValue;
        set => SetProperty(ref inhoudValue, value);
    }

    public bool Vet
    {
        get => vetValue;
        set => SetProperty(ref vetValue, value);
    }

    public bool Schuin
    {
        get => schuinValue;
        set => SetProperty(ref schuinValue, value);
    }

    public ICommand NieuwCommand { get; }
    public ICommand OpenenCommand { get; }
    public ICommand OpslaanCommand { get; }
    public ICommand AfsluitenCommand { get; }
    public ICommand AfsluitenEvent { get; }

    private void Afsluiten(CancelEventArgs e)
    {
        if (MessageBox.Show("Afsluiten", "Wilt u het programma sluiten ?",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
            MessageBoxResult.No)
        {
            e.Cancel = true;
        }
    }

    private void Afsluiten()
    {
        Application.Current.MainWindow.Close();
    }

    private void Nieuw()
    {
        Inhoud = string.Empty;
        Vet = false;
        Schuin = false;
    }

    private void Openen()
    {
        try
        {
            var dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = ".box";
            dlg.Filter = "Textbox documents |*.box";
            if (dlg.ShowDialog() == true)
            {
                using (var bestand = new StreamReader(dlg.FileName))
                {
                    Inhoud = bestand.ReadLine();
                    Vet = Convert.ToBoolean(bestand.ReadLine());
                    Schuin = Convert.ToBoolean(bestand.ReadLine());
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("openen mislukt : " + ex.Message);
        }
    }

    private void Opslaan()
    {
        try
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = "tekstbox";
            dlg.DefaultExt = ".box";
            dlg.Filter = "Textbox documents |*.box";
            if (dlg.ShowDialog() == true)
            {
                using (var bestand = new StreamWriter(dlg.FileName))
                {
                    bestand.WriteLine(Inhoud);
                    bestand.WriteLine(Vet.ToString());
                    bestand.WriteLine(Schuin.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("opslaan mislukt : " + ex.Message);
        }
    }
}
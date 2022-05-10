using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace DataBinding;

public class Persoon : ObservableObject
{
    private string naamValue;
    private decimal weddeValue;
    private DateTime inDienstValue;

    public Persoon(string nNaam, decimal nWedde, DateTime nInDienst)
    {
        Naam = nNaam;
        Wedde = nWedde;
        InDienst = nInDienst;
    }

    public string Naam
    {
        get => naamValue;
        set => SetProperty(ref naamValue, value);
    }

    public decimal Wedde
    {
        get => weddeValue;
        set => SetProperty(ref weddeValue, value);
    }

    public DateTime InDienst
    {
        get => inDienstValue;
        set => SetProperty(ref inDienstValue, value);
    }
}
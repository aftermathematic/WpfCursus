namespace MVVMHobby.Model;

public class Hobby
{
    public Hobby(string nCategorie, string nActiviteit, string nSymbool)
    {
        Categorie = nCategorie;
        Activiteit = nActiviteit;
        Symbool = nSymbool;
    }

    public Hobby()
    {
    }

    public int Id { get; set; }
    public string Categorie { get; set; }
    public string Activiteit { get; set; }
    public string Symbool { get; set; }
}
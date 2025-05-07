using System.IO;

namespace AutoverhuurProject.Domein.Models;

internal class Vestiging (Guid id, string luchthaven, string straat, string postcode, string plaats, string land, int lijnNummer) {

    public Guid Id { get; } = id; //readonly property, na aanmaak mag deze niet meer wijzigen

    public string Luchthaven { get; } = 
        String.IsNullOrWhiteSpace(luchthaven)
        ? throw new ArgumentException("Luchthaven moet ingevuld zijn.")
        : luchthaven;

    public string Straat { get; } =
        String.IsNullOrWhiteSpace(straat)
        ? throw new ArgumentException("Straat moet ingevuld zijn.")
        : straat;

    public string Postcode { get; } =       
        String.IsNullOrWhiteSpace(postcode)
        ? throw new ArgumentException("Postcode moet ingevuld zijn.")
        : postcode;

    public string Plaats { get; } = 
        String.IsNullOrWhiteSpace(plaats)
        ? throw new ArgumentException("Plaats moet ingevuld zijn.")
        : plaats;

    public string Land { get; } = 
        String.IsNullOrWhiteSpace(land)
        ? throw new ArgumentException("Land moet ingevuld zijn.")
        : land;

    public int LijnNummer { get; } = lijnNummer;
}

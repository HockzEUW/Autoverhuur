namespace AutoverhuurProject.Domein.Models;

internal class Klant (Guid id, string voornaam, string achternaam, string email, int lijnNummer, string? straat = null, int? postcode = null, string? woonplaats = null, string? land = null) {

    public Guid Id { get; } = id; //readonly property, na aanmaak mag deze niet meer wijzigen

    public string Voornaam { get; } =
        String.IsNullOrWhiteSpace(voornaam)
        ? throw new ArgumentException("Voornaam moet ingevuld zijn")
        : voornaam;

    public string Achternaam { get; } =
        String.IsNullOrWhiteSpace(achternaam)
        ? throw new ArgumentException("Achternaam moet ingevuld zijn")
        : achternaam;

    public string Email { get; } =
        String.IsNullOrWhiteSpace(email)
        ? throw new ArgumentException("Email moet ingevuld zijn")
        : email;

    public int LijnNummer { get; } = lijnNummer;
    public string? Straat { get; set; } = straat;
    public int? Postcode { get; set; } = postcode;
    public string? Woonplaats { get; set; } = woonplaats;
    public string? Land { get; set; } = land;
}

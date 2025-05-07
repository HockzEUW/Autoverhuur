namespace AutoverhuurProject.Domein.DTOs;

public record KlantDto
    (Guid Id, string Voornaam, string Achternaam, string Email, int LijnNummer, string? Straat, int? Postcode, string? Woonplaats, string? Land);

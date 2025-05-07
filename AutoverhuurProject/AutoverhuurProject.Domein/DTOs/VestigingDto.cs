namespace AutoverhuurProject.Domein.DTOs;

public record VestigingDto
    (Guid Id, string Luchthaven, string Straat, string Postcode, string Plaats, string Land, int LijnNummer);
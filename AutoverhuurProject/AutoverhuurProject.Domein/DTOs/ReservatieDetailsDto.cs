namespace AutoverhuurProject.Domein.DTOs;

public record ReservatieDetailsDto
    (Guid Id, string KlantVoornaam, string KlantAchternaam, string KlantAdres, string AutoModel, string VestigingLuchthaven, DateTime StartDatum, DateTime EindDatum, int? AantalPersonen);

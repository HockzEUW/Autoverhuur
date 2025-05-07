using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.DTOs;

public record ReservatieDto
     (Guid Id, Guid KlantId, Guid AutoId, Guid VestigingId, DateTime StartDatum, DateTime EindDatum, int? AantalPersonen = null);
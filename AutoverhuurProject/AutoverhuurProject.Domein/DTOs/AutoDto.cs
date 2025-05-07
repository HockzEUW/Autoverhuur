using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.DTOs;

public record AutoDto
     (Guid Id, string Nummerplaat, string Model, int Zitplaatsen, EMotortype Motortype, int LijnNummer);
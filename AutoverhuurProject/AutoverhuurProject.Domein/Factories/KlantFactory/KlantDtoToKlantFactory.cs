using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories.KlantFactory;

internal static class ReservatieDtoToKlantFactory {
    public static Klant KlantDtoToKlant(KlantDto klantDto) {
        Klant klant = new Klant(
                klantDto.Id,
                klantDto.Voornaam,
                klantDto.Achternaam,
                klantDto.Email,
                klantDto.LijnNummer,
                klantDto.Straat,
                klantDto.Postcode,
                klantDto.Woonplaats,
                klantDto.Land);
        return klant;
    }
}

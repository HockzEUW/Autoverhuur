using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories.KlantFactory;

internal static class KlantToKlantDtoFactory {
    public static KlantDto KlantToKlantDto(Klant klant) {
        KlantDto klantDto = new KlantDto(
                klant.Id,
                klant.Voornaam,
                klant.Achternaam,
                klant.Email,
                klant.LijnNummer,
                klant.Straat,
                klant.Postcode,
                klant.Woonplaats,
                klant.Land);
        return klantDto;
    }
}

using AutoverhuurProject.Domein.DTOs;

namespace AutoverhuurProject.Persistentie.Bestand.Factories;

internal static class KlantDtoFactory {
    
    internal static KlantDto? MapToKlantDto(string klant, int lijnNummer) {
        string[] data = klant.Split(';');

        if (data.Length == 7) {
            string voornaam = data[0];
            string achternaam = data[1];
            string email = data[2];
            string straat = data[3];
            int postcode = int.Parse(data[4]);
            string woonplaats = data[5];
            string land = data[6];
            return new KlantDto(Guid.NewGuid(), voornaam, achternaam, email, lijnNummer, straat, postcode, woonplaats, land);
        } else {
            return null;
        }
    }
}
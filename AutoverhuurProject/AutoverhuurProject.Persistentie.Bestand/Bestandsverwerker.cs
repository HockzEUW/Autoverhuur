using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Exceptions;
using AutoverhuurProject.Persistentie.Bestand.Factories;
using System.IO;

namespace AutoverhuurProject.Persistentie.Bestand;

public static class Bestandsverwerker {
    internal static List<KlantDto> LeesKlantenUitBestand(string bestandLocatie) {
        // ... Lees het bestand, verwerk elke lijn en zet om naar een klantDto lijst
        if (string.IsNullOrWhiteSpace(bestandLocatie)) {
            throw new DomeinException("Geen bestand ingevoerd.");
        }

        List<KlantDto> klanten = new();

        // Verwerk het bestand
        using (StreamReader sr = new StreamReader(bestandLocatie)) {
            string line;
            string[] header = sr.ReadLine().Trim().Split(';');
            string[] headerFormat = { "Voornaam", "Achternaam", "Email", "Straat", "Postcode", "Woonplaats", "Land" };
            int lijnNummer = 2; //header overslaan

            if (!header.SequenceEqual(headerFormat)) {
                throw new DomeinException("Geen geldig klanten bestand ingevoerd.");
            }
            while ((line = sr.ReadLine()) != null) {
                try {
                    KlantDto? nieuweKlant = KlantDtoFactory.MapToKlantDto(line, lijnNummer);
                    if (nieuweKlant != null) {
                        klanten.Add(nieuweKlant);
                    } else {
                        throw new DomeinException("Fout bij het inlezen uit bestand, lijn werd overgeslagen");
                    }
                    } catch (DomeinException e) {
                    ExceptionLogger.LogException(e.Message, Path.GetFileNameWithoutExtension(bestandLocatie), lijnNummer);
                }
                lijnNummer++;
            }
        }
        return klanten;
    }

    internal static List<AutoDto> LeesAutosUitBestanden (string bestandLocatie) {
        // ... Lees het bestand, verwerk elke lijn en zet om naar een autoDto lijst
        if (string.IsNullOrWhiteSpace(bestandLocatie)) {
            throw new DomeinException("Geen bestand ingevoerd.");
        }

        List<AutoDto> autos = new();

        // Verwerk het bestand
        using (StreamReader sr = new StreamReader(bestandLocatie)) {
            string line;
            string[] header = sr.ReadLine().Trim().Split(';') ;
            string[] headerFormat = {"Nummerplaat", "Model", "Zitplaatsen", "Motortype"};
            int lijnNummer = 2; //header overslaan

            if (!header.SequenceEqual(headerFormat)) {
                throw new DomeinException("Geen geldig auto bestand ingevoerd.");
            }

            while ((line = sr.ReadLine()) != null) {
                try {
                    AutoDto? nieuweAuto = AutoDtoFactory.MapToAutoDto(line, lijnNummer);
                    if (nieuweAuto != null) {
                        autos.Add(nieuweAuto);
                    } else {
                        throw new DomeinException("Fout bij het inlezen uit bestand, lijn werd overgeslagen");
                    }
                } catch (DomeinException e) {
                    ExceptionLogger.LogException(e.Message, Path.GetFileNameWithoutExtension(bestandLocatie), lijnNummer);
                }
                lijnNummer++;
            }
        }
        return autos;
    }

    internal static List<VestigingDto> LeesVestigingenUitBestand (string bestandLocatie) {
        // ... Lees het bestand, verwerk elke lijn en zet om naar een vestigingDto lijst
        if (string.IsNullOrWhiteSpace(bestandLocatie)) {
            throw new DomeinException("Geen bestand ingevoerd.");
        }

        List<VestigingDto> vestigingen = new();

        // Verwerk het bestand
        using (StreamReader sr = new StreamReader(bestandLocatie)) {
            string line;
            string[] header = sr.ReadLine().Trim().Split(';');
            string[] headerFormat = { "Luchthaven", "Straat", "Postcode", "Plaats", "Land" };
            int lijnNummer = 2; //header overslaan

            if (!header.SequenceEqual(headerFormat)) {
                throw new DomeinException("Geen geldig vestiging bestand ingevoerd.");
            }

            while ((line = sr.ReadLine()) != null) {
                try {
                    VestigingDto? nieuweVestiging = VestigingDtoFactory.MapToVestigingDto(line, lijnNummer);
                    if (nieuweVestiging != null) {
                        vestigingen.Add(nieuweVestiging);
                    } else {
                        throw new DomeinException("Fout bij het inlezen uit bestand, lijn werd overgeslagen");
                    }
                } catch (DomeinException e) {
                    ExceptionLogger.LogException(e.Message, Path.GetFileNameWithoutExtension(bestandLocatie), lijnNummer);
                }
                lijnNummer++;
            }
        }
        return vestigingen;
    }
}

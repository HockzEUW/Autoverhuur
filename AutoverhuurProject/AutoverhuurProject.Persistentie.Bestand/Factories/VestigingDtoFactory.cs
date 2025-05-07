using AutoverhuurProject.Domein.DTOs;

namespace AutoverhuurProject.Persistentie.Bestand.Factories;

internal static class VestigingDtoFactory {

    internal static VestigingDto? MapToVestigingDto(string vestiging, int lijnNummer) {
        string[] data = vestiging.Split(';');


        if(data.Length == 5) {
            string luchthaven = data[0];
            string straat = data[1];
            string postcode = data[2];
            string plaats = data[3];
            string land = data[4];
            return new VestigingDto(Guid.NewGuid(), luchthaven, straat, postcode, plaats, land, lijnNummer);
        } else {
            return null;
        }
    }
}

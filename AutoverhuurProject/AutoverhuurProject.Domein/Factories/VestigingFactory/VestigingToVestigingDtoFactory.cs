using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories.KlantFactory;

internal static class VestigingToVestigingDtoFactory {
    public static VestigingDto VestigingToVestigingDto(Vestiging vestiging) {
        VestigingDto vestigingDto = new VestigingDto(
            vestiging.Id,
            vestiging.Luchthaven,
            vestiging.Straat,
            vestiging.Postcode,
            vestiging.Plaats,
            vestiging.Land,
            vestiging.LijnNummer);
        return vestigingDto;
    }
}

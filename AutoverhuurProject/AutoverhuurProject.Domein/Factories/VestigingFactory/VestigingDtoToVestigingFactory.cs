using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories;

internal static class VestigingDtoToVestigingFactory {
    public static Vestiging VestigingDtoToVestiging(VestigingDto vestigingDto) {
        Vestiging vestiging = new Vestiging(
            vestigingDto.Id,
            vestigingDto.Luchthaven,
            vestigingDto.Straat,
            vestigingDto.Postcode,
            vestigingDto.Plaats,
            vestigingDto.Land,
            vestigingDto.LijnNummer);
        return vestiging;
    }
}

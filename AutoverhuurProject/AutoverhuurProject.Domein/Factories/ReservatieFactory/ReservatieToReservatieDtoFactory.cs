using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories.KlantFactory;

internal static class ReservatieToReservatieDtoFactory {
    public static ReservatieDto ReservatieToReservatieDto(Reservatie reservatie) {
        ReservatieDto reservatieDto = new ReservatieDto(
                reservatie.Id,
                reservatie.Klant,
                reservatie.Auto,
                reservatie.Vestiging,
                reservatie.StartDatum,
                reservatie.EindDatum,
                reservatie.AantalPersonen);
        return reservatieDto;
    }
}

using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories.KlantFactory;

internal static class ReservatieDtoToReservatieFactory {
    public static Reservatie ReservatieDtoToReservatie(ReservatieDto reservatieDto) {
        Reservatie reservatie = new Reservatie(
                reservatieDto.Id,
                reservatieDto.KlantId,
                reservatieDto.AutoId,
                reservatieDto.VestigingId,
                reservatieDto.StartDatum,
                reservatieDto.EindDatum,
                reservatieDto.AantalPersonen);
        return reservatie;
    }
}

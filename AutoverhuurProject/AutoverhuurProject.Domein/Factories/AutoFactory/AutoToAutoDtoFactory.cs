using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories.KlantFactory;

internal static class AutoToAutoDtoFactory {
    public static AutoDto AutoToAutoDto(Auto auto) {
        AutoDto autoDto = new AutoDto(
                auto.Id,
                auto.Nummerplaat,
                auto.Model,
                auto.Zitplaatsen,
                auto.Motortype,
                auto.LijnNummer);
        return autoDto;
    }
}

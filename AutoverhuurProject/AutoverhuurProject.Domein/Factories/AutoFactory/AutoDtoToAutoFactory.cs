using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Factories;

internal static class AutoDtoToAutoFactory {
    public static Auto AutoDtoToAuto(AutoDto autoDto) {
        Auto auto = new Auto(
                autoDto.Id,
                autoDto.Nummerplaat,
                autoDto.Model,
                autoDto.Zitplaatsen,
                autoDto.Motortype,
                autoDto.LijnNummer);
        return auto;
    }
}

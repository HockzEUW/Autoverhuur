using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Models;
using System;

namespace AutoverhuurProject.Persistentie.Bestand.Factories;

internal static class AutoDtoFactory {

    internal static AutoDto? MapToAutoDto(string auto, int lijnNummer) {
        string[] data = auto.Split(';');

        if(data.Length == 4) {
            string nummerplaat = data[0];
            string model = data[1];
            int zitplaatsen = int.Parse(data[2]);
            EMotortype motortype = (EMotortype) Enum.Parse(typeof(EMotortype), data[3]);
            return new AutoDto(Guid.NewGuid(), nummerplaat, model, zitplaatsen, motortype, lijnNummer);
        } else {
            return null;
        }
    }
}

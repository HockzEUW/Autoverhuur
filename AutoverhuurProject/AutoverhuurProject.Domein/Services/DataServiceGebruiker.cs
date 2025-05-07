using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Factories.KlantFactory;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Domein.Models;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace AutoverhuurProject.Domein.Services;

class DataServiceGebruiker {
    private IKlantRepositoryFull _klantRepoDb;
    private IAutoRepositoryFull _autoRepoDb;
    private IVestigingRepositoryFull _vestigingRepoDb;
    private IReservatieRepositoryFull _reservatieRepoDb;
    public DataServiceGebruiker(IKlantRepositoryFull klantRepoDb, IAutoRepositoryFull autoRepoDb, IVestigingRepositoryFull vestigingRepoDb, IReservatieRepositoryFull reservatieRepoDb) {
        _klantRepoDb = klantRepoDb;
        _autoRepoDb = autoRepoDb;
        _vestigingRepoDb = vestigingRepoDb;
        _reservatieRepoDb = reservatieRepoDb;
    }

    public IEnumerable<KlantDto> ZoekKlanten(string zoekopdracht) {
        List<KlantDto> klantenDto = new();

        foreach (var klant in _klantRepoDb.GetAll()) {
            if (klant.Voornaam.ToLower().Contains(zoekopdracht.ToLower()) || klant.Achternaam.ToLower().Contains(zoekopdracht.ToLower())) {
                klantenDto.Add(klant);
            }
        }
        return klantenDto;
    }

    internal void MaakReservatie(KlantDto klantDto, AutoDto autoDto, VestigingDto vestigingDto, DateTime startDatum, DateTime eindDatum, int? aantalPersonen) {
        Reservatie reservatie = new Reservatie(Guid.NewGuid(), klantDto.Id, autoDto.Id, vestigingDto.Id, startDatum, eindDatum, aantalPersonen);
        ReservatieDto reservatieDto = ReservatieToReservatieDtoFactory.ReservatieToReservatieDto(reservatie);
        _reservatieRepoDb.Add(reservatieDto);
    }

    internal IEnumerable<AutoDto> ZoekBeschikbareAutos(string vestigingId, DateTime startDatum, DateTime eindDatum) {
        return _autoRepoDb.ZoekBeschikbareAutos(vestigingId, startDatum, eindDatum);
    }

    public void GenereerAsciiDoc(IEnumerable<AutoDto> autos, string vestiging, DateTime startDatum, DateTime eindDatum) {
        var sb = new StringBuilder();
        sb.AppendLine("= Overzicht auto's");
        sb.AppendLine($"Vestiging: {vestiging}");
        sb.AppendLine($"Tijdstip: {startDatum} - {eindDatum}");
        sb.AppendLine();

        foreach (var auto in autos) {
            sb.AppendLine($"== {auto.Nummerplaat} - {auto.Model}");
            var vorigeReservatie = GeefVorigeReservatie(auto.Id, startDatum.ToString("yyyy-MM-dd"));
            var volgendeReservatie = GeefVolgendeReservatie(auto.Id, eindDatum.ToString("yyyy-MM-dd"));

            if (vorigeReservatie != null) {
                sb.AppendLine("=== Vorige reservatie");
                sb.AppendLine($"Klant: {vorigeReservatie.KlantVoornaam} {vorigeReservatie.KlantAchternaam} +");
                sb.AppendLine($"Adres: {vorigeReservatie.KlantAdres} +");
                sb.AppendLine($"Starttijd: {vorigeReservatie.StartDatum} +");
                sb.AppendLine($"Eindtijd: {vorigeReservatie.EindDatum}  +");
            }

            if (volgendeReservatie != null) {
                sb.AppendLine("=== Volgende reservatie");
                sb.AppendLine($"Klant: {volgendeReservatie.KlantVoornaam} {volgendeReservatie.KlantAchternaam} +");
                sb.AppendLine($"Adres: {volgendeReservatie.KlantAdres} +");
                sb.AppendLine($"Starttijd: {volgendeReservatie.StartDatum} +");
                sb.AppendLine($"Eindtijd: {volgendeReservatie.EindDatum} +");
            }

            sb.AppendLine();
        }

        // Schrijf het asciidoc document
        var filePath = "../../../../OverzichtAutos.adoc";
        File.WriteAllText(filePath, sb.ToString());
    }


    internal ReservatieDetailsDto GeefVorigeReservatie(Guid autoId, string datum) {
        return _reservatieRepoDb.GeefVorigeReservatie(autoId, datum);
    }

    internal ReservatieDetailsDto GeefVolgendeReservatie(Guid autoId, string datum) {
        return _reservatieRepoDb.GeefVolgendeReservatie(autoId, datum);
    }

}

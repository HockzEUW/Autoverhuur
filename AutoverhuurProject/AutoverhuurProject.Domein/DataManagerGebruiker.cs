using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Domein.Services;

namespace AutoverhuurProject.Domein;

public class DataManagerGebruiker (IKlantRepositoryFull klantRepoDb, IAutoRepositoryFull autoRepoDb, IVestigingRepositoryFull vestigingRepoDb, IReservatieRepositoryFull reservatieRepoDb){

    private DataServiceGebruiker _dataServiceGebruiker = new(klantRepoDb, autoRepoDb, vestigingRepoDb, reservatieRepoDb);
    public IEnumerable<KlantDto> GeefKlanten() {
        return klantRepoDb.GetAll();
    }
    public IEnumerable<KlantDto> ZoekKlanten(string zoekopdracht) {
        return _dataServiceGebruiker.ZoekKlanten(zoekopdracht);
    }
    public IEnumerable<AutoDto> GeefAutos() {
        return autoRepoDb.GetAll();
    }
    public IEnumerable<AutoDto> ZoekBeschikbareAutos(string vestigingId, DateTime startDatum, DateTime eindDatum) {
        return _dataServiceGebruiker.ZoekBeschikbareAutos(vestigingId, startDatum, eindDatum);
    }
    public IEnumerable<VestigingDto> GeefVestigingen() {
        return vestigingRepoDb.GetAll();
    }
    public void MaakReservatie(KlantDto klantDto, AutoDto autoDto, VestigingDto vestigingDto, DateTime startDatum, DateTime eindDatum, int? aantalPersonen) {
        _dataServiceGebruiker.MaakReservatie(klantDto, autoDto, vestigingDto, startDatum, eindDatum, aantalPersonen);
    }
    public IEnumerable<ReservatieDetailsDto> GeefReservatiesDetails(string klantNaam, string vestigingId, string datumReservatie) {
        return reservatieRepoDb.GeefReservatiesDetails(klantNaam, vestigingId, datumReservatie);
    }
    public void DeleteReservatie(Guid id) {
        reservatieRepoDb.DeleteReservatie(id);
    }

    public void GenereerAsciiDoc(IEnumerable<AutoDto> autos, string vestiging, DateTime startDatum, DateTime eindDatum) {
        _dataServiceGebruiker.GenereerAsciiDoc(autos, vestiging, startDatum, eindDatum);
    }
}

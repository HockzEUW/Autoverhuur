using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Factories;
using AutoverhuurProject.Domein.Factories.KlantFactory;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Services;

internal class DataService(
    IKlantRepositoryRead klantRepo, IAutoRepositoryRead autoRepo, IVestigingRepositoryRead vestigingRepo,
    IKlantRepositoryFull klantRepoDb, IAutoRepositoryFull autoRepoDb, IVestigingRepositoryFull vestigingRepoDb, IReservatieRepositoryFull reservatieRepoDb) {

    public void MaakDatabaseTablesLeeg() {
        reservatieRepoDb.DeleteAll();
        klantRepoDb.DeleteAll();
        autoRepoDb.DeleteAll();
        vestigingRepoDb.DeleteAll();
    }

    public int ImporteerKlantenUitBestandNaarDb() {
        foreach (var klant in klantRepo.GetAll()) {
            Klant klantObject = ReservatieDtoToKlantFactory.KlantDtoToKlant(klant);
            KlantDto klantDto = KlantToKlantDtoFactory.KlantToKlantDto(klantObject);
            klantRepoDb.Add(klant);
        }
        return klantRepoDb.GetAll().Count();
    }

    public int ImporteerAutosUitBestandNaarDb() {
        foreach (var auto in autoRepo.GetAll()) {
            Auto autoObject = AutoDtoToAutoFactory.AutoDtoToAuto(auto);
            AutoDto autoDto = AutoToAutoDtoFactory.AutoToAutoDto(autoObject);
            autoRepoDb.Add(autoDto);  
        }
        return autoRepoDb.GetAll().Count();
    }

    public int ImporteerVestigingenUitBestandNaarDb() {
        foreach (var vestiging in vestigingRepo.GetAll()) {
            Vestiging vestigingObject = VestigingDtoToVestigingFactory.VestigingDtoToVestiging(vestiging);
            VestigingDto vestigingDto = VestigingToVestigingDtoFactory.VestigingToVestigingDto(vestigingObject);
            vestigingRepoDb.Add(vestigingDto);
        }
        return vestigingRepoDb.GetAll().Count();
    }
}

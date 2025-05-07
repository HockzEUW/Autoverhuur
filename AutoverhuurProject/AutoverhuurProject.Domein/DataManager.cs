using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Domein.Services;

namespace AutoverhuurProject.Domein; 
public class DataManager (
    IKlantRepositoryRead klantRepo, IAutoRepositoryRead autoRepo, IVestigingRepositoryRead vestigingRepo,
    IKlantRepositoryFull klantRepoDb, IAutoRepositoryFull autoRepoDb, IVestigingRepositoryFull vestigingRepoDb, IReservatieRepositoryFull reservatieRepoDb) {

    private DataService _dataService = new DataService(klantRepo, autoRepo, vestigingRepo, klantRepoDb, autoRepoDb, vestigingRepoDb, reservatieRepoDb);

    public void MaakDatabaseTablesLeeg() {
        _dataService.MaakDatabaseTablesLeeg();
    }

    public int ImporteerKlantenUitBestandNaarDb() {
        return _dataService.ImporteerKlantenUitBestandNaarDb();
    }

    public int ImporteerAutosUitBestandNaarDb() {
        return _dataService.ImporteerAutosUitBestandNaarDb();
    }

    public int ImporteerVestigingenUitBestandNaarDb() {
        return _dataService.ImporteerVestigingenUitBestandNaarDb();
    }
}
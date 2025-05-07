using AutoverhuurProject.Domein.DTOs;

namespace AutoverhuurProject.Domein.Interfaces;

public interface IAutoRepositoryRead {
    IEnumerable<AutoDto> GetAll();
}

public interface IAutoRepositoryFull : IAutoRepositoryRead {
    void Add(AutoDto auto);
    void DeleteAll();
    //public IEnumerable<AutoDto> GetAutosByVestigingId(string vestigingId);
    IEnumerable<AutoDto> ZoekBeschikbareAutos(string vestigingId, DateTime startDatum, DateTime eindDatum);
}

using AutoverhuurProject.Domein.DTOs;

namespace AutoverhuurProject.Domein.Interfaces;

public interface IKlantRepositoryRead {
    IEnumerable<KlantDto> GetAll();
}

public interface IKlantRepositoryFull : IKlantRepositoryRead {
    void Add(KlantDto klant);
    void DeleteAll();
}

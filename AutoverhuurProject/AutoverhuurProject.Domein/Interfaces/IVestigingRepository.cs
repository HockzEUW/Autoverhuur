using AutoverhuurProject.Domein.DTOs;

namespace AutoverhuurProject.Domein.Interfaces;

public interface IVestigingRepositoryRead {
    IEnumerable<VestigingDto> GetAll();
}

public interface IVestigingRepositoryFull : IVestigingRepositoryRead {
    void Add(VestigingDto vestiging);
    void DeleteAll();
}

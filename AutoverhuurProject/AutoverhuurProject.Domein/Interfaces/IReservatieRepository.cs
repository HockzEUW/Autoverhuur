using AutoverhuurProject.Domein.DTOs;

namespace AutoverhuurProject.Domein.Interfaces;

public interface IReservatieRepositoryFull {
    void Add(ReservatieDto reservatieDto);
    void DeleteReservatie(Guid id);
    void DeleteAll();
    public IEnumerable<ReservatieDto> GetAll();
    IEnumerable<ReservatieDetailsDto> GeefReservatiesDetails(string klantNaam, string vestigingId, string datumReservatie);
    ReservatieDetailsDto GeefVolgendeReservatie(Guid autoId, string datum);
    ReservatieDetailsDto GeefVorigeReservatie(Guid autoId, string datum);
}
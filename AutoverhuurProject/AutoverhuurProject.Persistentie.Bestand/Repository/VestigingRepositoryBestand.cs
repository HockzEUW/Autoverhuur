using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Interfaces;

namespace AutoverhuurProject.Persistentie.Bestand.Repository;

public class VestigingRepositoryBestand : IVestigingRepositoryRead {
    private List<VestigingDto> _vestigingen;

    public VestigingRepositoryBestand(string pad) {
        _vestigingen = Bestandsverwerker.LeesVestigingenUitBestand(pad);
    }

    public IEnumerable<VestigingDto> GetAll() {
        return _vestigingen;
    }
}

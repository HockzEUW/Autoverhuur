using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Interfaces;

namespace AutoverhuurProject.Persistentie.Bestand.Repository;

public class KlantRepositoryBestand : IKlantRepositoryRead {
    private List<KlantDto> _klanten;

    public KlantRepositoryBestand(string pad) {
        _klanten = Bestandsverwerker.LeesKlantenUitBestand(pad);
    }

    public IEnumerable<KlantDto> GetAll() {
        return _klanten;
    }
}

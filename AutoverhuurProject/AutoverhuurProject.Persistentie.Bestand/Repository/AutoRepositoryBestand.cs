using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Interfaces;

namespace AutoverhuurProject.Persistentie.Bestand.Repository;

public class AutoRepositoryBestand : IAutoRepositoryRead {
    private List<AutoDto> _autos;

    public AutoRepositoryBestand(string pad) {
        _autos = Bestandsverwerker.LeesAutosUitBestanden(pad);
    }

    public IEnumerable<AutoDto> GetAll() {
        return _autos;
    }
}

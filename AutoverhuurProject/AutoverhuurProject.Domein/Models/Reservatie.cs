using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Exceptions;

namespace AutoverhuurProject.Domein.Models;

internal class Reservatie (Guid id, Guid klantId, Guid autoId, Guid vestigingId, DateTime startDatum, DateTime eindDatum, int? aantalPersonen = null) {

    public Guid Id { get; } = id; //readonly property, na aanmaak mag deze niet meer wijzigen

    public Guid Klant { get; } = klantId;

    public Guid Auto { get; } = autoId;

    public Guid Vestiging { get; } = vestigingId;

    public DateTime StartDatum { get; } =
        startDatum < DateTime.Today.AddDays(1)
        ? throw new DomeinException("Startdatum moet minstens morgen beginnen.")
        : startDatum;

    public DateTime EindDatum { get; } =
        eindDatum < startDatum.AddDays(1)
        ? throw new DomeinException("Einddatum moet minstens 1 dag later dan de startdatum zijn.")
        : eindDatum;
    public int? AantalPersonen { get; } =
        aantalPersonen <= 0
        ? throw new DomeinException("Opgegeven aantal zitplaatsen moet groter dan 0 zijn.")
        : aantalPersonen;

   
}

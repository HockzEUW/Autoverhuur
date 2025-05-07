namespace AutoverhuurProject.Domein.Models;

internal class Auto (Guid id, string nummerplaat, string model, int zitplaatsen, EMotortype motortype, int lijnNummer) {

    public Guid Id { get; } = id; //readonly property, na aanmaak mag deze niet meer wijzigen

    public string Nummerplaat { get; } =
        String.IsNullOrWhiteSpace(nummerplaat)
        ? throw new ArgumentException("Nummerplaat moet ingevuld zijn.")
        : nummerplaat;

    public string Model { get; } =
        String.IsNullOrWhiteSpace(model)
        ? throw new ArgumentException("Model moet ingevuld zijn.")
        : model;

    public int Zitplaatsen { get; } =
        zitplaatsen < 2 //minstens 2 zitplaatsen
        ? throw new ArgumentOutOfRangeException("Er moeten minstens 2 zitplaatsen zijn.")
        : zitplaatsen;

    public EMotortype Motortype { get; } = motortype;

    public int LijnNummer { get; } = lijnNummer;
}

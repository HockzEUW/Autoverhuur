using AutoverhuurProject.Domein.Exceptions;
using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Tests;

public class ReservatieTests {

    [Fact]
    public void Tests_Constructor_ReservatieWordtCorrectAangemaakt_GeldigeWaarde() {
        // Arrange
        var id = Guid.NewGuid();
        var klantId = Guid.NewGuid();
        var autoId = Guid.NewGuid();
        var vestigingId = Guid.NewGuid();
        var startDatum = DateTime.Now.AddDays(1);
        var eindDatum = DateTime.Now.AddDays(2);
        var aantalPersonen = 4;
    
        // Act
        var reservatie = new Reservatie(id, klantId, autoId, vestigingId, startDatum, eindDatum, aantalPersonen);
    
        // Assert
        Assert.Equal(id, reservatie.Id);
        Assert.Equal(klantId, reservatie.Klant);
        Assert.Equal(autoId, reservatie.Auto);
        Assert.Equal(vestigingId, reservatie.Vestiging);
        Assert.Equal(startDatum, reservatie.StartDatum);
        Assert.Equal(eindDatum, reservatie.EindDatum);
        Assert.Equal(aantalPersonen, reservatie.AantalPersonen);
    }

    [Fact]
    public void Tests_Constructor_ReservatieWordtCorrectAangemaakt_ZonderAantalPersonen() {
        // Arrange
        var id = Guid.NewGuid();
        var klantId = Guid.NewGuid();
        var autoId = Guid.NewGuid();
        var vestigingId = Guid.NewGuid();
        var startDatum = DateTime.Now.AddDays(1);
        var eindDatum = DateTime.Now.AddDays(2);

        // Act
        var reservatie = new Reservatie(id, klantId, autoId, vestigingId, startDatum, eindDatum);

        // Assert
        Assert.Equal(id, reservatie.Id);
        Assert.Equal(klantId, reservatie.Klant);
        Assert.Equal(autoId, reservatie.Auto);
        Assert.Equal(vestigingId, reservatie.Vestiging);
        Assert.Equal(startDatum, reservatie.StartDatum);
        Assert.Equal(eindDatum, reservatie.EindDatum);
        Assert.Null(reservatie.AantalPersonen);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Theory]
    [InlineData("2023-01-01", "2023-01-01")]
    [InlineData("2023-01-02", "2023-01-01")]
    [InlineData("2025-03-29", "2025-03-29")]
    [InlineData("2025-03-30", "2025-03-30")]
    [InlineData("2025-04-02", "2025-04-02")]
    public void Tests_Constructor_OngeldigeDatums_ArgumentException(string startDatum, string eindDatum) {
        // Arrange   
        Assert.Throws<DomeinException>(() => new Reservatie(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse(startDatum), DateTime.Parse(eindDatum), 4));
    }

    [Theory]
    [InlineData("2025-03-30", "2025-03-31")] //wijzigen op examen naar volgende dag!
    [InlineData("2025-03-31", "2025-04-01")]
    [InlineData("2025-04-02", "2025-04-08")]
    public void Tests_Constructor_GeldigeDatums_Succes(string startDatum, string eindDatum) {
        // Arrange
        var reservatie = new Reservatie(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse(startDatum), DateTime.Parse(eindDatum), 4);

        // Act & Assert
        Assert.Equal(DateTime.Parse(startDatum), reservatie.StartDatum);
        Assert.Equal(DateTime.Parse(eindDatum), reservatie.EindDatum);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Theory]
    [InlineData(-4)]
    [InlineData(-1)]
    [InlineData(0)]
    public void Tests_Constructor_OngeldigAantalPersonen_ArgumentException(int aantalPersonen) {
        // Arrange   
        Assert.Throws<DomeinException>(() => new Reservatie(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse("2025-05-03"), DateTime.Parse("2025-05-09"), aantalPersonen));
    }

    [Theory]
    [InlineData(1)] //wijzigen op examen!
    [InlineData(5)]
    [InlineData(13)]
    public void Tests_Constructor_GeldigAantalPersonen_Succes(int aantalPersonen) {
        // Arrange
        var reservatie = new Reservatie(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.Parse("2025-05-03"), DateTime.Parse("2025-05-09"), aantalPersonen);

        // Act & Assert
        Assert.Equal(aantalPersonen, reservatie.AantalPersonen);
    }

}
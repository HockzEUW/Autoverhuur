using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Tests; 
public class AutoTests {
    [Fact]
    public void Tests_Constructor_AutoWordtCorrectAangemaakt_GeldigeWaarde() {
        // Arrange
        var id = Guid.NewGuid();
        var nummerplaat = "1-ABC-123";
        var model = "Toyota Corolla";
        var zitplaatsen = 5;
        var motortype = EMotortype.Benzine;
        int lijnNummer = 2;

        // Act
        var auto = new Auto(id, nummerplaat, model, zitplaatsen, motortype, lijnNummer);

        // Assert
        Assert.Equal(id, auto.Id);
        Assert.Equal(nummerplaat, auto.Nummerplaat);
        Assert.Equal(model, auto.Model);
        Assert.Equal(zitplaatsen, auto.Zitplaatsen);
        Assert.Equal(motortype, auto.Motortype);
        Assert.Equal(lijnNummer, auto.LijnNummer);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void Tests_Constructor_OngeldigeNummerplaat_ArgumentException(string nummerplaat) {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), nummerplaat, "Tesla Model Y", 5, EMotortype.Elektrisch, 2));
    }

    [Theory]
    [InlineData("1-ABC-123")]
    [InlineData("2-XYZ-999")]
    [InlineData("T-TXI-091")]
    [InlineData("Z-GRG-002")]
    public void Tests_Constructor_GeldigeNummerplaat_Succes(string nummerplaat) {
        // Arrange
        var auto = new Auto(Guid.NewGuid(), nummerplaat, "Tesla Model Y", 5, EMotortype.Elektrisch, 2);

        // Act & Assert
        Assert.Equal(nummerplaat, auto.Nummerplaat);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void Tests_Constructor_OngeldigModel_ArgumentException(string model) {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Auto(Guid.NewGuid(), "1-ABC-123", model, 5, EMotortype.Elektrisch, 2));
    }

    [Theory]
    [InlineData("Volvo XC60")]
    [InlineData("Nissan Leaf")]
    [InlineData("Volvo V60")]
    [InlineData("BMW X5")]
    public void Tests_Constructor_GeldigModel_Succes(string model) {
        // Act & Assert
        var auto = new Auto(Guid.NewGuid(), "1-ABC-123", model, 5, EMotortype.Elektrisch, 2);

        // Act & Assert
        Assert.Equal(model, auto.Model);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    [Theory]
    [InlineData(-23)]
    [InlineData(-1)]
    [InlineData(1)]
    public void Tests_Constructor_OngeldigAantalZitplaatsen_ArgumentOutOfRangeException(int zitplaatsen) {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Auto(Guid.NewGuid(), "1-ABC-123", "Tesla Model Y", zitplaatsen, EMotortype.Elektrisch, 2));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(9)]
    public void Tests_Constructor_GeldigAantalZitplaatsen_Succes(int zitplaatsen) {
        // Act & Assert
        var auto = new Auto(Guid.NewGuid(), "1-ABC-123", "Tesla Model Y", zitplaatsen, EMotortype.Elektrisch, 2);

        // Act & Assert
        Assert.Equal(zitplaatsen, auto.Zitplaatsen);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
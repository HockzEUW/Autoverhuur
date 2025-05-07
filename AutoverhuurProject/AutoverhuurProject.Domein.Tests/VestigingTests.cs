using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Tests {
    public class VestigingTests {
        [Fact]
        public void Tests_Constructor_VestigingWordtCorrectAangemaakt_GeldigeWaarde() {
            // Arrange
            var id = Guid.NewGuid();
            var luchthaven = "Schiphol";
            var straat = "Evert van de Beekstraat 202";
            var postcode = "1118 CP";
            var plaats = "Amsterdam";
            var land = "Nederland";
            int lijnNummer = 2;

            // Act
            var vestiging = new Vestiging(id, luchthaven, straat, postcode, plaats, land, lijnNummer);

            // Assert
            Assert.Equal(id, vestiging.Id);
            Assert.Equal(luchthaven, vestiging.Luchthaven);
            Assert.Equal(straat, vestiging.Straat);
            Assert.Equal(postcode, vestiging.Postcode);
            Assert.Equal(plaats, vestiging.Plaats);
            Assert.Equal(land, vestiging.Land);
            Assert.Equal(lijnNummer, vestiging.LijnNummer);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigLuchthaven_ArgumentException(string luchthaven) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Vestiging(Guid.NewGuid(), luchthaven, "Evert van de Beekstraat 202", "1118 CP", "Amsterdam", "Nederland", 2));
        }

        [Theory]
        [InlineData("Schiphol")]
        [InlineData("Charles de Gaulle")]
        [InlineData("Frankfurt am Main")]
        [InlineData("Barajas")]
        public void Tests_Constructor_GeldigeLuchthaven_Succes(string luchthaven) {
            // Arrange
            var vestiging = new Vestiging(Guid.NewGuid(), luchthaven, "Evert van de Beekstraat 202", "1118 CP", "Amsterdam", "Nederland", 2);

            // Act & Assert
            Assert.Equal(luchthaven, vestiging.Luchthaven);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigeStraat_ArgumentException(string straat) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Vestiging(Guid.NewGuid(), "Schiphol", straat, "1118 CP", "Amsterdam", "Nederland", 2));
        }

        [Theory]
        [InlineData("Evert van de Beekstraat 202")]
        [InlineData("Rue des Halles 95700")]
        [InlineData("Flughafenstraße 60549")]
        [InlineData("Avda. de la Hispanidad")]
        public void Tests_Constructor_GeldigeStraat_Succes(string straat) {
            // Arrange
            var vestiging = new Vestiging(Guid.NewGuid(), "Schiphol", straat, "1118 CP", "Amsterdam", "Nederland", 2);

            // Act & Assert
            Assert.Equal(straat, vestiging.Straat);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigePostcode_ArgumentException(string postcode) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Vestiging(Guid.NewGuid(), "Evert van de Beekstraat 202", "1118 CP", postcode, "Amsterdam", "Nederland", 2));
        }

        [Theory]
        [InlineData("1118 CP")]
        [InlineData("Roissy-en-France")]
        [InlineData("Frankfurt am Main")]
        [InlineData("28042")]
        public void Tests_Constructor_GeldigePostcode_Succes(string postcode) {
            // Arrange
            var vestiging = new Vestiging(Guid.NewGuid(), "Schiphol", "Evert van de Beekstraat 202", postcode, "Amsterdam", "Nederland", 2);

            // Act & Assert
            Assert.Equal(postcode, vestiging.Postcode);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigePlaats_ArgumentException(string plaats) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Vestiging(Guid.NewGuid(), "Schiphol", "Evert van de Beekstraat 202", "1118 CP", plaats, "Nederland", 2));
        }

        [Theory]
        [InlineData("Amsterdam")]
        [InlineData("Parijs")]
        [InlineData("Madrid")]
        [InlineData("Barcelona")]
        public void Tests_Constructor_GeldigePlaats_Succes(string plaats) {
            // Arrange
            var vestiging = new Vestiging(Guid.NewGuid(), "Schiphol", "Evert van de Beekstraat 202", "1118 CP", plaats, "Nederland", 2);

            // Act & Assert
            Assert.Equal(plaats, vestiging.Plaats);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigLand_ArgumentException(string land) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Vestiging(Guid.NewGuid(), "Schiphol", "Evert van de Beekstraat 202", "1118 CP", "Amsterdam", land, 2));
        }

        [Theory]
        [InlineData("Spanje")]
        [InlineData("Nederland")]
        [InlineData("Italië")]
        [InlineData("België")]
        public void Tests_Constructor_GeldigLand_Succes(string land) {
            // Arrange
            var vestiging = new Vestiging(Guid.NewGuid(), "Schiphol", "Evert van de Beekstraat 202", "1118 CP", "Amsterdam", land, 2);

            // Act & Assert
            Assert.Equal(land, vestiging.Land);
        }
    }
}
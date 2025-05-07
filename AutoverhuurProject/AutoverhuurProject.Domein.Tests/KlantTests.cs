using AutoverhuurProject.Domein.Models;

namespace AutoverhuurProject.Domein.Tests {
    public class KlantTests {
        [Fact]
        public void Tests_Constructor_KlantWordtCorrectAangemaakt_GeldigeWaardeMetNulls() {
            // Arrange
            var id = Guid.NewGuid();
            var voornaam = "Sophie";
            var achternaam = "Peeters";
            var email = "sophie.peeters@example.com";
            var straat = "Hoofdweg 139";
            var postcode = 3755; 
            var woonplaats = "Amsterdam";
            var land = "Nederland";
            var lijnNummer = 2;

            // Act
            var klant = new Klant(id, voornaam, achternaam, email, lijnNummer, straat, postcode, woonplaats, land);

            // Assert
            Assert.Equal(id, klant.Id);
            Assert.Equal(voornaam, klant.Voornaam);
            Assert.Equal(achternaam, klant.Achternaam);
            Assert.Equal(email, klant.Email);
            Assert.Equal(lijnNummer, klant.LijnNummer);
            Assert.Equal(straat, klant.Straat);
            Assert.Equal(postcode, klant.Postcode);
            Assert.Equal(woonplaats, klant.Woonplaats);
            Assert.Equal(land, klant.Land);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact]
        public void Tests_Constructor_KlantWordtCorrectAangemaakt_GeldigeWaardeZonderNulls() {
            // Arrange
            var id = Guid.NewGuid();
            var voornaam = "Sophie";
            var achternaam = "Peeters";
            var email = "sophie.peeters@example.com";
            var lijnNummer = 2;

            // Act
            var klant = new Klant(id, voornaam, achternaam, email, 2);

            // Assert
            Assert.Equal(id, klant.Id);
            Assert.Equal(voornaam, klant.Voornaam);
            Assert.Equal(achternaam, klant.Achternaam);
            Assert.Equal(email, klant.Email);
            Assert.Equal(lijnNummer, klant.LijnNummer);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigeVoornaam_ArgumentException(string voornaam) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Klant(Guid.NewGuid(), voornaam, "Jansen", "jan.jansen@example.com", 2));
        }

        [Theory]
        [InlineData("Jan")]
        [InlineData("Piet")]
        [InlineData("Klaas")]
        public void Tests_Constructor_GeldigeVoornaam_Succes(string voornaam) {
            // Arrange
            var klant = new Klant(Guid.NewGuid(), voornaam, "Jansen", "jan.jansen@example.com", 2);

            // Act & Assert
            Assert.Equal(voornaam, klant.Voornaam);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigeAchternaam_ArgumentException(string achternaam) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Klant(Guid.NewGuid(), "Jan", achternaam, "jan.jansen@example.com", 2));
        }

        [Theory]
        [InlineData("Jansen")]
        [InlineData("Pietersen")]
        [InlineData("Klaassen")]
        public void Tests_Constructor_GeldigeAchternaam_Succes(string achternaam) {
            // Arrange
            var klant = new Klant(Guid.NewGuid(), "Jan", achternaam, "jan.jansen@example.com", 2);

            // Act & Assert
            Assert.Equal(achternaam, klant.Achternaam);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Tests_Constructor_OngeldigEmail_ArgumentException(string email) {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Klant(Guid.NewGuid(), "Jan", "Jansen", email, 2));
        }

        [Theory]
        [InlineData("jan.jansen@example.com")]
        [InlineData("piet.pietersen@example.com")]
        [InlineData("klaas.klaassen@example.com")]
        public void Tests_Constructor_GeldigEmail_Succes(string email) {
            // Arrange
            var klant = new Klant(Guid.NewGuid(), "Jan", "Jansen", email, 2);

            // Act & Assert
            Assert.Equal(email, klant.Email);
        }
    }
}
using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Exceptions;
using AutoverhuurProject.Domein.Interfaces;
using Microsoft.Data.SqlClient;

namespace AutoverhuurProject.Persistentie.Db;

public class KlantRepositoryDb : IKlantRepositoryFull {
    private readonly string _connectionString;

    public KlantRepositoryDb(string connectionString) {
        _connectionString = connectionString;
    }

    public void Add(KlantDto klantDto) {
        try {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Voeg product toe aan Product-tabel
            const string klantQuery = @"
            INSERT INTO Klanten (Id, Voornaam, Achternaam, Email, Straat, Postcode, Woonplaats, Land, LijnNummer)
            VALUES (@Id, @Voornaam, @Achternaam, @Email, @Straat, @Postcode, @Woonplaats, @Land, @LijnNummer)";

            using var klantCommand = new SqlCommand(klantQuery, connection);
            klantCommand.Parameters.AddWithValue("@Id", klantDto.Id);
            klantCommand.Parameters.AddWithValue("@Voornaam", klantDto.Voornaam);
            klantCommand.Parameters.AddWithValue("@Achternaam", klantDto.Achternaam);
            klantCommand.Parameters.AddWithValue("@Email", klantDto.Email);
            klantCommand.Parameters.AddWithValue("@Straat", klantDto.Straat);
            klantCommand.Parameters.AddWithValue("@Postcode", klantDto.Postcode);
            klantCommand.Parameters.AddWithValue("@Woonplaats", klantDto.Woonplaats);
            klantCommand.Parameters.AddWithValue("@Land", klantDto.Land);
            klantCommand.Parameters.AddWithValue("@LijnNummer", klantDto.LijnNummer);
            klantCommand.ExecuteNonQuery();
        } catch (Exception ex) {
            ExceptionLogger.LogException(ex.Message, "Klanten", klantDto.LijnNummer);
        }
    }

    public IEnumerable<KlantDto> GetAll() {
        List<KlantDto> klanten = new();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            SELECT k.Id, k.Voornaam, k.Achternaam, k.Email, k.Straat, k.Postcode, k.Woonplaats, k.Land, k.LijnNummer
            FROM Klanten k";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var klant = MapProductFromReader(reader);
            if (klant != null) {
                klanten.Add(klant);
            }
        }

        return klanten;
    }

    private static KlantDto? MapProductFromReader(SqlDataReader reader) {
        Guid id = Guid.Parse(reader.GetString(0));
        string voornaam = reader.GetString(1);
        string achternaam = reader.GetString(2);
        string email = reader.GetString(3);
        string straat = reader.GetString(4);
        int postcode = int.Parse(reader.GetString(5));
        string woonplaats = reader.GetString(6);
        string land = reader.GetString(7);
        int lijnNummer = reader.GetInt32(8);

        KlantDto klantDto = new(
            id,
            voornaam,
            achternaam,
            email,
            lijnNummer,
            straat,
            postcode,
            woonplaats,
            land);
        return klantDto;
    }

    public void DeleteAll() {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            DELETE
            FROM Klanten";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();
    }


}

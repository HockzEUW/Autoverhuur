using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Domein.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AutoverhuurProject.Persistentie.Db;

public class ReservatieRepositoryDb : IReservatieRepositoryFull {
    private readonly string _connectionString;

    public ReservatieRepositoryDb(string connectionString) {
        _connectionString = connectionString;
    }

    public void Add(ReservatieDto reservatieDto) {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        const string reservatieQuery = @"
        INSERT INTO Reservaties (Id, KlantId, AutoId, VestigingId, StartDatum, EindDatum, AantalPersonen)
        VALUES (@Id, @KlantId, @AutoId, @VestigingId, @StartDatum, @EindDatum, @AantalPersonen)";

        using var reservatieCommand = new SqlCommand(reservatieQuery, connection);
        reservatieCommand.Parameters.AddWithValue("@Id", reservatieDto.Id);
        reservatieCommand.Parameters.AddWithValue("@KlantId", reservatieDto.KlantId);
        reservatieCommand.Parameters.AddWithValue("@AutoId", reservatieDto.AutoId);
        reservatieCommand.Parameters.AddWithValue("@VestigingId", reservatieDto.VestigingId);
        reservatieCommand.Parameters.AddWithValue("@StartDatum", reservatieDto.StartDatum);
        reservatieCommand.Parameters.AddWithValue("@EindDatum", reservatieDto.EindDatum);
        reservatieCommand.Parameters.AddWithValue("@AantalPersonen", reservatieDto.AantalPersonen ?? (object)DBNull.Value);
        reservatieCommand.ExecuteNonQuery();
    }

    public IEnumerable<ReservatieDto> GetAll() {
        List<ReservatieDto> reservatiesDto = new();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            SELECT r.Id, r.KlantId, r.AutoId, r.VestigingId, r.StartDatum, r.EindDatum, r.AantalPersonen
            FROM Reservaties r";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var reservatie = MapReservatieFromReader(reader);
            if (reservatie != null) {
                reservatiesDto.Add(reservatie);
            }
        }

        return reservatiesDto;
    }
    private static ReservatieDto? MapReservatieFromReader(SqlDataReader reader) {
        Guid id = Guid.Parse(reader.GetString(0));
        Guid klantId = Guid.Parse(reader.GetString(1));
        Guid autoId = Guid.Parse(reader.GetString(2));
        Guid vestigingId = Guid.Parse(reader.GetString(3));
        DateTime startDatum = reader.GetDateTime(4);
        DateTime eindDatum = reader.GetDateTime(5);
        int? aantalPersonen = int.TryParse(reader.GetString(6), out int tempAantalPersonen) ? tempAantalPersonen : (int?)null;


        ReservatieDto reservatieDto = new(
            id,
            klantId,
            autoId,
            vestigingId,
            startDatum,
            eindDatum,
            aantalPersonen);
        return reservatieDto;
    }
    public IEnumerable<ReservatieDetailsDto> GeefReservatiesDetails(string klantNaam, string vestigingId, string datumReservatie) {
        List<ReservatieDetailsDto> reservatieDetails = new();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = @"
        SELECT r.Id, k.Voornaam, k.Achternaam, k.Straat, k.Postcode, k.Woonplaats, k.Land, a.Model, v.Luchthaven, r.StartDatum, r.EindDatum, r.AantalPersonen
        FROM Reservaties r
        JOIN Klanten k ON r.KlantId = k.Id
        JOIN Autos a ON r.AutoId = a.Id
        JOIN Vestigingen v ON r.VestigingId = v.Id
        WHERE (k.Voornaam LIKE @KlantNaam OR k.Achternaam LIKE @KlantNaam)
        AND v.Id = @VestigingId
        AND @DatumReservatie BETWEEN r.StartDatum AND r.EindDatum";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@KlantNaam", "%" + klantNaam + "%");
        command.Parameters.AddWithValue("@VestigingId", vestigingId);
        command.Parameters.AddWithValue("@DatumReservatie", DateTime.Parse(datumReservatie));

        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var reservatieDetail = MapReservatieDetailsFromReader(reader);
            if (reservatieDetail != null) {
                reservatieDetails.Add(reservatieDetail);
            }
        }
        return reservatieDetails;
    }




    private static ReservatieDetailsDto? MapReservatieDetailsFromReader(SqlDataReader reader) {
        Guid id = Guid.Parse(reader.GetString(0));
        string klantVoornaam = reader.GetString(1);
        string klantAchternaam = reader.GetString(2);
        string klantStraat = reader.GetString(3);
        string klantPostcode = reader.GetString(4);
        string klantWoonplaats = reader.GetString(5);
        string klantLand = reader.GetString(6);
        string klantAdres = $"{klantStraat}, {klantPostcode} {klantWoonplaats}, {klantLand}";
        string autoModel = reader.GetString(7);
        string vestigingLuchthaven = reader.GetString(8);
        DateTime startDatum = reader.GetDateTime(9);
        DateTime eindDatum = reader.GetDateTime(10);
        int? aantalPersonen = reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11);

        ReservatieDetailsDto reservatieDetailsDto = new(
            id,
            klantVoornaam,
            klantAchternaam,
            klantAdres, // Voeg dit veld toe
            autoModel,
            vestigingLuchthaven,
            startDatum,
            eindDatum,
            aantalPersonen
        );
        return reservatieDetailsDto;
    }


    public void DeleteReservatie(Guid id) {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        const string query = @"
            DELETE
            FROM Reservaties
            WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }

    public void DeleteAll() {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            DELETE
            FROM Reservaties";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();
    }

    public ReservatieDetailsDto GeefVorigeReservatie(Guid autoId, string datum) {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
        SELECT TOP 1 r.Id, k.Voornaam, k.Achternaam, k.Straat, k.Postcode, k.Woonplaats, k.Land, a.Model, v.Luchthaven, r.StartDatum, r.EindDatum, r.AantalPersonen
        FROM Reservaties r
        JOIN Klanten k ON r.KlantId = k.Id
        JOIN Autos a ON r.AutoId = a.Id
        JOIN Vestigingen v on r.VestigingId = v.Id
        WHERE r.AutoId = @AutoId AND r.EindDatum < @Datum
        ORDER BY r.EindDatum DESC";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@AutoId", autoId);
        command.Parameters.AddWithValue("@Datum", DateTime.Parse(datum));

        using var reader = command.ExecuteReader();
        if (reader.Read()) {
            return MapReservatieDetailsFromReader(reader);
        }

        return null;
    }

    public ReservatieDetailsDto GeefVolgendeReservatie(Guid autoId, string datum) {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
        SELECT TOP 1 r.Id, k.Voornaam, k.Achternaam, k.Straat, k.Postcode, k.Woonplaats, k.Land, a.Model, v.Luchthaven, r.StartDatum, r.EindDatum, r.AantalPersonen
        FROM Reservaties r
        JOIN Klanten k ON r.KlantId = k.Id
        JOIN Autos a ON r.AutoId = a.Id
        JOIN Vestigingen v on r.VestigingId = v.Id
        WHERE r.AutoId = @AutoId AND r.StartDatum > @Datum
        ORDER BY r.StartDatum ASC";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@AutoId", autoId);
        command.Parameters.AddWithValue("@Datum", DateTime.Parse(datum));

        using var reader = command.ExecuteReader();
        if (reader.Read()) {
            return MapReservatieDetailsFromReader(reader);
        }

        return null;
    }

}

using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Exceptions;
using AutoverhuurProject.Domein.Interfaces;
using Microsoft.Data.SqlClient;

namespace AutoverhuurProject.Persistentie.Db;

public class VestigingRepositoryDb : IVestigingRepositoryFull {
    private readonly string _connectionString;

    public VestigingRepositoryDb(string connectionString) {
        _connectionString = connectionString;
    }

    public void Add(VestigingDto vestigingDto) {
        try {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();


            const string vestigingQuery = @"
            INSERT INTO Vestigingen (Id, Luchthaven, Straat, Postcode, Plaats, Land, LijnNummer)
            VALUES (@Id, @Luchthaven, @Straat, @Postcode, @Plaats, @Land, @LijnNummer)";

            using var klantCommand = new SqlCommand(vestigingQuery, connection);
            klantCommand.Parameters.AddWithValue("@Id", vestigingDto.Id);
            klantCommand.Parameters.AddWithValue("@Luchthaven", vestigingDto.Luchthaven);
            klantCommand.Parameters.AddWithValue("@Straat", vestigingDto.Straat);
            klantCommand.Parameters.AddWithValue("@Postcode", vestigingDto.Postcode);
            klantCommand.Parameters.AddWithValue("@Plaats", vestigingDto.Plaats);
            klantCommand.Parameters.AddWithValue("@Land", vestigingDto.Land);
            klantCommand.Parameters.AddWithValue("@LijnNummer", vestigingDto.LijnNummer);
            klantCommand.ExecuteNonQuery();
        } catch (Exception ex) {
            throw new DomeinException($"Fout bij het importeren van auto (lijnnummer {vestigingDto.LijnNummer}) naar database: {ex.Message}");
        }
    }

    public IEnumerable<VestigingDto> GetAll() {
        List<VestigingDto> vestigingenDto = new();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            SELECT v.Id, v.Luchthaven, v.Straat, v.Postcode, v.Plaats, v.Land, v.LijnNummer
            FROM Vestigingen v";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var vestiging = MapVestigingFromReader(reader);
            if (vestiging != null) {
                vestigingenDto.Add(vestiging);
            }
        }

        return vestigingenDto;
    }
    private static VestigingDto? MapVestigingFromReader(SqlDataReader reader) {
        Guid id = Guid.Parse(reader.GetString(0));
        string luchthaven = reader.GetString(1);
        string straat = reader.GetString(2);
        string postcode = reader.GetString(3);
        string plaats = reader.GetString(4);
        string land = reader.GetString(5);
        int lijnNummer = reader.GetInt32(6);

        VestigingDto vestigingDto = new(
            id,
            luchthaven,
            straat,
            postcode,
            plaats,
            land,
            lijnNummer);
        return vestigingDto;
    }

    public void DeleteAll() {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            DELETE
            FROM Vestigingen";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();
    }
}

using AutoverhuurProject.Domein.DTOs;
using AutoverhuurProject.Domein.Exceptions;
using AutoverhuurProject.Domein.Interfaces;
using AutoverhuurProject.Domein.Models;
using Microsoft.Data.SqlClient;

namespace AutoverhuurProject.Persistentie.Db;

public class AutoRepositoryDb : IAutoRepositoryFull {
    private readonly string _connectionString;

    public AutoRepositoryDb(string connectionString) {
        _connectionString = connectionString;
    }

    public void Add(AutoDto autoDto) {
        try {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Get a random VestigingenId
            const string vestigingQuery = "SELECT Id FROM Vestigingen";
            using var vestigingCommand = new SqlCommand(vestigingQuery, connection);
            using var reader = vestigingCommand.ExecuteReader();
            List<string> vestigingenIds = new();
            while (reader.Read()) {
                vestigingenIds.Add(reader.GetString(0));
            }
            reader.Close();

            if (vestigingenIds.Count == 0) {
                throw new InvalidOperationException("Geen vestigingen gevonden in de database.");
            }

            var random = new Random();
            var randomVestigingenId = vestigingenIds[random.Next(vestigingenIds.Count)];

            const string autoQuery = @"
            INSERT INTO Autos (Id, Nummerplaat, Model, Zitplaatsen, Motortype, VestigingenId, LijnNummer)
            VALUES (@Id, @Nummerplaat, @Model, @Zitplaatsen, @Motortype, @VestigingenId, @LijnNummer)";

            using var autoCommand = new SqlCommand(autoQuery, connection);
            autoCommand.Parameters.AddWithValue("@Id", autoDto.Id);
            autoCommand.Parameters.AddWithValue("@Nummerplaat", autoDto.Nummerplaat);
            autoCommand.Parameters.AddWithValue("@Model", autoDto.Model);
            autoCommand.Parameters.AddWithValue("@Zitplaatsen", autoDto.Zitplaatsen);
            autoCommand.Parameters.AddWithValue("@Motortype", autoDto.Motortype);
            autoCommand.Parameters.AddWithValue("@VestigingenId", randomVestigingenId);
            autoCommand.Parameters.AddWithValue("@LijnNummer", autoDto.LijnNummer);
            autoCommand.ExecuteNonQuery();
        } catch (Exception ex) {
            ExceptionLogger.LogException(ex.Message, ex.Source, autoDto.LijnNummer);
        }
    }

    public IEnumerable<AutoDto> GetAll() {
        List<AutoDto> autosDto = new();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            SELECT a.Id, a.Nummerplaat, a.Model, a.Zitplaatsen, a.Motortype, a.LijnNummer
            FROM Autos a";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var auto = MapAutoFromReader(reader);
            if (auto != null) {
                autosDto.Add(auto);
            }
        }

        return autosDto;
    }
    private static AutoDto? MapAutoFromReader(SqlDataReader reader) {
        Guid id = Guid.Parse(reader.GetString(0));
        string nummerplaat = reader.GetString(1);
        string model = reader.GetString(2);
        int zitplaatsen = reader.GetInt32(3);
        EMotortype motortype = (EMotortype) Enum.Parse(typeof(EMotortype), reader.GetString(4));
        int lijnNummer = reader.GetInt32(5);

        AutoDto autoDto = new(
            id,
            nummerplaat,
            model,
            zitplaatsen,
            motortype,
            lijnNummer);
        return autoDto;
    }

    public void DeleteAll() {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
            DELETE
            FROM Autos";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();
    }

    //public IEnumerable<AutoDto> GetAutosByVestigingId(string vestigingId) {
    //    List<AutoDto> autosDto = new();
    //    using var connection = new SqlConnection(_connectionString);
    //    connection.Open();
    //    const string query = @"
    //        SELECT a.Id, a.Nummerplaat, a.Model, a.Zitplaatsen, a.Motortype, a.LijnNummer
    //        FROM Autos a
    //        WHERE a.VestigingenId = @VestigingenId";
    //    using var command = new SqlCommand(query, connection);
    //    command.Parameters.AddWithValue("@VestigingenId", vestigingId);
    //    using var reader = command.ExecuteReader();
    //    while (reader.Read()) {
    //        var auto = MapAutoFromReader(reader);
    //        if (auto != null) {
    //            autosDto.Add(auto);
    //        }
    //    }
    //    return autosDto;
    //}

    public IEnumerable<AutoDto> ZoekBeschikbareAutos(string vestigingId, DateTime startDatum, DateTime eindDatum) {
        List<AutoDto> autosDto = new();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = @"
        SELECT a.Id, a.Nummerplaat, a.Model, a.Zitplaatsen, a.Motortype, a.LijnNummer
        FROM Autos a
        WHERE a.VestigingenId = @VestigingenId
        AND a.Id NOT IN (
            SELECT r.AutoId
            FROM Reservaties r
            WHERE @StartDatum < r.EindDatum AND @EindDatum > r.StartDatum
        )";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@VestigingenId", vestigingId);
        command.Parameters.AddWithValue("@StartDatum", startDatum);
        command.Parameters.AddWithValue("@EindDatum", eindDatum);

        using var reader = command.ExecuteReader();
        while (reader.Read()) {
            var auto = MapAutoFromReader(reader);
            if (auto != null) {
                autosDto.Add(auto);
            }
        }

        return autosDto;
    }

}

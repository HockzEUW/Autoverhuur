namespace AutoverhuurProject.Domein.Exceptions {
    public static class ExceptionLogger {
        private const string _logFilePath = "../../../../errors.csv";
        public static void LogException(string error, string bestand, int lijnNummer) {
            if (!File.Exists(_logFilePath)) {
                File.WriteAllText(_logFilePath, "Bestand;Lijnnummer;Foutboodschap\n");
            }

            // Create the log entry
            var logEntry = $"\"{bestand}\";\"{lijnNummer}\";\"{error}\"\n";

            // Append the log entry to the CSV file
            File.AppendAllText(_logFilePath, logEntry);
        }
    }
}
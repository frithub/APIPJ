namespace APIPJF;

using StackExchange.Redis;
using System;

public class LogService
{
    private readonly IConnectionMultiplexer _redis;

    public LogService(string redisConnectionString)
    {
        _redis = ConnectionMultiplexer.Connect(redisConnectionString);
    }

    public void LogAction(string action, int personnelNumber, string details)
    {
        var db = _redis.GetDatabase();

        // Zeitstempel für die Log-Einträge
        string timestamp = DateTime.UtcNow.ToString("o"); // ISO 8601 Format

        // Log-Eintrag
        string logMessage = $"{timestamp} | Action: {action} | PersonnelNumber: {personnelNumber} | Details: {details}";

        // Logs in einer Redis-Liste speichern
        db.ListRightPush("UserLogs", logMessage);

        Console.WriteLine($"Log added: {logMessage}");
    }

    public void PrintLogs()
    {
        var db = _redis.GetDatabase();

        // Alle Logs auslesen
        var logs = db.ListRange("UserLogs");

        Console.WriteLine("Logs:");
        foreach (var log in logs)
        {
            Console.WriteLine(log.ToString());
        }
    }
}

namespace APIPJF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class User
{
    public User(int personnelNumber, string firstName, string lastName, string department, string email, string tel, int rights)
    {
        PersonnelNumber = personnelNumber;
        FirstName = firstName;
        LastName = lastName;
        Department = department;
        Email = email;
        Tel = tel;
        Rights = rights;
        CreatedAt = DateTime.UtcNow;
    }
    public User(){}
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Automatische Generierung (IDENTITY)
    public int PersonnelNumber { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(100)]
    public string Department { get; set; }
    
    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; }
    
    [MaxLength(50)]
    public string Tel { get; set; }
    
    [Required]
    public int Rights { get; set; }
    
    [Required]
    public bool IsActive { get; set; } = true; // Benutzerstatus (aktiv oder deaktiviert)
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool Login(string email, string password)
    {
        // Login ohne Passwortabfrage
        if (!IsActive)
        {
            Console.WriteLine("Benutzer ist deaktiviert.");
            return false;
        }

        if (Email == email)
        {
            Console.WriteLine($"Benutzer {FirstName} {LastName} erfolgreich angemeldet.");
            return true;
        }

        Console.WriteLine("Anmeldedaten ungültig.");
        return false;
    }

    // Benutzer deaktivieren
    public void DisableUser()
    {
        IsActive = false;
        Console.WriteLine($"Benutzer {FirstName} {LastName} wurde deaktiviert.");
    }

    // Benutzer aktivieren
    public void EnableUser()
    {
        IsActive = true;
        Console.WriteLine($"Benutzer {FirstName} {LastName} wurde aktiviert.");
    }

    // Benutzerinformationen anzeigen
    public override string ToString()
    {
        return $"[{PersonnelNumber}] {FirstName} {LastName}, Abteilung: {Department}, Email: {Email}, Tel: {Tel}, Rechte: {Rights}, Aktiv: {IsActive}";
    }
  
}
namespace APIPJF;

public class UserManager
{
    private readonly LogService _logService;

    public UserManager(LogService logService)
    {
        _logService = logService;
    }
    
    // Benutzer hinzufügen
    public void AddUser(User user)
    {
        
        using (UserDbContext context = new UserDbContext())
        {
            context.Users.Add(user);
            context.SaveChanges();
            
            // Logging
            _logService.LogAction("UserCreated", user.PersonnelNumber, $"User {user.FirstName} {user.LastName} added.");
        }
        // into DB
        Console.WriteLine($"Benutzer {user.FirstName} {user.LastName} wurde hinzugefügt.");
    }
    
    // Benutzer deaktivieren
    public void DisableUser(int personnelNumber)
    {
        using (UserDbContext context = new UserDbContext())
        {
            var user = context.Users.FirstOrDefault(u => u.PersonnelNumber == personnelNumber);
            
            if (user == null)
            {
                Console.WriteLine("Benutzer nicht gefunden.");
            }
            else
            {
                user.DisableUser();
                // Logging
                _logService.LogAction("UserDisabled", personnelNumber, $"User {user.FirstName} {user.LastName} disabled.");
            }
            
        }
        
    }

    // Alle Benutzer auflisten
    public List<User> ListUsers()
    {
        using (UserDbContext context = new UserDbContext())
        {
            Console.WriteLine("Benutzerliste:");
            foreach (var user in context.Users)
            {
                Console.WriteLine(user);
            }

            return context.Users.ToList();
        }
        

        
    }
    // 
    public void Login(string email, string password)
    {
        using (UserDbContext context = new UserDbContext())
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("Login failed: No user with the provided email address.");
                return;
            }

            if (!user.IsActive)
            {
                Console.WriteLine("Login failed: User is disabled.");
                return;
            }

            user.Login(email, password);
            return;
        }
    }
    
    public bool EditUser(int personnelNumber, string? firstName = null, string? lastName = null,
        string? department = null, string? email = null, string? tel = null, int? rights = null)
    {
        using (UserDbContext context = new UserDbContext())
        {
            // Benutzer anhand der PersonnelNumber aus der Datenbank abrufen
            var user = context.Users.FirstOrDefault(u => u.PersonnelNumber == personnelNumber);

            if (user == null)
            {
                Console.WriteLine($"No user found with Personnel Number: {personnelNumber}");
                return false;
            }
            
            string details = "No changes made";
            // Attribute aktualisieren, wenn neue Werte angegeben wurden
            if (!string.IsNullOrWhiteSpace(firstName)) user.FirstName = firstName;
            if (!string.IsNullOrWhiteSpace(lastName)) user.LastName = lastName;
            if (!string.IsNullOrWhiteSpace(department)) user.Department = department;
            if (!string.IsNullOrWhiteSpace(email)) user.Email = email;
            if (!string.IsNullOrWhiteSpace(tel)) user.Tel = tel;
            if (rights.HasValue) user.Rights = rights.Value;

            // Änderungen in der Datenbank speichern
            context.SaveChanges();

            Console.WriteLine($"User with Personnel Number {personnelNumber} has been updated.");
            // Logging
            _logService.LogAction("UserEdited", personnelNumber, details.Trim());
            
            return true;
        }
    }
}

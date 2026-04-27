namespace FoodDelivery.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Email { get; private set; }
    public string Password { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Phone { get; private set; }

    public bool IsLoggedIn { get; private set; }

    public User(string email, string password, string firstName, string lastName, DateTime birthDate, string phone)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email");

        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            throw new ArgumentException("Invalid password");

        if (birthDate < new DateTime(1900, 1, 1) || birthDate > DateTime.Today)
            throw new ArgumentException("Invalid birth date");

        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Phone = phone;
    }

    public bool Login(string email, string password)
    {
        if (Email == email && Password == password)
        {
            IsLoggedIn = true;
            return true;
        }
        return false;
    }

    public void Logout() => IsLoggedIn = false;
}
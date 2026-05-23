namespace Ticketing.Domain.Entities;

public class User
{
    private readonly List<UserRole> _userRoles = [];
    private readonly List<Ticket> _tickets = [];
    private readonly List<Response> _responses = [];

    private User()
    {
    }

    private User(Guid userId, string username, string passwordHash, string? email)
    {
        UserId = userId;
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string? Email { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();

    public IReadOnlyCollection<Response> Responses => _responses.AsReadOnly();

    public static User Register(string username, string passwordHash, string? email)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username is required.", nameof(username));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException("Password hash is required.", nameof(passwordHash));
        }

        return new User(Guid.NewGuid(), username.Trim(), passwordHash, string.IsNullOrWhiteSpace(email) ? null : email.Trim());
    }

    public void AssignRole(Role role)
    {
        if (_userRoles.Any(userRole => userRole.RoleId == role.RoleId))
        {
            return;
        }

        _userRoles.Add(UserRole.Create(UserId, role.RoleId));
    }
}

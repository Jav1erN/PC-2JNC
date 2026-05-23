namespace Ticketing.Domain.Entities;

public class Role
{
    private readonly List<UserRole> _userRoles = [];

    private Role()
    {
    }

    private Role(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }

    public Guid RoleId { get; private set; }

    public string RoleName { get; private set; } = string.Empty;

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public static Role Create(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            throw new ArgumentException("Role name is required.", nameof(roleName));
        }

        return new Role(Guid.NewGuid(), roleName.Trim());
    }
}

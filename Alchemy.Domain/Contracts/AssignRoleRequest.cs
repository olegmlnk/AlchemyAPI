namespace Alchemy.Domain.Contracts
{
    public class AssignRoleRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}

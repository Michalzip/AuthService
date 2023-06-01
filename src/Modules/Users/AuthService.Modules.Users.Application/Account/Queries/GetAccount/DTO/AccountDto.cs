namespace AuthService.Modules.Application.Account.Queries.GetAccount.DTO
{
    public class AccountDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
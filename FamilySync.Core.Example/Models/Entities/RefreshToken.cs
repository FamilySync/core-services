namespace FamilySync.Core.Example.Models.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid IdentityId { get; set; }
        public string Token { get; set; }
    }
}
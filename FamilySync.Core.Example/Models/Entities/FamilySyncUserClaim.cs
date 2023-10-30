
namespace Ajour.Models.Authentication.Entities
{
    public class FamilySyncUserClaim
    {
        public Guid Id { get; set; }
        public string AccessLevel { get; set; }
        public string Claim { get; set; }
        
        public FamilySyncUser User { get; set; }
    }
}
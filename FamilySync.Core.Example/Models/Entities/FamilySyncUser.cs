using System;
using System.Collections.Generic;

namespace Ajour.Models.Authentication.Entities
{
    public class FamilySyncUser
    {
        public Guid Id { get; set; }
        
        public List<FamilySyncUserClaim> Claims { get; set; }
    }
}
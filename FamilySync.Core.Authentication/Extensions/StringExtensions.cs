namespace FamilySync.Core.Authentication.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> GetIdentityHierarchy(this string identity)
    {
        // Create a list to store the claim hierarchy, initialized with the original claim.
        List<string> claims = new() { identity };

        // Check if the claim contains a ':'
        if (identity.Contains(':'))
        {
            // Split the claim by ':'
            string[] split = identity.Split(':');

            // Iterate through the parts of the claim hierarchy
            for (int i = split.Length; i >= 0; i--)
            {
                // Create a new claim by joining the parts up to the current index with ':'
                claims.Add(string.Join(':', split[..i]));
            }
        }

        // Filter out empty claims and return a distinct set of claims.
        return claims
            .Where(o => !string.IsNullOrEmpty(o))
            .Distinct();
    }
}
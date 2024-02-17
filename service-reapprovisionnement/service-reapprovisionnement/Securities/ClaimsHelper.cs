using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Claims;

namespace Securities {
	

public class ClaimsHelper
{
		private Dictionary<string, string> issuerByName = new Dictionary<string, string>();

		public ClaimsHelper() {
			this.issuerByName["name"] = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
			this.issuerByName["role"] = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
		}

		public string GetName(ClaimsIdentity identity) {
			return this.GetClaimValue(identity, this.issuerByName["name"])[0];
		}

		public List<string> GetRoles(ClaimsIdentity identity) {
			return this.GetClaimValue(identity, this.issuerByName["role"]);
		}

		public bool IsGestionaire(ClaimsIdentity identity) {
			List<string> roles = this.GetRoles(identity);
			return roles.Contains("GESTIONNAIRE");
		}

    // Méthode pour récupérer la valeur d'une revendication spécifique à partir de l'identité principale
    private List<string> GetClaimValue(ClaimsIdentity identity, string claimType)
    {
        // Vérifier si l'identité est valide et non nulle
        if (identity == null)
        {
            throw new ArgumentNullException(nameof(identity));
        }

				List<string> claimValues = new List<string>();

        // Parcourir les revendications dans l'identité
        foreach (Claim claim in identity.Claims)
        {
            // Vérifier si le type de la revendication correspond au type spécifié
            if (claim.Type == claimType)
            {
                // Retourner la valeur de la revendication si elle est trouvée
                claimValues.Add(claim.Value);
            }
        }

        // Retourner null si la revendication spécifiée n'est pas trouvée dans l'identité
        return claimValues;
    }
}

}
using service_reapprovisionnement.Enum;

namespace Reapprovisionnement.Models;

public class BonDeCommande
{
    public Guid Id { get; set; } // UUID (Automatique)
    public int idBonDeCommande { get; set; }
    public string DateCreation { get; set; }
    public int Quantite { get; set; }
    public EtatCommande EtatCommande { get; set; }
    public Produit Produit { get; set; }
}
namespace Reapprovisionnement.Models;

public class Fournisseur
{
    public Guid Id { get; set; }
    public int idFournissuer { get; set; }
    public String nom { get; set; }
    public String email { get; set; }
    public String raison_sociel { get; set; }
    public List<Produit> Produits { get; set; }
    
}
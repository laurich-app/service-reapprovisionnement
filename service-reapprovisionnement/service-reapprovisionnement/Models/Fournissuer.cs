namespace Reapprovisionnement.Models;

public class Fournissuer
{
    //public Guid Id { get; set; }
    public int idFournissuer { get; set; }
    public String email { get; set; }
    public String raison_sociel { get; set; }
    public List<Produit> Produits { get; set; }
    
}
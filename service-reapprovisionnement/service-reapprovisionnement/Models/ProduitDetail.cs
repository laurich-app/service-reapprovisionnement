namespace Reapprovisionnement.Models;

public class ProduitDetail
{
    public double PrixUnitaireFournisseur { get; set; }
    public int IdProduitCatalogue { get; set; }
    public string Libelle { get; set; }
    public string Description { get; set; }
    public List<string> Couleurs { get; set; }
    public string Sexe { get; set; }
    public string Taille { get; set; }
    public string ImageUrl { get; set; }

    public ProduitDetail()
    {
        // Initialise la liste dans le constructeur
        Couleurs = new List<string>();
    }
}
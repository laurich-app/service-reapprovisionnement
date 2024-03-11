namespace Reapprovisionnement.Models;

public class ProduitDetail
{
    public double prix_unitaire_fournisseur { get; set; }
    public int id_produit_catalogue { get; set; }
    public string libelle { get; set; }
    public string description { get; set; }
    public List<string> couleurs { get; set; }
    public string sexe { get; set; }
    public string taille { get; set; }
    public string image_url { get; set; }

    public ProduitDetail()
    {
        // Initialise la liste dans le constructeur
        couleurs = new List<string>();
    }
}
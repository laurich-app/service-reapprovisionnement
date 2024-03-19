namespace Dtos.Rabbits;

public class ProduitCatalogueManquantDTO {
	public int id_produit { get; set; }
    public double prix { get; set; }
    public string sexe { get; set; }
    public string taille { get; set; }
    public string image_url { get; set; }
    public string couleur { get; set; }
    public string libelle { get; set; }
    public string description { get; set; }
}
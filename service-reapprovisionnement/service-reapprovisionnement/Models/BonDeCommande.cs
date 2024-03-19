using service_reapprovisionnement.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Reapprovisionnement.Models;

public class BonDeCommande
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime date_creation { get; set; }
    public int quantite { get; set; }
    [BsonRepresentation(BsonType.String)]

    public EtatCommande etat_commande { get; set; }
    public ProduitDetail produit { get; set; }
}
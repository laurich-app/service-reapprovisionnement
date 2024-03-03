using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reapprovisionnement.Models;

public class Fournisseur
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int idFournisseur { get; set; }
    public String nom { get; set; }
    public String email { get; set; }
    public String raison_sociel { get; set; }
    public List<Produit> Produits { get; set; }
    
}
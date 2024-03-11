using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reapprovisionnement.Models;

public class Fournisseur
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string email { get; set; }
    public string raison_sociale { get; set; }
    public List<Produit>? produits { get; set; }
    
}
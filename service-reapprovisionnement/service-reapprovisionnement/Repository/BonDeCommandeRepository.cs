using MongoDB.Bson;
using MongoDB.Driver;
using Reapprovisionnement.Models;
using services;
using service_reapprovisionnement.Enum;

namespace service_reapprovisionnement.Repository;

public class BonDeCommandeRepository : IBonDeCommandeRepository
{
    private readonly IMongoCollection<BonDeCommande> bonDeCommande;
    
    public BonDeCommandeRepository(DatabaseService databaseService)
    {
        this.bonDeCommande = databaseService.mongoDatabase.GetCollection<BonDeCommande>("bonDeCommande");
    }
    
    public List<BonDeCommande> GetAll(int page, int pageSize)
    {
        var skipAmount = (page - 1) * pageSize;
        return this.bonDeCommande
            .Find(new BsonDocument()) // Filtre vide pour récupérer tous les articles
            .Skip(skipAmount)
            .Limit(pageSize)
            .ToList();
    }

    public BonDeCommande GetById(string id)
    {
        return this.bonDeCommande.Find(cmd => cmd.Id == id).SingleOrDefault();
    }

    public void Update(string id, BonDeCommande entity)
    {
        this.bonDeCommande.ReplaceOne(cmd => cmd.Id == id, entity);
    }

    public void Delete(string id)
    {
        this.bonDeCommande.DeleteOne(cmd => cmd.Id == id);
    }

    public BonDeCommande Create(BonDeCommande entity)
    {
        this.bonDeCommande.InsertOne(entity);
        return entity;
    }
    
    public int GetTotalBonDeCommande()
    {
        // Compter tous les documents dans la collection
        return Convert.ToInt32(this.bonDeCommande.CountDocuments(new BsonDocument()));
    }

    public async Task SupprimerBonsDeCommandeEnCoursAvecProduit(int idProduit, string couleur)
    {
        var filter = Builders<BonDeCommande>.Filter.And(
            Builders<BonDeCommande>.Filter.Eq(b => b.etat_commande, EtatCommande.EN_COURS),
            Builders<BonDeCommande>.Filter.Eq(b => b.produit.id_produit_catalogue, idProduit),
            Builders<BonDeCommande>.Filter.Eq(b => b.produit.couleur, couleur)
        );

        await bonDeCommande.DeleteManyAsync(filter);
    }
}
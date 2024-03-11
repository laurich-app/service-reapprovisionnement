using MongoDB.Bson;
using MongoDB.Driver;
using Reapprovisionnement.Models;
using services;

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

    public BonDeCommande GetById(int id)
    {
        return this.bonDeCommande.Find(cmd => cmd.idBonDeCommande == id).SingleOrDefault();
    }

    public void Update(int id, BonDeCommande entity)
    {
        this.bonDeCommande.ReplaceOne(cmd => cmd.idBonDeCommande == id, entity);
    }

    public void Delete(int id)
    {
        this.bonDeCommande.DeleteOne(cmd => cmd.idBonDeCommande == id);
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
}
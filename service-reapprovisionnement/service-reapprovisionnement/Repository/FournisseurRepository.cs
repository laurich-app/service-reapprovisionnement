using MongoDB.Bson;
using Reapprovisionnement.Models;
using MongoDB.Driver;
using services;

namespace service_reapprovisionnement.Repository;

public class FournisseurRepository : IFournisseurRepository
{
    private readonly IMongoCollection<Fournisseur> fournisseur;
    
    public FournisseurRepository(DatabaseService databaseService)
    {
        this.fournisseur = databaseService.mongoDatabase.GetCollection<Fournisseur>("fournisseur");
    }
    
    public Fournisseur Create(Fournisseur entity)
    {
        this.fournisseur.InsertOne(entity);
        return entity;
    }
    
    public void Delete(string id)
    {
        this.fournisseur.DeleteOne(cmd => cmd.Id == id);
    }
    
    public List<Fournisseur> GetAll(int page, int pageSize)
    {
        var skipAmount = (page - 1) * pageSize;
        return this.fournisseur
            .Find(new BsonDocument()) // Filtre vide pour récupérer tous les articles
            .Skip(skipAmount)
            .Limit(pageSize)
            .ToList();
    }

    public Fournisseur GetById(string id)
    {
        return this.fournisseur.Find(cmd => cmd.Id == id).SingleOrDefault();
    }
    
    public void Update(string id, Fournisseur entity)
    {
        this.fournisseur.ReplaceOne(cmd => cmd.Id == id, entity);
    }

    public int GetTotalFournisseurs()
    {
        // Compter tous les documents dans la collection
        return Convert.ToInt32(this.fournisseur.CountDocuments(new BsonDocument()));
    }
}
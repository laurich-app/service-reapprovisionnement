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
    
    public void Delete(int id)
    {
        this.fournisseur.DeleteOne(cmd => cmd.idFournisseur == id);
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

    public Fournisseur GetById(int id)
    {
        return this.fournisseur.Find(cmd => cmd.idFournisseur == id).SingleOrDefault();
    }
    
    public void Update(int id, Fournisseur entity)
    {
        this.fournisseur.ReplaceOne(cmd => cmd.idFournisseur == id, entity);
    }

    public long GetTotalFournisseurs()
    {
        // Compter tous les documents dans la collection
        return this.fournisseur.CountDocuments(new BsonDocument());
    }

    public long GetFilteredFournisseurs(FilterDefinition<Fournisseur> filter)
    {
        // Compter les documents qui correspondent à un filtre spécifique
        return this.fournisseur.CountDocuments(filter);
    }
}
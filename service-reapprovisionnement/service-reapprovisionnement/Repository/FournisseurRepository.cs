using Reapprovisionnement.Models;
using MongoDB.Driver;

namespace service_reapprovisionnement.Repository;

public class FournisseurRepository : IFournisseurRepository
{
    private readonly IMongoCollection<Fournisseur> fournisseur;
    
    public FournisseurRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase("fournisseur");
        this.fournisseur = database.GetCollection<Fournisseur>("fournissuer");
    }
    
    public Fournisseur Create(Fournisseur entity)
    {
        this.fournisseur.InsertOne(entity);
        return entity;
    }
    
    public void Delete(int id)
    {
        this.fournisseur.DeleteOne(cmd => cmd.idFounisseur == id);
    }
    
    public List<Fournisseur> GetAll()
    {
        return this.fournisseur.Find(cmd => true).ToList();
    }
    
    public Fournisseur GetById(int id)
    {
        return this.fournisseur.Find(cmd => cmd.idCommande == id).SingleOrDefault();
    }
    
    public void Update(int id, Fournisseur entity)
    {
        this.fournisseur.ReplaceOne(cmd => cmd.idCommande == id, entity);
    }
}
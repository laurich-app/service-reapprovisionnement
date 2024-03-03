using Reapprovisionnement.Models;

namespace services;

public interface IFounisseurService
{
    List<Fournisseur> GetAll();

    Fournisseur GetById(int id);

    Fournisseur Create(Fournisseur entity);

    void Delete(int id);

    void Update(int id, Fournisseur entity);
    
    
}
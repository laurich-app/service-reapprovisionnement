using Reapprovisionnement.Models;
using service_reapprovisionnement.Repository;

namespace services;

public class FournisseurService : IFounisseurService
{
    private readonly IFournisseurRepository fournisseurRepository;


    public List<Fournisseur> GetAll()
    {
        throw new NotImplementedException();
    }

    public Fournisseur GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Fournisseur Create(Fournisseur entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(int id, Fournisseur entity)
    {
        throw new NotImplementedException();
    }
}
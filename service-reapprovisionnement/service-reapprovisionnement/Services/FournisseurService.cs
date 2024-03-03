using Dtos;
using Reapprovisionnement.Models;
using service_reapprovisionnement.Repository;

namespace services;

public class FournisseurService : IFounisseurService
{
    private readonly IFournisseurRepository fournisseurRepository;


    public Pagination<Fournisseur> GetAll(int page, int pageSize)
    {
        Paginated pItem = new Paginated(
             this.fournisseurRepository.GetTotalFournisseurs(),
             pageSize,
             page
         );
        Pagination<Fournisseur> p = new Pagination<Fournisseur>(
            this.fournisseurRepository.GetAll(page, pageSize),
            pItem
        );
        return p;
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
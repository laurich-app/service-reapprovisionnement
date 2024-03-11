using Dtos;
using Reapprovisionnement.Models;
using service_reapprovisionnement.Repository;
using services.Exception;

namespace services;

public class FournisseurService : IFounisseurService
{
    private readonly IFournisseurRepository fournisseurRepository;

    public FournisseurService(FournisseurRepository fournisseurRepository)
    {
        this.fournisseurRepository = fournisseurRepository;
    }

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
        return this.fournisseurRepository.GetById(id) ??
               throw new FournisseurNotFoundException("le fournisseur " + id + " n'existe pas dans la base de données");
    }

    public Fournisseur Create(Fournisseur entity)
    {
        return this.fournisseurRepository.Create(entity);
    }

    public void Delete(int id)
    {
        this.fournisseurRepository.Delete(id);
   }

    public void Update(int id, Fournisseur entity)
    {
        this.fournisseurRepository.Update(id, entity);
    }
}
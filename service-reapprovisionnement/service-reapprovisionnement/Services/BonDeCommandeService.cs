using Dtos;
using Reapprovisionnement.Models;
using service_reapprovisionnement.Enum;
using service_reapprovisionnement.Repository;
using services.Exception;

namespace services;

public class BonDeCommandeService : IBonDeCommandeService
{
    private readonly IBonDeCommandeRepository bonDeCommandeRepository;
    
    public BonDeCommandeService(BonDeCommandeRepository bonDeCommandeRepository)
    {
        this.bonDeCommandeRepository = bonDeCommandeRepository;
    }
    
    public BonDeCommandeService() { }
    
    public Pagination<BonDeCommande> GetAll(int page, int pageSize)
    {
        Paginated pItem = new Paginated(
            this.bonDeCommandeRepository.GetTotalBonDeCommande(),
            pageSize,
            page
        );
        Pagination<BonDeCommande> p = new Pagination<BonDeCommande>(
            this.bonDeCommandeRepository.GetAll(page, pageSize),
            pItem
        );
        return p;
    }

    public BonDeCommande GetById(string id)
    {
        return this.bonDeCommandeRepository.GetById(id) ??
               throw new BonDeCommandeNotFoundException("le bon de commande " + id + " n'existe pas dans la base de données");
    }

    public BonDeCommande Create(BonDeCommande entity)
    {
        return this.bonDeCommandeRepository.Create(entity);
    }

    public void Delete(string id)
    {
        this.bonDeCommandeRepository.Delete(id);
    }

    public void Update(string id, BonDeCommande entity)
    {
        this.bonDeCommandeRepository.Update(id, entity);
    }

    public BonDeCommande UpdateLivraison(string id, EtatCommande etat)
    {
        BonDeCommande bonDeCommande = this.bonDeCommandeRepository.GetById(id);
        if (bonDeCommande == null)
            throw new BonDeCommandeNotFoundException();

        bonDeCommande.etat_commande = etat;
        this.bonDeCommandeRepository.Update(id, bonDeCommande);
        return bonDeCommande;
    }
}
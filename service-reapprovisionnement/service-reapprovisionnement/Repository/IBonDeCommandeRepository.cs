using Reapprovisionnement.Models;

namespace service_reapprovisionnement.Repository;

public interface IBonDeCommandeRepository
{
    List<BonDeCommande> GetAll(int page, int pageSize);
    
    BonDeCommande GetById(int id);
    
    void Update(int id, BonDeCommande entity);
    
    void Delete(int id);

    BonDeCommande Create(BonDeCommande entity);
    
    int GetTotalBonDeCommande();
}
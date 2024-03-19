using Reapprovisionnement.Models;

namespace service_reapprovisionnement.Repository;

public interface IBonDeCommandeRepository
{
    List<BonDeCommande> GetAll(int page, int pageSize);
    
    BonDeCommande GetById(string id);
    
    void Update(string id, BonDeCommande entity);
    
    void Delete(string id);

    BonDeCommande Create(BonDeCommande entity);
    
    int GetTotalBonDeCommande();

    Task SupprimerBonsDeCommandeEnCoursAvecProduit(int idProduit, string couleur);

}
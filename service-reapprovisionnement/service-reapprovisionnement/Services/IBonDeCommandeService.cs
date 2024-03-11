using Dtos;
using Reapprovisionnement.Models;
using service_reapprovisionnement.Enum;

namespace services;

public interface IBonDeCommandeService
{
    Pagination<BonDeCommande> GetAll(int page, int pageSize);

    BonDeCommande GetById(string id);

    BonDeCommande Create(BonDeCommande entity);

    void Delete(string id);

    void Update(string id, BonDeCommande entity);

    BonDeCommande UpdateLivraison(string id, EtatCommande etatCommande);
}
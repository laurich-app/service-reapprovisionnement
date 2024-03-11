using Dtos;
using Reapprovisionnement.Models;

namespace services;

public interface IBonDeCommandeService
{
    Pagination<BonDeCommande> GetAll(int page, int pageSize);

    BonDeCommande GetById(int id);

    BonDeCommande Create(BonDeCommande entity);

    void Delete(int id);

    void Update(int id, BonDeCommande entity);
}
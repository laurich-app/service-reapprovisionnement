using Dtos;
using Reapprovisionnement.Models;

namespace services;

public interface IFounisseurService
{
    Pagination<Fournisseur> GetAll(int page, int pageSize);

    Fournisseur GetById(int id);

    Fournisseur Create(Fournisseur entity);

    void Delete(int id);

    void Update(int id, Fournisseur entity);
    
    
}
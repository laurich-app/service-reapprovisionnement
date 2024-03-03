using Reapprovisionnement.Models;

namespace service_reapprovisionnement.Repository;

public interface IFournisseurRepository
{
    List<Fournisseur> GetAll(int page, int pageSize);

    Fournisseur GetById(int id);

    Fournisseur Create(Fournisseur entity);

    void Delete(int id);

    void Update(int id, Fournisseur entity);

    int GetTotalFournisseurs();
}
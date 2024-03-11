using Reapprovisionnement.Models;

namespace service_reapprovisionnement.Repository;

public interface IFournisseurRepository
{
    List<Fournisseur> GetAll(int page, int pageSize);

    Fournisseur GetById(string id);

    Fournisseur Create(Fournisseur entity);

    void Delete(string id);

    void Update(string id, Fournisseur entity);

    int GetTotalFournisseurs();
}
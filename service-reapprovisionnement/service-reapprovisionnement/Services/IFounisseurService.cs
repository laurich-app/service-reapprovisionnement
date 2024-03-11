using Dtos;
using Reapprovisionnement.Models;

namespace services;

public interface IFounisseurService
{
    Pagination<Fournisseur> GetAll(int page, int pageSize);

    Fournisseur GetById(string id);

    Fournisseur Create(Fournisseur entity);

    void Delete(string id);

    Fournisseur Update(string id, FournisseurUpdate entity);

    Produit AddProduit(string id_fournisseur, Produit produit);

    Produit UpdProduit(string id_fournisseur, int id_catalogue_produit, ProduitUpd produitUpd);

    void DeleteProduit(string id_fournisseur, int id_catalogue_produit);
}
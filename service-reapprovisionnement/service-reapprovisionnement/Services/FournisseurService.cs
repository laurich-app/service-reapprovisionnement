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

    public Fournisseur GetById(string id)
    {
        return this.fournisseurRepository.GetById(id) ??
               throw new FournisseurNotFoundException("le fournisseur " + id + " n'existe pas dans la base de données");
    }

    public Fournisseur Create(Fournisseur entity)
    {
        return this.fournisseurRepository.Create(entity);
    }

    public void Delete(string id)
    {
        this.fournisseurRepository.Delete(id);
   }

    public Fournisseur Update(string id, FournisseurUpdate entity)
    {
        Fournisseur f = this.fournisseurRepository.GetById(id);
        this.fournisseurRepository.Update(id, FournisseurUpdate.convertToDTO(f, entity));
        return f;
    }

    public Produit AddProduit(string id_fournisseur, Produit produit)
    {
        Fournisseur f = this.fournisseurRepository.GetById(id_fournisseur);
        if (f == null)
            throw new FournisseurNotFoundException();
        Produit exist = f.produits.Find(i => i.id_produit_catalogue == produit.id_produit_catalogue);
        if (exist != null)
            throw new ProduitAlreadyExistException();
        List<Produit> produits = f.produits;
        produits.Add(produit);
        f.produits = produits;
        this.fournisseurRepository.Update(id_fournisseur, f);
        return produit;
    }

    public Produit UpdProduit(string id_fournisseur, int id_catalogue_produit, ProduitUpd produitUpd)
    {
        Fournisseur f = this.fournisseurRepository.GetById(id_fournisseur);
        if (f == null)
            throw new FournisseurNotFoundException();
        Produit p = f.produits.Find(t => t.id_produit_catalogue == id_catalogue_produit);
        if (p == null)
            throw new ProduitNotFoundException();
        p.prix_unitaire_fournisseur = produitUpd.prix_unitaire_fournisseur;
        this.fournisseurRepository.Update(id_fournisseur, f);
        return p;
    }

    public void DeleteProduit(string id_fournisseur, int id_catalogue_produit)
    {
        Fournisseur f = this.fournisseurRepository.GetById(id_fournisseur);
        if (f == null)
            throw new FournisseurNotFoundException();
        Produit p = f.produits.Find(p => p.id_produit_catalogue == id_catalogue_produit);
        if (p == null)
            throw new ProduitNotFoundException();

        f.produits.Remove(p);
        this.fournisseurRepository.Update(id_fournisseur, f);
    }
}
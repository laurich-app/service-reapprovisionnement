using Dtos;
using Dtos.Rabbits;
using Reapprovisionnement.Models;
using service_reapprovisionnement.Enum;
using service_reapprovisionnement.Repository;
using services.Exception;
using services;

namespace services;

public class BonDeCommandeService : IBonDeCommandeService
{
    private readonly IBonDeCommandeRepository bonDeCommandeRepository;
    private readonly RabbitMQProducerService rabbitMQProducerService;
    
    public BonDeCommandeService(BonDeCommandeRepository bonDeCommandeRepository, RabbitMQProducerService rabbitMQProducerService)
    {
        this.bonDeCommandeRepository = bonDeCommandeRepository;
        this.rabbitMQProducerService = rabbitMQProducerService;
    }
    
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
        if (bonDeCommande.etat_commande == EtatCommande.LIVRER)
            throw new DejaLivrerEtatInchangeableException();

        bonDeCommande.etat_commande = etat;
        this.bonDeCommandeRepository.Update(id, bonDeCommande);

        if(etat == EtatCommande.LIVRER) {
            var p = new ProduitReapprovisionnementDTO();
            p.id_produit = bonDeCommande.produit.id_produit_catalogue;
            p.couleur = bonDeCommande.produit.couleur;
            p.quantite = bonDeCommande.quantite;
            this.rabbitMQProducerService.SendStockReappro(p);
        }
        return bonDeCommande;
    }

    /**
    * Lors de la suppression d'un stock, on annule les commandes en cours correspondante.
    */
    public void SupprimerCommande(int id_produit, string couleur)
    {
        this.bonDeCommandeRepository.SupprimerBonsDeCommandeEnCoursAvecProduit(id_produit, couleur);
    }

    public void StockReapproDemande(ProduitCatalogueManquantDTO produit){
        BonDeCommande bonDeCommande = new BonDeCommande();

        bonDeCommande.date_creation = DateTime.Now;
        bonDeCommande.quantite = 5;
        bonDeCommande.etat_commande = EtatCommande.EN_COURS;
        bonDeCommande.produit = new ProduitDetail();
        bonDeCommande.produit.prix_unitaire_fournisseur = produit.prix;
        bonDeCommande.produit.id_produit_catalogue = produit.id_produit;
        bonDeCommande.produit.libelle = produit.libelle;
        bonDeCommande.produit.description = produit.description;
        bonDeCommande.produit.couleur = produit.couleur;
        bonDeCommande.produit.sexe = produit.sexe;
        bonDeCommande.produit.taille = produit.taille;
        bonDeCommande.produit.image_url = produit.image_url;

        this.bonDeCommandeRepository.Create(bonDeCommande);
    }

}
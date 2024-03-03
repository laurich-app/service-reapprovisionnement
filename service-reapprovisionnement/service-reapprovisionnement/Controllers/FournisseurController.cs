using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reapprovisionnement.Models;
using services;

namespace service_reapprovisionnement.Controllers;
[Route("api/fournisseurs")]
[ApiController]
//[Authorize(Roles = "ROLE_GESTIONNAIRE")]
//Console.WriteLine(this._claimsHelper.IsGestionaire((ClaimsIdentity)principal.Identity));
[AllowAnonymous]
public class FournisseurController: ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsHelper _claimsHelper;

    private readonly FournisseurService _fournisseurService;

    private readonly ILogger<FournisseurController> _logger;

    public FournisseurController(ILogger<FournisseurController> logger, IHttpContextAccessor httpContextAccessor, ClaimsHelper claimsHelper, FournisseurService fournisseurService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _claimsHelper = claimsHelper;
        _fournisseurService = fournisseurService;
    }

    // GET /api/fournisseurs
    [HttpGet]
    public IActionResult GetFournisseurs([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        // Logique pour récupérer la liste des fournisseurs paginés
        Pagination<Fournisseur> p = this._fournisseurService.GetAll(page, limit);

        // Retourne la réponse avec le format spécifié
        return Ok(p);
    }
    
    // GET /api/fournisseurs/{id}
    [HttpGet("{id}")]
    public IActionResult GetFournisseur(Guid id)
    {
        // Logique pour récupérer un fournisseur par son ID
        // ...

        // Retourne la réponse avec le format spécifié
        return Ok(new Fournisseur());
    }
    
    // POST /api/fournisseurs
    [HttpPost]
    public IActionResult AddFournisseur([FromBody] Fournisseur fournisseur)
    {
        // Logique pour ajouter un fournisseur
        // ...

        // Retourne la réponse avec le format spécifié
        return CreatedAtAction(nameof(GetFournisseur), new { id = fournisseur.idFournissuer }, fournisseur);
    }
    
    // PUT /api/fournisseurs/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateFournisseur(Guid id, [FromBody] Fournisseur fournisseur)
    {
        // Logique pour mettre à jour un fournisseur par son ID
        // ...

        // Retourne la réponse avec le format spécifié
        return Ok(fournisseur);
    }
    
    // DELETE /api/fournisseurs/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteFournisseur(Guid id)
    {
        // Logique pour supprimer un fournisseur par son ID
        // ...

        // Retourne la réponse avec le code 204 (No Content)
        return NoContent();
    }
    
    // POST /api/fournisseurs/{id}/produits
    [HttpPost("{id}/produits")]
    public IActionResult AddProduitToFournisseur(Guid id, [FromBody] Produit produit)
    {
        // Logique pour ajouter un produit à un fournisseur par son ID
        // ...

        // Retourne la réponse avec le format spécifié
        //return CreatedAtAction(nameof(GetProduit), new { fournisseurId = id, produitId = produit.IdProduitCatalogue }, produit);
        return null;
    }

    // ... Ajouter d'autres endpoints pour les opérations liées aux produits
}
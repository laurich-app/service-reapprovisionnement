using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reapprovisionnement.Models;
using services;
using System.Security.Claims;
using Securities;
using Dtos;
using services.Exception;

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
    public IActionResult GetFournisseur(int id)
    {
        // Logique pour récupérer un fournisseur par son ID
        try
        {
            Fournisseur fournisseur = this._fournisseurService.GetById(id);
            return Ok(fournisseur);
        }
        catch (FournisseurNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // POST /api/fournisseurs
    [HttpPost]
    public IActionResult AddFournisseur([FromBody] Fournisseur fournisseur)
    {
        // Logique pour ajouter un fournisseur
        try
        {
            Fournisseur f = this._fournisseurService.Create(fournisseur);
            return Ok(f);
        }
        catch (FournisseurNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // PUT /api/fournisseurs/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateFournisseur(int id, [FromBody] Fournisseur fournisseur)
    {
        // Logique pour mettre à jour un fournisseur par son ID
        try
        {
            Fournisseur f = this._fournisseurService.Update(id, fournisseur);
            return Ok(f);
        }
        catch (FournisseurNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
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
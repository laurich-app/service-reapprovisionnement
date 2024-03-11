using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reapprovisionnement.Models;
using services;
using System.Security.Claims;
using Securities;
using Dtos;
using services.Exception;

namespace service_reapprovisionnement.Controllers;
[Route("fournisseurs")]
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

    private Boolean Authentifier()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        return this._claimsHelper.IsGestionaire((ClaimsIdentity)principal.Identity);
    }

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
        if (!this.Authentifier())
            return Unauthorized();
        
        // Logique pour récupérer la liste des fournisseurs paginés
        Pagination<Fournisseur> p = this._fournisseurService.GetAll(page, limit);

        // Retourne la réponse avec le format spécifié
        return Ok(p);
    }
    
    // GET /fournisseurs/{id}
    [HttpGet("{id}")]
    public IActionResult GetFournisseur(string id)
    {
        if (!this.Authentifier())
            return Unauthorized();
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
    
    // POST /fournisseurs
    [HttpPost]
    public IActionResult AddFournisseur([FromBody] Fournisseur fournisseur)
    {
        if (!this.Authentifier())
            return Unauthorized();
        // Logique pour ajouter un fournisseur
        try
        {
            Fournisseur f = this._fournisseurService.Create(fournisseur);
            return CreatedAtAction(nameof(GetFournisseur), new { id = f.Id }, f);

        }
        catch (FournisseurNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // PUT /api/fournisseurs/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateFournisseur(string id, [FromBody] FournisseurUpdate fournisseur)
    {
        if (!this.Authentifier())
            return Unauthorized();
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
    public IActionResult DeleteFournisseur(string id)
    {
        if (!this.Authentifier())
            return Unauthorized();
        // Logique pour supprimer un fournisseur par son ID
        this._fournisseurService.Delete(id);

        // Retourne la réponse avec le code 204 (No Content)
        return NoContent();
    }
    
    // POST /fournisseurs/{id}/produits
    [HttpPost("{id}/produits")]
    public IActionResult AddProduitToFournisseur([FromRoute] string id, [FromBody] Produit produit)
    {
        if (!this.Authentifier())
            return Unauthorized();
        try
        {
            // Logique pour ajouter un produit à un fournisseur par son ID
            Produit p = this._fournisseurService.AddProduit(id, produit);

            // Retourne la réponse avec le format spécifié
            return CreatedAtAction(nameof(GetFournisseur), new { id }, p);
        }
        catch (FournisseurNotFoundException p)
        {
            return NotFound(p.Message);
        }
        catch (ProduitAlreadyExistException p)
        {
            return Conflict(p.Message);
        }
    }
    
    // PUT /fournisseurs/{id}/produits/{catalogueId}
    [HttpPut("{id}/produits/{catalogueId}")]
    public IActionResult UpdProduitToFournisseur([FromRoute] string id, int catalogueId, [FromBody] ProduitUpd produit)
    {
        if (!this.Authentifier())
            return Unauthorized();
        try
        {
            // Logique pour ajouter un produit à un fournisseur par son ID
            Produit p = this._fournisseurService.UpdProduit(id, catalogueId, produit);

            // Retourne la réponse avec le format spécifié
            return Ok(p);
        }
        catch (ProduitNotFoundException p)
        {
            return NotFound(p.Message);
        }
        catch (FournisseurNotFoundException p)
        {
            return NotFound(p.Message);
        }
    }
    
    // DELETE /fournisseurs/{id}/produits/{catalogueId}
    [HttpDelete("{id}/produits/{catalogueId}")]
    public IActionResult DeleteProduitToFournisseur([FromRoute] string id, int catalogueId)
    {
        if (!this.Authentifier())
            return Unauthorized();
        // Logique pour ajouter un produit à un fournisseur par son ID
        try
        {
            this._fournisseurService.DeleteProduit(id, catalogueId);
            return NoContent();
        }
        catch (ProduitNotFoundException p)
        {
            return NotFound(p.Message);
        }
        catch (FournisseurNotFoundException p)
        {
            return NotFound(p.Message);
        }
    }  
}
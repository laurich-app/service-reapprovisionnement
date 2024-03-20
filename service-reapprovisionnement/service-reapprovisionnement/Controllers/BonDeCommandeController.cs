using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reapprovisionnement.Models;
using services;
using Securities;
using System.Security.Claims;
using Dtos;
using service_reapprovisionnement.Enum;
using services.Exception;

namespace service_reapprovisionnement.Controllers;
[Route("boncommandes")]
[ApiController]
public class BonDeCommandeController :  ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsHelper _claimsHelper;

    private readonly IBonDeCommandeService _bonDeCommandeService;

    private readonly ILogger<BonDeCommandeController> _logger;

    private Boolean Authentifier()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        return this._claimsHelper.IsGestionaire((ClaimsIdentity)principal.Identity);
    }

    public BonDeCommandeController(ILogger<BonDeCommandeController> logger, IHttpContextAccessor httpContextAccessor, ClaimsHelper claimsHelper, IBonDeCommandeService bonDeCommandeService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _claimsHelper = claimsHelper;
        _bonDeCommandeService = bonDeCommandeService;
    }
    
    // GET /api/fournisseurs
    [HttpGet]
    public IActionResult GetBonDeCommandes([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        if (!this.Authentifier())
            return Unauthorized();
        
        // Logique pour récupérer la liste des fournisseurs paginés
        Pagination<BonDeCommande> p = this._bonDeCommandeService.GetAll(page, limit);

        // Retourne la réponse avec le format spécifié
        return Ok(p);
    }
    
    // GET /fournisseurs/{id}
    [HttpGet("{id}")]
    public IActionResult GetBonDeCommandesById(string id)
    {
        if (!this.Authentifier())
            return Unauthorized();
        // Logique pour récupérer un fournisseur par son ID
        try
        {
            BonDeCommande bonDeCommande = this._bonDeCommandeService.GetById(id);
            return Ok(bonDeCommande);
        }
        catch (BonDeCommandeNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    // GET /fournisseurs/{id}
    [HttpPut("{id}/etat")]
    public IActionResult UpdBonDeCommandeEtat(string id, [FromBody] BonDeCommandeEtatDTO dto)
    {
        if (!this.Authentifier())
            return Unauthorized();
        // Logique pour récupérer un fournisseur par son ID
        try
        {
            BonDeCommande bonDeCommande = this._bonDeCommandeService.UpdateLivraison(id, dto.etat);
            return Ok(bonDeCommande);
        }
        catch (BonDeCommandeNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DejaLivrerEtatInchangeableException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
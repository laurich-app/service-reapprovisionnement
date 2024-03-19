using System.Security.Claims;
using Castle.Core.Configuration;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Reapprovisionnement.Models;
using Securities;
using service_reapprovisionnement.Controllers;
using services;

namespace service_reapprovisionnement.test;
[TestFixture]
public class TestFournisseurController
{
    private FournisseurController _controller;
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private Mock<ClaimsHelper> _claimsHelperMock;
    private Mock<IFournisseurService> _fournisseurServiceMock;
    private Mock<HttpContext> _httpContext;
    private Mock<ClaimsPrincipal> _claimsPrincipal;
    private Mock<ClaimsIdentity> _claimsIdentity;
    private List<Claim> _claims;

    [SetUp]
    public void SetUp()
    {
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _httpContext = new Mock<HttpContext>();
        _claimsPrincipal = new Mock<ClaimsPrincipal>();
        _claimsIdentity = new Mock<ClaimsIdentity>();
        _claimsPrincipal.SetupGet(x => x.Identity).Returns(() => _claimsIdentity.Object);
        _httpContext.SetupGet(x => x.User).Returns(() => _claimsPrincipal.Object);
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(() => _httpContext.Object);
        Claim claim = new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "GESTIONNAIRE");
        _claims = new List<Claim>();
        _claims.Add(claim);
        _claimsIdentity.SetupGet(x => x.Claims).Returns(() => _claims);
        _claimsHelperMock = new Mock<ClaimsHelper>();
        _fournisseurServiceMock = new Mock<IFournisseurService>();

        var loggerMock = new Mock<ILogger<FournisseurController>>();

        _controller = new FournisseurController(
            loggerMock.Object,
            _httpContextAccessorMock.Object,
            _claimsHelperMock.Object,
            _fournisseurServiceMock.Object
        );
    }

    [Test]
    public void GetFournisseurs_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
    {
        // Arrange
        _claims = new List<Claim>();

        // Act
        var result = _controller.GetFournisseurs();

        // Assert
        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
    }

    [Test]
    public void GetFournisseur_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
    {
        // Arrange
        _claims = new List<Claim>();

        // Act
        var result = _controller.GetFournisseur("123");

        // Assert
        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
    }

    [Test]
    public void GetFournisseur_WhenFournisseurExistsAndUserIsAuthenticated_ReturnsOk()
    {
        // Arrange
        _fournisseurServiceMock.Setup(s => s.GetById(It.IsAny<string>())).Returns(new Fournisseur());

        // Act
        var result = _controller.GetFournisseur("123");

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

// Ajoutez des tests similaires pour les autres cas, par exemple lorsque le fournisseur n'existe pas

    [Test]
    public void AddFournisseur_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
    {
        // Arrange
        _claims = new List<Claim>();

        // Act
        var result = _controller.AddFournisseur(new Fournisseur());

        // Assert
        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
    }

    [Test]
    public void AddFournisseur_WhenUserIsAuthenticated_ReturnsCreatedAtAction()
    {
        // Arrange
        _fournisseurServiceMock.Setup(s => s.Create(It.IsAny<Fournisseur>())).Returns(new Fournisseur { Id = "123" });

        // Act
        var result = _controller.AddFournisseur(new Fournisseur());

        // Assert
        Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
    }
    
    [Test]
    public void UpdateFournisseur_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
    {
        // Arrange
        _claims = new List<Claim>();

        // Act
        var result = _controller.UpdateFournisseur("123", new FournisseurUpdate("blabla", "dfghjkk", new List<Produit>()));

        // Assert
        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
    }

    [Test]
    public void UpdateFournisseur_WhenFournisseurExistsAndUserIsAuthenticated_ReturnsOk()
    {
        // Arrange
        _fournisseurServiceMock.Setup(s => s.Update(It.IsAny<string>(), It.IsAny<FournisseurUpdate>())).Returns(new Fournisseur());

        // Act
        var result = _controller.UpdateFournisseur("123", new FournisseurUpdate("blabla", "dfghjkk", new List<Produit>()));

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

// Ajoutez des tests similaires pour les autres cas, par exemple lorsque le fournisseur n'existe pas

    [Test]
    public void DeleteFournisseur_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
    {
        // Arrange
        _claims = new List<Claim>();

        // Act
        var result = _controller.DeleteFournisseur("123");

        // Assert
        Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
    }

    [Test]
    public void DeleteFournisseur_WhenUserIsAuthenticated_ReturnsNoContent()
    {

        // Act
        var result = _controller.DeleteFournisseur("123");

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
    
    [Test]
public void AddProduitToFournisseur_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
{
    // Arrange
    _claims = new List<Claim>();

    // Act
    var result = _controller.AddProduitToFournisseur("123", new Produit());

    // Assert
    Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
}

[Test]
public void AddProduitToFournisseur_WhenFournisseurExistsAndUserIsAuthenticated_ReturnsCreatedAtAction()
{
    // Arrange
    _fournisseurServiceMock.Setup(s => s.AddProduit(It.IsAny<string>(), It.IsAny<Produit>())).Returns(new Produit());

    // Act
    var result = _controller.AddProduitToFournisseur("123", new Produit());

    // Assert
    Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
}

// Ajoutez des tests similaires pour les autres cas, par exemple lorsque le fournisseur n'existe pas ou lorsque le produit existe déjà

[Test]
public void UpdateProduitToFournisseur_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
{
    // Arrange
    _claims = new List<Claim>();

    // Act
    var result = _controller.UpdProduitToFournisseur("123", 1, new ProduitUpd(11));

    // Assert
    Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
}

[Test]
public void UpdateProduitToFournisseur_WhenFournisseurAndProduitExistsAndUserIsAuthenticated_ReturnsOk()
{
    // Arrange
    _fournisseurServiceMock.Setup(s => s.UpdProduit(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<ProduitUpd>())).Returns(new Produit());

    // Act
    var result = _controller.UpdProduitToFournisseur("123", 1, new ProduitUpd(13));

    // Assert
    Assert.That(result, Is.InstanceOf<OkObjectResult>());
}

// Ajoutez des tests similaires pour les autres cas, par exemple lorsque le fournisseur ou le produit n'existe pas

[Test]
public void DeleteProduitToFournisseur_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
{
    // Arrange
    _claims = new List<Claim>();

    // Act
    var result = _controller.DeleteProduitToFournisseur("123", 1);

    // Assert
    Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
}

[Test]
public void DeleteProduitToFournisseur_WhenFournisseurAndProduitExistsAndUserIsAuthenticated_ReturnsNoContent()
{
    
    // Act
    var result = _controller.DeleteProduitToFournisseur("123", 1);

    // Assert
    Assert.That(result, Is.InstanceOf<NoContentResult>());
}
} 

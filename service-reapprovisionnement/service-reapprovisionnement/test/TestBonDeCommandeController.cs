using System.Security.Claims;
using System.Security.Principal;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Reapprovisionnement.Models;
using Securities;
using service_reapprovisionnement.Controllers;
using service_reapprovisionnement.Enum;
using services;
using services.Exception;
using Moq;
using NUnit.Framework;

namespace service_reapprovisionnement.test;
[TestFixture]
public class TestBonDeCommandeController
{
        private BonDeCommandeController _controller;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<HttpContext> _httpContext;
        private Mock<ClaimsPrincipal> _claimsPrincipal;
        private Mock<ClaimsIdentity> _claimsIdentity;
        private Mock<ClaimsHelper> _claimsHelperMock;
        private Mock<IBonDeCommandeService> _bonDeCommandeServiceMock;
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
            _bonDeCommandeServiceMock = new Mock<IBonDeCommandeService>();

            var loggerMock = new Mock<ILogger<BonDeCommandeController>>();

            _controller = new BonDeCommandeController(
                loggerMock.Object,
                _httpContextAccessorMock.Object,
                _claimsHelperMock.Object,
                _bonDeCommandeServiceMock.Object
            );
        }

        [Test]
        public void GetBonDeCommandes_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
        {
            // Arrange
            _claims = new List<Claim>();
            // Act
            var result = _controller.GetBonDeCommandes();

            // Assert
            //Assert.IsInstanceOf<UnauthorizedResult>(result);
            Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        }

        [Test]
        public void GetBonDeCommandes_WhenUserIsAuthenticated_ReturnsOk()
        {
            var pagination = new Pagination<BonDeCommande>(new List<BonDeCommande>(), new Paginated(0, 0, 10));

            _bonDeCommandeServiceMock
                .Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagination);

            // Act
            var result = _controller.GetBonDeCommandes();

            // Assert
            //Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetBonDeCommandesById_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
        {
            // Arrange
            _claims = new List<Claim>();

            // Act
            var result = _controller.GetBonDeCommandesById("123");

            // Assert
            //Assert.IsInstanceOf<UnauthorizedResult>(result);
            Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        }

        [Test]
        public void GetBonDeCommandesById_WhenUserIsAuthenticatedAndBonDeCommandeExists_ReturnsOk()
        {
            // Arrange
            _bonDeCommandeServiceMock.Setup(s => s.GetById(It.IsAny<string>())).Returns(new BonDeCommande());

            // Act
            var result = _controller.GetBonDeCommandesById("123");

            // Assert
            //Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetBonDeCommandesById_WhenUserIsAuthenticatedAndBonDeCommandeDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _bonDeCommandeServiceMock.Setup(s => s.GetById(It.IsAny<string>())).Throws<BonDeCommandeNotFoundException>();

            // Act
            var result = _controller.GetBonDeCommandesById("123");

            // Assert
            //Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public void UpdBonDeCommandeEtat_WhenUserIsNotAuthenticated_ReturnsUnauthorized()
        {
            // Arrange
            _claims = new List<Claim>();
            // Act
            var result = _controller.UpdBonDeCommandeEtat("123", new EtatCommande());

            // Assert
            //Assert.IsInstanceOf<UnauthorizedResult>(result);
            Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        }

        [Test]
        public void UpdBonDeCommandeEtat_WhenUserIsAuthenticatedAndBonDeCommandeExists_ReturnsOk()
        {
            // Arrange
            _bonDeCommandeServiceMock.Setup(s => s.UpdateLivraison(It.IsAny<string>(), It.IsAny<EtatCommande>())).Returns(new BonDeCommande());

            // Act
            var result = _controller.UpdBonDeCommandeEtat("123", new EtatCommande());

            // Assert
           //Assert.IsInstanceOf<OkObjectResult>(result);
           Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void UpdBonDeCommandeEtat_WhenUserIsAuthenticatedAndBonDeCommandeDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _bonDeCommandeServiceMock.Setup(s => s.UpdateLivraison(It.IsAny<string>(), It.IsAny<EtatCommande>())).Throws<BonDeCommandeNotFoundException>();

            // Act
            var result = _controller.UpdBonDeCommandeEtat("123", new EtatCommande());

            // Assert
            //Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
}
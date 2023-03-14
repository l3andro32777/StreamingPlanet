using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StreamingPlanet.Data;
using Moq;
using StreamingPlanet.Models;
using StreamingPlanet.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Builder;
using System.Net;
using StreamingPlanet;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace StreamingPlanetTest
{
    public class UserUnitTests : IClassFixture<ApplicationDbContextFixture>
    {
        private ApplicationDbContext _context;

        public UserUnitTests(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;
        }

        [Fact]
        public async void Register_With_Valid_Password_Returns_Success()
        {
            // Arrange
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<CinemaUser>>();
            var validator = new Mock<IUserValidator<CinemaUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<CinemaUser>>();
            pwdValidators.Add(new PasswordValidator<CinemaUser>());
            var userManager = new UserManager<CinemaUser>(new UserStore<CinemaUser>(_context), options.Object, new PasswordHasher<CinemaUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<CinemaUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<CinemaUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            var bookUsr = new CinemaUser() { FullName = "testeNome", UserName = "testeUsername" };

            // Act
            var result = await userManager.CreateAsync(bookUsr, "T€ste1");

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async void Register_With_Invalid_Password_Returns_Fail()
        {
            // Arrange
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<CinemaUser>>();
            var validator = new Mock<IUserValidator<CinemaUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<CinemaUser>>();
            pwdValidators.Add(new PasswordValidator<CinemaUser>());
            var userManager = new UserManager<CinemaUser>(new UserStore<CinemaUser>(_context), options.Object, new PasswordHasher<CinemaUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<CinemaUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<CinemaUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            var bookUsr = new CinemaUser() { FullName = "testeNome", UserName = "testeUsername" };

            // Act
            var result = await userManager.CreateAsync(bookUsr, "teste");

            // Assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public async void Register_Two_Identical_Users_Should_Throw_Exception()
        {
            // Arrange
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<CinemaUser>>();
            var validator = new Mock<IUserValidator<CinemaUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<CinemaUser>>();
            pwdValidators.Add(new PasswordValidator<CinemaUser>());
            var userManager = new UserManager<CinemaUser>(new UserStore<CinemaUser>(_context), options.Object, new PasswordHasher<CinemaUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<CinemaUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<CinemaUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            var bookUsr1 = new CinemaUser() { FullName = "testeNome", UserName = "testeUsername" };
            var bookUsr2 = new CinemaUser() { FullName = "testeNome", UserName = "testeUsername" };

            // Act
            await userManager.CreateAsync(bookUsr1, "T€ste1");

            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => userManager.CreateAsync(bookUsr2, "T€ste1"));
        }

        [Fact]
        public async Task CheckSignIn_With_Created_User_Returns_Success()
        {
            //Arrange
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<CinemaUser>>();
            var validator = new Mock<IUserValidator<CinemaUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<CinemaUser>>();
            pwdValidators.Add(new PasswordValidator<CinemaUser>());
            var userManager = new UserManager<CinemaUser>(new UserStore<CinemaUser>(_context), options.Object, new PasswordHasher<CinemaUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<CinemaUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<CinemaUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<CinemaUser>>();
            var signInManager = new SignInManager<CinemaUser>(userManager, mockContextAccessor.Object, mockPrincipalFactory.Object, options.Object,
                                                            new Mock<ILogger<SignInManager<CinemaUser>>>().Object,
                                                            new Mock<IAuthenticationSchemeProvider>().Object, new Mock<IUserConfirmation<CinemaUser>>().Object);

            var bookUsr = new CinemaUser() { FullName = "testeNome", UserName = "teste@teste.com" };

            // Act
            await userManager.CreateAsync(bookUsr, "T€ste1");
            var result = await signInManager.CheckPasswordSignInAsync(bookUsr, "T€ste1", false);

            //Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task CheckSignIn_With_NonExisting_User_Returns_Fail()
        {
            //Arrange
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<CinemaUser>>();
            var validator = new Mock<IUserValidator<CinemaUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<CinemaUser>>();
            pwdValidators.Add(new PasswordValidator<CinemaUser>());
            var userManager = new UserManager<CinemaUser>(new UserStore<CinemaUser>(_context), options.Object, new PasswordHasher<CinemaUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<CinemaUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<CinemaUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<CinemaUser>>();
            var signInManager = new SignInManager<CinemaUser>(userManager, mockContextAccessor.Object, mockPrincipalFactory.Object, options.Object,
                                                            new Mock<ILogger<SignInManager<CinemaUser>>>().Object,
                                                            new Mock<IAuthenticationSchemeProvider>().Object, new Mock<IUserConfirmation<CinemaUser>>().Object);

            var bookUsr = new CinemaUser() { FullName = "testeNome", UserName = "teste@teste.com" };

            //Act
            var result = await signInManager.CheckPasswordSignInAsync(bookUsr, "T€ste1", false);

            //Assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public async void ChangePassword_Should_Return_Success()
        {
            // Arrange
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<CinemaUser>>();
            var validator = new Mock<IUserValidator<CinemaUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<CinemaUser>>();
            pwdValidators.Add(new PasswordValidator<CinemaUser>());
            var userManager = new UserManager<CinemaUser>(new UserStore<CinemaUser>(_context), options.Object, new PasswordHasher<CinemaUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<CinemaUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<CinemaUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            var bookUsr = new CinemaUser() { FullName = "testeNome", UserName = "testeUsername" };
            var oldPassword = "T€ste1";
            var newPassword = "NewP@ss1";

            // Act
            await userManager.CreateAsync(bookUsr, oldPassword);
            var result = await userManager.ChangePasswordAsync(bookUsr, oldPassword, newPassword);

            // Assert
            Assert.True(result.Succeeded);
        }
    }
}
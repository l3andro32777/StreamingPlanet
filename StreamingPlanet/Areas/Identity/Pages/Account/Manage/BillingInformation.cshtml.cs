using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StreamingPlanet.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace StreamingPlanet.Areas.Identity.Pages.Account.Manage
{
    public class BillingInformationModel : PageModel
    {
        private readonly UserManager<CinemaUser> _userManager;
        private readonly SignInManager<CinemaUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public BillingInformationModel(UserManager<CinemaUser> userManager, SignInManager<CinemaUser> signInManager, ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [Display(Name = "Nome do titular")]
            public string FullName { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [Display(Name = "Número do cartão de crédito/débito")]
            public string CardNumber { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Data de validade")]
            public DateTime ExpirationDate { get; set; }

            [Required]
            [Display(Name = "CVV/CVC")]
            public int CCV { get; set; }

            [Required]
            [Display(Name = "Morada")]
            public string Address1 { get; set; }

            [Display(Name = "Morada alternativa (opcional)")]
            public string Address2 { get; set; }

            [Required]
            [Display(Name = "Código postal")]
            public string PostalCode { get; set; }

            [Required]
            [Display(Name = "Cidade")]
            public string City { get; set; }

            [Required]
            [Display(Name = "País")]
            public string Country { get; set; }

            [Required]
            [Display(Name = "Número de telemóvel")]
            public string PhoneNumber { get; set; }

        }

        private async Task LoadAsync(CinemaUser user)
        {
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Input = new InputModel
            {
                FullName = user.FullName,
                CardNumber = user.CardNumber,
                ExpirationDate = user.ExpirationDate, 
                CCV = user.CCV,
                Address1 = user.Address1,
                Address2 = user.Address2,
                PostalCode = user.PostalCode,
                City = user.City,
                Country = user.Country,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.FullName = Input.FullName;
            user.CardNumber = Input.CardNumber;
            user.ExpirationDate = Input.ExpirationDate;
            user.CCV = Input.CCV;
            user.Address1 = Input.Address1;
            user.Address2 = Input.Address2;
            user.PostalCode = Input.PostalCode;
            user.City = Input.City;
            user.Country = Input.Country;
            user.PhoneNumber = Input.PhoneNumber;

            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User successfully updated their billing information.");
            StatusMessage = "Dados de pagamento atualizados com sucesso.";

            return RedirectToPage();
        }
    }
}
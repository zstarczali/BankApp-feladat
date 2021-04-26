using System;
using System.Collections.Generic;
using BankSystem.Services.BankAccount;

namespace BankSystem.Web.Pages.Account
{
    using BankSystem.Models;

    using Common;
    using Common.EmailSender.Interface;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using System.ComponentModel.DataAnnotations;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class RegisterModel : BasePageModel
    {
        private readonly IEmailSender emailSender;
        private readonly ILogger<RegisterModel> logger;
        private readonly UserManager<BankUser> userManager;
        private readonly IBankAccountUniqueIdHelper uniqueIdHelper;


        public RegisterModel(
            UserManager<BankUser> userManager,
            ILogger<RegisterModel> logger, 
            IEmailSender emailSender, IBankAccountUniqueIdHelper uniqueIdHelper)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.uniqueIdHelper = uniqueIdHelper;
        }

        [BindProperty]
        public InputModel Input { get; set; }


        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (this.User.Identity.IsAuthenticated)
            {
                return this.LocalRedirect(returnUrl);
            }

            this.ReturnUrl = returnUrl;

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (this.User.Identity.IsAuthenticated)
            {
                return this.LocalRedirect(returnUrl);
            }

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var mainAccountId = this.uniqueIdHelper.GenerateAccountUniqueId();
            var savingsAccountId = this.uniqueIdHelper.GenerateAccountUniqueId();

            var user = new BankUser
            {
                UserName = this.Input.Email,
                Email = this.Input.Email,
                FullName = this.Input.FullName,
                EmailConfirmed = true, // just for test purposes

                BankAccounts = new List<BankAccount> 
                {
                    new BankAccount
                    {
                        Balance = 0,
                        Credit = 50000,
                        InterestRate = 10,

                        Currency = BankAccount.CurrencyType.HUF, 
                        CreatedOn = DateTime.UtcNow,
                        Name = "Main account",
                        UniqueId = mainAccountId,
                        TypeId = BankAccount.AccountType.NormalAccount
                    },
                    new BankAccount
                    {
                        Balance = 5000, // one time bonus
                        Credit = 0,
                        InterestRate = 0,

                        Currency = BankAccount.CurrencyType.HUF, 
                        CreatedOn = DateTime.UtcNow,
                        Name = "Savings account",
                        UniqueId = savingsAccountId,
                        TypeId = BankAccount.AccountType.SavingAccount
                    }
                }
            };

            var result = await userManager.CreateAsync(user, this.Input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.Page();
            }

            this.logger.LogInformation("User created a new account with password.");

            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = this.Url.Page(
                EmailMessages.EmailConfirmationPage,
                null,
                new { userId = user.Id, code },
                this.Request.Scheme);
            await this.emailSender.SendEmailAsync(GlobalConstants.BankSystemEmail, this.Input.Email,
                EmailMessages.ConfirmEmailSubject,
                string.Format(EmailMessages.EmailConfirmationMessage, HtmlEncoder.Default.Encode(callbackUrl)));

            this.ShowSuccessMessage(NotificationMessages.SuccessfulRegistration);
            return this.RedirectToLoginPage();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [MaxLength(ModelConstants.User.FullNameMaxLength)]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [StringLength(ModelConstants.User.PasswordMaxLength, MinimumLength = ModelConstants.User.PasswordMinLength)]
            [DataType(DataType.Password)]
            [RegularExpression(ModelConstants.User.PasswordRegex,
                ErrorMessage = ModelConstants.User.PasswordErrorMessage)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

    }
}
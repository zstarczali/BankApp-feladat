using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BankSystem.Services.Models.CashTransfer
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using BankSystem.Models;
    using Common;
    using Common.AutoMapping.Interfaces;

    public class CashTransferCreateServiceModel : CashTransferBaseServiceModel
    {
        public string Description { get; set; }

        [Required] 
        public decimal Amount { get; set; }

        [Required] 
        public string AccountId { get; set; }

        [Required]
        public string ReferenceNumber { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public string RecipientName { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public string Destination { get; set; }
    }
}
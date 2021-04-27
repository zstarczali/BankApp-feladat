﻿namespace BankSystem.Services.Tests.Tests
{
    using System;
    using AutoMapper;
    using Common.Configuration;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Moq;
    using Setup;

    public abstract class BaseTest
    {
        private const string SampleBankName = "OtpBank";
        private const string SampleUniqueIdentifier = "ABC";
        private const string SampleFirst3CardDigits = "123";
        private const string SampleBankCountry = "Hungary";

        protected BaseTest()
            => this.Mapper = TestSetup.InitializeMapper();

        protected IMapper Mapper { get; }

        protected BankSystemDbContext DatabaseInstance
        {
            get
            {
                var options = new DbContextOptionsBuilder<BankSystemDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
                    .Options;

                return new BankSystemDbContext(options);
            }
        }

        protected Mock<IOptions<BankConfiguration>> MockedBankConfiguration
        {
            get
            {
                var bankConfiguration = new BankConfiguration
                {
                    BankName = SampleBankName,
                    UniqueIdentifier = SampleUniqueIdentifier,
                    First3CardDigits = SampleFirst3CardDigits,
                    Country = SampleBankCountry,
                };

                var options = new Mock<IOptions<BankConfiguration>>();
                options.Setup(x => x.Value).Returns(bankConfiguration);

                return options;
            }
        }
    }
}
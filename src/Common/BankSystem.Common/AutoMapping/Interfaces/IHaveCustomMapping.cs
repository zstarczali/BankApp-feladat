using AutoMapper;

namespace BankSystem.Common.AutoMapping.Interfaces
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
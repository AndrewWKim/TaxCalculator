using AutoMapper;
using TaxCalculator.Models.Entities;
using TaxCalculator.Models.RequestModels;
using TaxCalculator.Models.ViewModels;

namespace TaxCalculator.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapTaxPayerContract();
        }

        private void MapTaxPayerContract()
        {
            CreateMap<TaxPayerContract, TaxPayerContractViewModel>()
                .ForMember(
                    dest => dest.CharitySpent,
                    opt => opt.MapFrom(src => src.CharitySpent.HasValue ? src.CharitySpent : 0))
                .ForMember(
                    dest => dest.IncomeTax,
                    opt => opt.MapFrom(src => src.IncomeTax.HasValue ? src.IncomeTax : 0))
                .ForMember(
                    dest => dest.SocialTax,
                    opt => opt.MapFrom(src => src.SocialTax.HasValue ? src.SocialTax : 0))
                .ForMember(
                    dest => dest.TotalTax,
                    opt => opt.MapFrom(src => src.TotalTax.HasValue ? src.TotalTax : 0));
            CreateMap<TaxPayerContractModel, TaxPayerContract>();
        }
    }
}
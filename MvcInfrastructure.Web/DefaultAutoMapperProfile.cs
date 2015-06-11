using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MvcInfrastructure.Sample.Business;
using MvcInfrastructure.Sample.Web.Models;

namespace MvcInfrastructure.Sample.Web
{
    public class DefaultAutoMapperProfile : Profile
    {
        public class NameResolver : ValueResolver<User, string>
        {
            protected override string ResolveCore(User source)
            {
                return string.Format("{0} {1}", source.FirstName, source.LastName);
            }
        }

        public class PriceResolver : ValueResolver<Product, decimal>
        {
            protected override decimal ResolveCore(Product source)
            {
                return source.Price - source.DisCount;
            }
        }

        public override string ProfileName
        {
            get { return "DefaultAutoMapperProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<User, TestViewModel>()
              .ForMember(dest => dest.ID, opt => opt.UseValue<string>(Guid.NewGuid().ToString()))
              .ForMember(dest => dest.UserName, opt => opt.ResolveUsing<NameResolver>());

            Mapper.CreateMap<Product, TestViewModel>()
             .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.ProructPrice, opt => opt.ResolveUsing<PriceResolver>());


        }
    }
}

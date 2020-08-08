using AutoMapper;
using DataApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApi.Mapping
{
    public class PersonViewModelMapping : Profile
    {

        //public PersonViewModelMapping()
        //{
        //    CreateMap<Person, PersonViewModel>()
        //        .ForMember(x=>x.Country, z=>z.MapFrom(y=>y.CountryCodeNavigation.Txt3))
        //        .ForMember(x => x.Greeting, z => z.MapFrom(y => y.Greeting.Txt2))
        //        .ForMember(x => x.Contact, z => z.MapFrom(y => y.PersonContact.FirstOrDefault().Txt));


        //}

    }
}

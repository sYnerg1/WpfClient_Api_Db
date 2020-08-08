using AutoMapper;
using DataApi.BAL.DTOs;
using DataApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataApi.BAL.Mapping
{
    public class BisnessMapping:Profile
    {
        public BisnessMapping()
        {
            CreateMap<Person, PersonDTO>()
                .ForMember(x => x.Country1, z => z.MapFrom(y => y.CountryCodeNavigation.Txt1))
                .ForMember(x => x.Country2, z => z.MapFrom(y => y.CountryCodeNavigation.Txt2))
                .ForMember(x => x.Country3, z => z.MapFrom(y => y.CountryCodeNavigation.Txt3))
                .ForMember(x => x.Country4, z => z.MapFrom(y => y.CountryCodeNavigation.Txt4))
                .ForMember(x => x.Greeting1, z => z.MapFrom(y => y.Greeting.Txt1))
                .ForMember(x => x.Greeting2, z => z.MapFrom(y => y.Greeting.Txt2))
                .ForMember(x => x.Greeting3, z => z.MapFrom(y => y.Greeting.Txt3))
                .ForMember(x => x.Greeting4, z => z.MapFrom(y => y.Greeting.Txt4))
                .ForMember(x => x.Contact, z => z.MapFrom(y => y.PersonContact.FirstOrDefault().Txt));
        }
    }
}

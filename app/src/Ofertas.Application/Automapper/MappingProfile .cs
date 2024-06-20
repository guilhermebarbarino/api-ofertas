using AutoMapper;
using Ofertas.Application.ViewModels;
using Ofertas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ofertas.Application.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Oferta,OfertaRequest>().ReverseMap();
            CreateMap<Oferta, OfertaResponse>().ReverseMap();
        }
    }
}

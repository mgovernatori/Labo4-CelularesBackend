using AutoMapper;
using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Celular.DTO;
using CelularesAPI.Models.Marca.DTO;
using CelularesAPI.Models.Sistema;
using CelularesAPI.Models.Sistema.DTO;
using CelularesAPI.Models.Usuario;
using CelularesAPI.Models.Usuario.DTO;
using CelularesAPI.Models.Marca;
using CelularesAPI.Models.Color;

namespace CelularesAPI.Config
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);


            CreateMap<Celular, CelularDTO>()
                .ForMember(dest => dest.Colores, opts => opts.Ignore());

            CreateMap<CelularDTO, Celular>()
                .ForMember(dest => dest.Colores, opts => opts.Ignore());


            CreateMap<Celular, CelularesDTO>().ReverseMap();
            CreateMap<Celular, CreateCelularDTO>().ReverseMap();
            CreateMap<UpdateCelularDTO, Celular>()
                .ForMember(dest => dest.Colores, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));  
            CreateMap<CreateCelularDTO, Celular>().ForMember(
                dest => dest.Colores, opt => opt.Ignore()
            );



            CreateMap<CreateMarcaDTO, Marca>().ReverseMap();
            CreateMap<UpdateMarcaDTO, Marca>().ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });

            CreateMap<CreateSistemaDTO, Sistema>().ReverseMap();
            CreateMap<UpdateSistemaDTO, Sistema>().ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, UsuariosDTO>().ReverseMap();
            CreateMap<Usuario, CreateUsuarioDTO>().ReverseMap();
            CreateMap<UpdateUsuarioDTO, Usuario>().ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });

        }
    }
}

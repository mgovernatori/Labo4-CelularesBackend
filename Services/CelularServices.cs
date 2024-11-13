using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using AutoMapper;
using CelularesAPI.Repositories;
using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Celular.DTO;
using CelularesAPI.Models.Color;
using CelularesAPI.Models.Color.DTO;

namespace CelularesAPI.Services
{
    public class CelularServices
    {
        private readonly IMapper _mapper;
        private readonly ICelularRepository _celularRepository;
        private readonly ColorServices _colorServices;
        private readonly IColoresCelularRepository _coloresCelularRepository;
        private readonly MarcaServices _marcaServices;
        private readonly SistemaServices _sistemaServices;

        public CelularServices(IMapper mapper, 
            ICelularRepository celularRepository, 
            ColorServices colorServices,
            IColoresCelularRepository coloresCelularRepository, 
            MarcaServices marcaServices, 
            SistemaServices sistemaServices 
        )
        {
            _mapper = mapper;
            _celularRepository = celularRepository;
            _colorServices = colorServices;
            _coloresCelularRepository = coloresCelularRepository;
            _marcaServices = marcaServices;
            _sistemaServices = sistemaServices;
        }

        private async Task<Celular> GetOneByIdOrException(int id)
        {
            var celular = await _celularRepository.GetOne(c => c.Id == id); 
            if (celular == null)
            {
                throw new Exception();
            }
            return celular;
        }

        public async Task<CelularDTO> GetOne(int id)
        {
            var celular = await GetOneByIdOrException(id);

            if (celular == null)
            {
                throw new Exception();
            }

            var coloresCelular = await _coloresCelularRepository.GetAll(cc => cc.IdCelular == id);

            var ids = coloresCelular.Select(cc => cc.IdColor).ToList(); 
            var todosColores = await _colorServices.GetAllFromCelular(ids);

            var coloresDict = todosColores.ToDictionary(c => c.Id, c => c); 

            var coloresImagen = new List<ColorImagen>();

            foreach (var cc in coloresCelular)
            {
                if (coloresDict.TryGetValue(cc.IdColor, out var color))
                {
                    coloresImagen.Add(new ColorImagen
                    {
                        NombreColor = color.Nombre,
                        UrlImagen = cc.UrlImagen
                    });
                }
            }

            var celularDTO = _mapper.Map<CelularDTO>(celular);
            celularDTO.Colores = coloresImagen; 
            
            return celularDTO;
        }

        public async Task<List<CelularesDTO>> GetAll()
        {
            var celulares = await _celularRepository.GetAll();

            var ids = celulares.Select(c => c.Id).ToList();

            var colores = await _coloresCelularRepository.GetAll(cc => ids.Contains(cc.IdCelular));

            var coloresDict = colores.
                GroupBy(cc => cc.IdCelular).
                ToDictionary(g => g.Key, g => g.First().UrlImagen);
           
            var celsDTO = new List<CelularesDTO>();

            foreach (var c in celulares)
            {
                var celDTO = _mapper.Map<CelularesDTO>(c);

                if (coloresDict.TryGetValue(c.Id, out var urlImagen))
                {
                    celDTO.Imagen = urlImagen;
                }

                celsDTO.Add(celDTO);
            }

            return celsDTO;
        }

        public async Task<List<CelularesDTO>> GetAllByMarca(string marca)
        {
            var celulares = await _celularRepository.GetAll(c => c.Marca.Nombre == marca);
            return _mapper.Map<List<CelularesDTO>>(celulares);
        }


        public async Task<Celular> CreateOne(CreateCelularDTO createCelularDTO)
        {
            var celular = _mapper.Map<Celular>(createCelularDTO);

            celular.Colores = new List<Color>();

            var marca = await _marcaServices.GetOne(celular.IdMarca);
            if (marca == null)
            {
                throw new InvalidOperationException($"La marca no existe.");
            }

            var sistema = await _sistemaServices.GetOne(celular.IdSistema);
            if (sistema == null)
            {
                throw new InvalidOperationException($"El sistema no existe.");
            }


            await _celularRepository.Add(celular);


            foreach (var col in createCelularDTO.Colores)
            {
                var color = await _colorServices.GetOneByNombre(col.NombreColor);

                if (color == null)
                {
                    color = new Color { Nombre = col.NombreColor };
                    await _colorServices.CreateOne(color);
                }
                celular.Colores.Add(color);

                var colorCelular = new ColoresCelular
                {
                    IdCelular = celular.Id,
                    IdColor = color.Id,
                    UrlImagen = col.UrlImagen,
                };

                await _coloresCelularRepository.Add(colorCelular);
            }
            return celular;
        }

        public async Task<Celular> UpdateOne(int id, UpdateCelularDTO updateCelularDTO)
        {
            var celular = await GetOneByIdOrException(id);

            var celularMapped = _mapper.Map(updateCelularDTO, celular);


            await _marcaServices.GetOne(celular.IdMarca);
            await _sistemaServices.GetOne(celular.IdSistema);

            if (updateCelularDTO.Colores != null)
            {
                foreach (var col in updateCelularDTO.Colores)
                {

                    var color = await _colorServices.GetOneByNombre(col.NombreColor) ?? new Color { Nombre = col.NombreColor };

                    if (color.Id == 0)
                    {
                        await _colorServices.CreateOne(color);
                    }

                    var colorCelular = await _coloresCelularRepository.GetOne(cc => cc.IdCelular == celular.Id && cc.IdColor == color.Id);

                    if (colorCelular == null)
                    {
                        colorCelular = new ColoresCelular
                        {
                            IdCelular = celular.Id,
                            IdColor = color.Id,
                            UrlImagen = col.UrlImagen
                        };

                        await _coloresCelularRepository.Add(colorCelular);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(col.UrlImagen))
                        {
                            colorCelular.UrlImagen = col.UrlImagen;
                            await _coloresCelularRepository.Update(colorCelular);
                        }
                    }
                }
            }
            await _celularRepository.Update(celularMapped);

            return celularMapped;
        }


        public async Task DeleteOne(int id)
        {
            var celular = await GetOneByIdOrException(id);

            await _celularRepository.Delete(celular);
        }


    }
}

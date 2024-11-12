using CelularesAPI.Models.Celular.DTO;
using CelularesAPI.Models.Celular;
using CelularesAPI.Models.Color;
using CelularesAPI.Repositories;
using CelularesAPI.Models.Color.DTO;
using AutoMapper;

namespace CelularesAPI.Services
{
    public class ColorServices
    {
        private readonly IMapper _mapper;
        private readonly IColorRepository _colorRepository;

        public ColorServices(IMapper mapper, IColorRepository colorRepository)
        {
            _mapper = mapper;
            _colorRepository = colorRepository;
        }

        public async Task<Color> GetOneById(int id)
        {
            var color = await _colorRepository.GetOne(c => c.Id == id);
            return color;
        }


        public async Task<Color> GetOneByNombre(string nombre)
        {
            var color = await _colorRepository.GetOne(c => c.Nombre == nombre);
            return color;
        }

        public async Task<List<Color>> GetAllFromCelular(List<int> ids)
        {
            var colores = await _colorRepository.GetAll(c => ids.Contains(c.Id));
            return colores.ToList();
        }


        public async Task<Color> CreateOne(Color color)
        {

            await _colorRepository.Add(color);
            return color;
        }
    }
}

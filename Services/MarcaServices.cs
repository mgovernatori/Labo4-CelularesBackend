using AutoMapper;
using CelularesAPI.Models.Marca.DTO;
using CelularesAPI.Repositories;
using CelularesAPI.Models.Marca;

namespace CelularesAPI.Services
{
    public class MarcaServices
    {

        private readonly IMapper _mapper;
        private readonly IMarcaRepository _marcaRepository;

        public MarcaServices(IMapper mapper, IMarcaRepository marcaRepository)
        {
            _mapper = mapper;
            _marcaRepository = marcaRepository;
        }

        public async Task<List<Marca>> GetAll()
        {
            var marcas = await _marcaRepository.GetAll();
            return marcas.ToList();
        }

        public async Task<Marca> GetOne(int id)
        {
            var marca = await _marcaRepository.GetOne(m => m.Id == id);
            if (marca == null)
            {
                throw new Exception();
            }
            return marca;
        }

        public async Task<Marca> CreateOne(CreateMarcaDTO createMarcaDTO)
        {
            var marca = _mapper.Map<Marca>(createMarcaDTO);
            await _marcaRepository.Add(marca);
            return marca;
        }

        public async Task<Marca> UpdateOne(int id, UpdateMarcaDTO updateMarcaDTO)
        {
            var marca = await GetOne(id);
            var marcaMapped = _mapper.Map(updateMarcaDTO, marca);

            await _marcaRepository.Update(marcaMapped);
            return marcaMapped;
        }

        public async Task DeleteOne(int id)
        {
            var marca = await GetOne(id);
            await _marcaRepository.Delete(marca);
        }

    }
}

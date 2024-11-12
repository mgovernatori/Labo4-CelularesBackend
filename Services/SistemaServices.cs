using AutoMapper;
using CelularesAPI.Models.Marca.DTO;
using CelularesAPI.Models.Marca;
using CelularesAPI.Models.Sistema;
using CelularesAPI.Models.Sistema.DTO;
using CelularesAPI.Repositories;

namespace CelularesAPI.Services
{
    public class SistemaServices
    {
        private readonly IMapper _mapper;
        private readonly ISistemaRepository _sistemaRepository;

        public SistemaServices(IMapper mapper, ISistemaRepository sistemaRepository)
        {
            _mapper = mapper;
            _sistemaRepository = sistemaRepository;
        }

        public async Task<List<Sistema>> GetAll()
        {
            var sistemas = await _sistemaRepository.GetAll();
            return sistemas.ToList();
        }

        public async Task<Sistema> GetOne(int id)
        {
            var sistema = await _sistemaRepository.GetOne(s => s.Id == id);
            if (sistema == null)
            {
                throw new Exception();
            }
            return sistema;
        }

        public async Task<Sistema> CreateOne(CreateSistemaDTO createSistemaDTO)
        {
            var sistema = _mapper.Map<Sistema>(createSistemaDTO);
            await _sistemaRepository.Add(sistema);
            return sistema;
        }

        public async Task<Sistema> UpdateOne(int id, UpdateSistemaDTO updateSistemaDTO)
        {
            var sistema = await GetOne(id);
            var sistemaMapped = _mapper.Map(updateSistemaDTO, sistema);

            await _sistemaRepository.Update(sistemaMapped);
            return sistemaMapped;
        }

        public async Task DeleteOne(int id)
        {
            var sistema = await GetOne(id);
            await _sistemaRepository.Delete(sistema);
        }
    }
}

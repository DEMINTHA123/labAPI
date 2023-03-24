using AutoMapper;
using labAPI.DTOs.EquipmentDTO;
using labAPI.Entities;
using labAPI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace labAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        protected readonly IEquipmentRepository _repository;
        protected readonly IMapper _mapper;

        public EquipmentController(
            IEquipmentRepository repository,
            IMapper mapper
            )
        {
            _repository= repository;
            _mapper= mapper;
        }
        // GET: api/<EquimpentController>
        [HttpGet]
        public async Task<IEnumerable<EquipmentOutputDTO>> Get()
        {
            return _mapper.Map<IEnumerable<Equipment>, IEnumerable<EquipmentOutputDTO>>(await _repository.GetAll());
        }

        // GET api/<EquimpentController>/5
        [HttpGet("{id}")]
        public async Task<EquipmentOutputDTO> Get(int id)
        {
            return _mapper.Map<Equipment, EquipmentOutputDTO>(await _repository.GetById(id));
        }

        // POST api/<EquimpentController>
        [HttpPost]
        public async void Post(EquipmentInputDTO equipmentDTO)
        {
            await _repository.Add(_mapper.Map<Equipment>(equipmentDTO));
        }

        [HttpPost("upload-image")]
        public async void Uploadimage(EquipmentInputDTO equipmentDTO)
        {
            await _repository.Add(_mapper.Map<Equipment>(equipmentDTO));
        }
        // PUT api/<EquimpentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EquimpentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

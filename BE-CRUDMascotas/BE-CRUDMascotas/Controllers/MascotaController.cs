using AutoMapper;
using BE_CRUDMascotas.Models;
using BE_CRUDMascotas.Models.DTO;
using BE_CRUDMascotas.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly IMascotaRepository _mascotaRepository;
        private readonly IMapper _mapper;


        public MascotaController(IMapper mapper, IMascotaRepository mascotaRepository)
        {
            _mapper = mapper;
            _mascotaRepository = mascotaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listMascotas = await _mascotaRepository.GetListMascotas();

                var listMascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listMascotas);
                return Ok(listMascotasDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascotaById(id);

                if (mascota == null) return NotFound();

                var mascotaDTO = _mapper.Map<MascotaDTO>(mascota);

                return Ok(mascotaDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascotaById(id);

                if (mascota == null) return NotFound();

                await _mascotaRepository.DeleteMascotaById(mascota);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(MascotaDTO mascotaDTO)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDTO);

                mascota.FechaCreacion = DateTime.Now;

                mascota = await _mascotaRepository.AddMascota(mascota);

                //Devuelve ruta Get con el registro creado

                var mascotaItemDto = _mapper.Map<MascotaDTO>(mascota);
                return CreatedAtAction("Get", new { id = mascota.Id }, mascotaItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MascotaDTO mascotaDto)
        {
            try
            {

                var mascota = _mapper.Map<Mascota>(mascotaDto);

                if (id != mascota.Id) return BadRequest();

                var mascotaItem = await _mascotaRepository.GetMascotaById(id);

                if (mascotaItem == null) return NotFound();

                await _mascotaRepository.UpdateMascota(mascota);

                return NoContent();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

using AutoMapper;
using MagicVilla_Api.Datos;
using MagicVilla_Api.Modelos;
using MagicVilla_Api.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

//video de You tube https://www.youtube.com/watch?v=OuiExAqVapk&ab_channel=BaezStoneCreators minuto 2:37:23

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger <VillaController> _logger;
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDBContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>>GetVillas()
        {
            _logger.LogInformation("Obtener las villas");

            IEnumerable<Villa> VillaList = await _dbContext.Villas.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<VillaDto>>(VillaList));
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if(id == 0) {
                _logger.LogError("Error al obtener los datos de la villa de ID : "+id);
                return BadRequest();
            }
            //var Villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var Villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);

            if (Villa == null) { return NotFound(); }

            return Ok(_mapper.Map<VillaDto>(Villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody]VillaCreateDto createDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    
            
            if(await _dbContext.Villas.FirstOrDefaultAsync(v =>v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }


            if(createDto == null) 
            {
                return BadRequest(createDto); 
            }

            Villa modelo =  _mapper.Map<Villa>(createDto);        
           
            await _dbContext.Villas.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();
            
            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbContext.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null) { return NotFound(); }

            _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDto updateDto) 
        {
            if(updateDto == null || id!= updateDto.Id) 
            { 
                return BadRequest();
            }
           
            Villa modelo = _mapper.Map<Villa>(updateDto);

            _dbContext.Villas.Update(modelo);
            await _dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateParvialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
                       
            if(villa == null) { return BadRequest(); }

            patchDto.ApplyTo(villaDto, ModelState);

            if(!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);

            _dbContext.Villas.Update(modelo);
            await _dbContext.SaveChangesAsync();
                    
            return NoContent();
        }

    }
}

using MagicVilla_Api.Datos;
using MagicVilla_Api.Modelos;
using MagicVilla_Api.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//video de You tube https://www.youtube.com/watch?v=OuiExAqVapk&ab_channel=BaezStoneCreators minuto 2:37:23

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger <VillaController> _logger;

        private readonly ApplicationDBContext _dbContext;

        public VillaController(ILogger<VillaController> logger, ApplicationDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>>GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(_dbContext.Villas.ToList());
        }

        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id == 0) {
                _logger.LogError("Error al obtener los datos de la villa de ID : "+id);
                return BadRequest();
            }
            //var Villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var Villa = _dbContext.Villas.FirstOrDefault(x => x.Id == id);

            if (Villa == null) { return NotFound(); }

            return Ok(Villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody]VillaDto villaDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    
            
            if(_dbContext.Villas.FirstOrDefault(v =>v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }


            if(villaDto == null) 
            {
                return BadRequest(); 
            }
            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa modelo = new()
            {               
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            _dbContext.Villas.Add(modelo);
            _dbContext.SaveChanges();
            
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult EliminarVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = _dbContext.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null) { return NotFound(); }

            _dbContext.Villas.Remove(villa);
            _dbContext.SaveChanges();

            return NoContent();

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDto villaDto) 
        {
            if(villaDto == null || id!= villaDto.Id) 
            { 
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //villa.Nombre = villaDto.Nombre;
            //villa.Ocupantes = villaDto.Ocupantes;
            //villa.MetrosCuadrados =villaDto.MetrosCuadrados;

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            _dbContext.Villas.Update(modelo);
            _dbContext.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateParvialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _dbContext.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };

            if(villa == null) { return BadRequest(); }

            patchDto.ApplyTo(villaDto, ModelState);

            if(!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            _dbContext.Villas.Update(modelo);
            _dbContext.SaveChanges();
                    
            return NoContent();
        }

    }
}

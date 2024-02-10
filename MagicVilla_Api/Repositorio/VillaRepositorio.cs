using MagicVilla_Api.Datos;
using MagicVilla_Api.Modelos;
using MagicVilla_Api.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MagicVilla_Api.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        private readonly ApplicationDBContext _db;
        public VillaRepositorio(ApplicationDBContext db):base(db)
        {
            _db = db;           
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}

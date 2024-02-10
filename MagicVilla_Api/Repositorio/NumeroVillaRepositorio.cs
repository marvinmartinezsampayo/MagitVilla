using MagicVilla_Api.Datos;
using MagicVilla_Api.Modelos;
using MagicVilla_Api.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MagicVilla_Api.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        private readonly ApplicationDBContext _db;
        public NumeroVillaRepositorio(ApplicationDBContext db):base(db)
        {
            _db = db;           
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}

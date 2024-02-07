using MagicVilla_Api.Modelos.Dto;

namespace MagicVilla_Api.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto {Id=1,Nombre="Vista a la Piscina",Ocupantes=20,MetrosCuadrados=200},
            new VillaDto {Id=2,Nombre="Vista a la Playa",Ocupantes=4,MetrosCuadrados=80}
        };
    }
}

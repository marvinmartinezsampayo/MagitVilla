using System.Net;

namespace MagicVilla_Api.Modelos
{
    public class ApiResponse
    {
        public HttpStatusCode statusCode { get; set; }  
        public bool IsExitoso { get; set; }= true;
        public List<string> ErrorMessages { get; set; }
        public object Resultado {  get; set; }
    }
}

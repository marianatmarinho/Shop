using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    //Endpoint = URL
    // HTTPS://LOCALHOST:5001
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [Route("")]
        public string MeuMetodo()
        {
            return "Olá Mundo";
        }
    }
}
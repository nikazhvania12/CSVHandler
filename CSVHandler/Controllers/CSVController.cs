using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSVHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        [HttpGet()]
        [Route("CSVToModel")]
        public List<Book> ListBooks(string filepath)
        {
            return CSVHelper.ConvertToModel<Book>(filepath);
        }
    }
}

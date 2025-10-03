using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //POST api/users
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PutProfilePicture(IFormFile file) //para receber arquivos na api
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            //Processar a imagem, ex: salvar no banco de dados, salvar local, salvar blob storage ou s3.

            return Ok(description);
        }
    }


}

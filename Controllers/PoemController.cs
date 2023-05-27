using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blogApi.Models;
using Poem = blogApi.Models.Poem;

namespace blogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoemController : ControllerBase
    {
        public readonly WebApiContext _dbcontext;

        public PoemController(WebApiContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetPoems")]
        public IActionResult Listar()
        {

            List<Poem> listPoems= new List<Poem>();

            try
            {
                listPoems = _dbcontext.Poem.ToList();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Consulta Exitosa",
                    Detail = listPoems
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al consultar",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });

            }

        }


        [HttpGet]
        [Route("GetPoem/{id}")]
        public IActionResult GetPoem(int id)
        {
            Poem poem = new Poem();
            try
            {
                poem = _dbcontext.Poem.Find(id);
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Consulta Exitosa",
                    Detail = poem
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al consultar",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });

            }

        }

        [HttpPost]
        [Route("AddPoem")]
        public IActionResult AddPoem(Poem poem)
        {
            try
            {
                _dbcontext.Poem.Add(poem);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Poema agregado correctamente",
                    Detail = poem
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al agregar el poema",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });

            }

        }

        [HttpPut]
        [Route("UpdatePoem")]
        public IActionResult UpdatePoem(Poem poem)
        {
            try
            {
                _dbcontext.Entry(poem).State = EntityState.Modified;
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Poema actualizado correctamente",
                    Detail = poem
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al actualizar el poema",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });

            }

        }

        [HttpDelete]
        [Route("DeletePoem/{id}")]
        public IActionResult DeletePoem(int id)
        {
            Poem poem = _dbcontext.Poem.Find(id);
            if (poem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    code = "NotFound",
                    message = "Poema no encontrado"
                });
            }

            try
            {
                // Verificar si existen relaciones con otras entidades
                bool hasRelatedData = CheckRelatedData(poem);

                if (hasRelatedData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        code = "BadRequest",
                        message = "No es posible eliminar el poema debido a las relaciones existentes"
                    });
                }

                _dbcontext.Poem.Remove(poem);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Poema eliminado exitosamente",
                    Detail = poem
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "InternalServerError",
                    message = "Error al eliminar el poema",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });
            }
        }

        private bool CheckRelatedData(Poem poem)
        {
            // Realiza las verificaciones necesarias para determinar si existen relaciones con otras entidades
            // Retorna true si existen relaciones, o false si no hay relaciones

            // Ejemplo:
            bool hasRelatedData = _dbcontext.Comment.Any(e => e.IdPoem == poem.IdPoem);

            return hasRelatedData;
        }


    }
}

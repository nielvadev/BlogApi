using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blogApi.Models;
using User = blogApi.Models.User;

namespace blogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly WebApiContext _dbcontext;

        public UsersController(WebApiContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult Listar()
        {

            List<User> listUsers = new List<User>();

            try
            {
                listUsers = _dbcontext.Users.ToList();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Consulta Exitosa",
                    Detail = listUsers
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
        [Route("GetUser/{id}")]

        public IActionResult GetUser(int id)
        {
            User user = new User();
            try
            {
                user = _dbcontext.Users.Find(id);
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Consulta Exitosa",
                    Detail = user
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
        [Route("CreateUser")]
        public IActionResult CreateUser(User user)
        {
            try
            {
                _dbcontext.Users.Add(user);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Usuario creado exitosamente",
                    Detail = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al crear usuario",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });

            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                _dbcontext.Entry(user).State = EntityState.Modified;
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Usuario actualizado exitosamente",
                    Detail = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al actualizar usuario",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });

            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = _dbcontext.Users.Find(id);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    code = "NotFound",
                    message = "Usuario no encontrado"
                });
            }

            // Verificar si existen relaciones con otras entidades
            bool hasRelatedData = CheckRelatedData(user);

            if (hasRelatedData)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "BadRequest",
                    message = "No es posible eliminar el usuario debido a las relaciones existentes"
                });
            }

            try
            {
                _dbcontext.Users.Remove(user);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Usuario eliminado exitosamente",
                    Detail = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    code = "InternalServerError",
                    message = "Error al eliminar usuario",
                    Detail = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                });
            }
        }

        private bool CheckRelatedData(User user)
        {
            // Realiza las verificaciones necesarias para determinar si existen relaciones con otras entidades
            // Puedes utilizar consultas LINQ para verificar si hay datos relacionados en otras tablas
            // Retorna true si existen relaciones, o false si no hay relaciones

            // Ejemplo:
            bool hasRelatedData = _dbcontext.Poem.Any(e => e.IdUser == user.IdUser);

            return hasRelatedData;
        }


    }
}

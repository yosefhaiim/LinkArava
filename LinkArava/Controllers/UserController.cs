using LinkArava.DTO;
using LinkArava.Models;
using LinkArava.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LinkArava.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly JWTService jwtService;

        

        public UserController(UserService _UserService, JWTService _jwtService) 
        { userService = _UserService; 
            _jwtService = jwtService;
        }
        

    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<SingleUserResponseDTO>> getUser(int id)
        {
            UserModel user = await userService.getUserById(id);
            return user != null ? Ok(user) : NotFound();
        }




        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<string>> register([FromBody] UserModel user)
        {
            int userId = await userService.register(user);
            if(user != null)
            {                
                await Response.WriteAsync(userId.ToString());
                return Created(); // make sure not closing aftwe it being sent to the claient
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> login([FromBody] UserModel user)
        {
            UserModel useFromDb = await userService.getUserByPassword(user.UserName, user.password);
            if (useFromDb != null)
            {
                return Unauthorized("Invalid userName or password");
            }
            // לבדוק את זה מה בדיוק לשים
            string token = jwtService.genJWToken(useFromDb);
            return Ok(token);
        }

    }
}

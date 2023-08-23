using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserDataService.Data;
using UserDataService.Models;

namespace UserDataService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly DAL _DataService;

        public UserController(DAL data_Service) => _DataService = data_Service;


        [HttpGet]
        public async Task<List<UserData>> Get() =>await _DataService.GetAsync();

        [HttpGet("/test")]
        public async Task<IActionResult> Test()
        {
            return Ok("User Data service is up and running");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> Get(Guid id)
        {
            var Data = await _DataService.GetAsync(id);

            if (Data is null)
            {
                return NotFound();
            }

            return Data;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserData newUser)
        {
            await _DataService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.User_Id }, newUser);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Update(Guid id, UserData updatedData)
        {
            var User = await _DataService.GetAsync(id);

            if (User is null)
            {
                return NotFound();
            }

            updatedData.User_Id = User.User_Id;

            await _DataService.UpdateAsync(id, updatedData);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var User = await _DataService.GetAsync(id);

            if (User is null)
            {
                return NotFound();
            }

            await _DataService.RemoveAsync(id);

            return NoContent();
        }

    }
}

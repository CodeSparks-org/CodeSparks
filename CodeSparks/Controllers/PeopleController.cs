using CodeSparks.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeSparks.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IUserRepository _userRepository;

        public PeopleController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = await _userRepository.GetAllUsers();

            return View(allUsers);
        }
    }
}

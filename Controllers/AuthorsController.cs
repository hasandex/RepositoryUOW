using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using RepositoryUOW.Core;
using RepositoryUOW.Core.IRepositories;
using RepositoryUOW.Core.Models;

namespace RepositoryUOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseRepository<Author> _authorsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IBaseRepository<Author> authorsRepository, IUnitOfWork unitOfWork)
        {
            _authorsRepository = authorsRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        //[Route("GetAuthorById/{id:int}")]
        public IActionResult GetById(int id)
        {
            //return Ok(_authorsRepository.GetById(id));
            return Ok(_unitOfWork.Authors.GetById(id));
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _authorsRepository.GetByIdAsync(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _authorsRepository.GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            return Ok(_authorsRepository.Find(a => a.Name == name));
        }
        [HttpGet("GetAllOrdered")]
        public IActionResult GetAllOrdered(string name)
        {
            return Ok(_authorsRepository.FindAll(a=>a.Name == name, take:1,skip:1, a=>a.Id));
        }
    }
}

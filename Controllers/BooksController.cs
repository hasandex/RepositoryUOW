using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryUOW.Core;
using RepositoryUOW.Core.IRepositories;
using RepositoryUOW.Core.Models;

namespace RepositoryUOW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _booksRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IBaseRepository<Book> booksRepository, IUnitOfWork unitOfWork)
        {
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok(_booksRepository.GetById(id));
        }
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _booksRepository.GetByIdAsync(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _booksRepository.GetAll());
        }
        [HttpGet("GetByTitle")]
        public IActionResult GetByName(string title)
        {
            return Ok(_booksRepository.Find(a => a.Title == title));
        }
        [HttpGet("GetWithAuthor")]
        public IActionResult GetWithInclude(string title,[FromQuery] string[] includes = null)
        {
            return Ok(_booksRepository.Find(a=>a.Title == title, includes));
        }
     
    }
}

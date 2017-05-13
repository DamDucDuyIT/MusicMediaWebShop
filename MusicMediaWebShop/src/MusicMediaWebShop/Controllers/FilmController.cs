using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Models.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicMediaWebShop.Controllers
{
    public class FilmController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 5;

        public FilmController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Films(string TagDetail, string Tag)
        {
            return View(await _repository.GetAllFilms(TagDetail, Tag));
        }
    }
}

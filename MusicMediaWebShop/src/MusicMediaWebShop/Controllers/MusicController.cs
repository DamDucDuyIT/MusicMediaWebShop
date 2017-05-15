using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Models.Repository;
using MusicMediaWebShop.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicMediaWebShop.Controllers
{
    public class MusicController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 5;
        private SignInManager<ApplicationUser> _signInManager;

        public MusicController(IProductRepository repository, SignInManager<ApplicationUser> signInManager)
        {
            _repository = repository;
            _signInManager = signInManager;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Musics(string TagDetail, string Tag)
        {

            return View(await _repository.GetAllMusics(TagDetail, Tag));
        }

   
    }
}


using FamilyPhotos.Models;
using FamilyPhotos.Repository;
using FamilyPhotos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Controllers
{
    public class PhotoController : Controller
    {
        private PhotoRepository repository;

        public PhotoController (PhotoRepository repository)
        {
            if (repository==null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            this.repository = repository;
        }
        

        public IActionResult Index()
        {
            var pics=repository.GetAllPhotos();

            return View(pics);
        }

        public FileContentResult GetImage(int photoId)
        {
            var pic = repository.GetPicture(photoId);

            if (pic==null || pic.Picture == null)
            {
                return null;
            }
            return File(pic.Picture, "image/jpg"); //TODO: lecserélni értelmes content type-ra
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PhotoViewModel());
        }

        [HttpPost]
        //public IActionResult Create(string Title, string Description)

        public IActionResult Create(PhotoViewModel viewModel)
        {
            //azon a controller/action-on ami modelt fogad kötelező a validálás és eredményének ellenőrzése
            //méghozzá a ModellState állapotának ellenőrzése, itt jelenik meg a validálás végeredménye
            if (!ModelState.IsValid )
            {
                //A View-t fel kell készíteni a hibainformációk megjelenítésére
                return View(viewModel);
            }
            //el kel lvégeznünk a viewModel => Model transzformációt
            var model = new PhotoModel();
            
            model.Title = viewModel.Title;
            model.Description = viewModel.Description;
            model.ContentType = viewModel.PictureFormBrowser.ContentType;

            model.Picture = new byte[viewModel.PictureFormBrowser.Length];
            
            using (var stream = viewModel.PictureFormBrowser.OpenReadStream())
            {
                stream.Read(model.Picture, 0,(int) viewModel.PictureFormBrowser.Length);
            }

            //viewModel.ContentType = viewModel.PictureFormBrowser.ContentType;
            repository.AddPhoto(model);

            //a kép elmentése után térjen vissza az index oldalra
            return RedirectToAction("Index");
        }
    }
}

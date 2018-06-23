using AutoMapper;
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
        private IMapper mapper;

        public PhotoController (PhotoRepository repository, IMapper mapper)
        {
            if (repository==null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            this.repository = repository;

            if (mapper==null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
            this.mapper = mapper;
        }
        

        public IActionResult Index()
        {
            var pics=repository.GetAllPhotos();

            return View(pics);
        }

        public IActionResult Details(int id)
        {
            var model = repository.GetPicture(id);
            var viewModel = mapper.Map<PhotoViewModel>(model);

            return View(viewModel);
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
                             
            var model = mapper.Map<PhotoModel>(viewModel);

            //Több profil betöltése
            //var autoMapperCfg = new AutoMapper.MapperConfiguration(
            //        cfg => {
            //            cfg.AddProfile(new PhotoProfile());
            //            cfg.AddProfile(new PhotoProfile());
            //            cfg.AddProfile(new PhotoProfile());
            //            cfg.AddProfile(new PhotoProfile());
            //        });



            //viewModel.ContentType = viewModel.PictureFormBrowser.ContentType;
            repository.AddPhoto(model);

            //a kép elmentése után térjen vissza az index oldalra
            return RedirectToAction("Index");
        }
    }
}

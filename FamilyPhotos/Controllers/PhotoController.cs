using AutoMapper;
using FamilyPhotos.Models;
using FamilyPhotos.Repository;
using FamilyPhotos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FamilyPhotos.Filters.MyExceptionFilter;

namespace FamilyPhotos.Controllers
{

    [MyExceptionFilter2(2)]
    [MyExceptionFilter3(Order =1)]
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = repository.GetPicture(id);
            var viewModel = mapper.Map<PhotoViewModel>(model); 
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PhotoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var model = mapper.Map<PhotoModel>(viewModel);
            repository.UpdatePhoto(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = repository.GetPicture(id);
            var viewModel = mapper.Map<PhotoViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(PhotoViewModel viewModel)
        {
            repository.DeletePhoto(viewModel.id);

            return RedirectToAction("Index");
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

        public IActionResult EzEgyHibakod()
        {
            try
            {
                //innentől a UseStatusCodePages nem szereplő
                throw new Exception("Itt is van hiba");
            }
            catch (Exception)
            {
                //ha lekezeljük a hibákat, és csak a végeredményt jelezzük  
                //akkor a StatusCodePage megjelenik a felhasználónál 
                return StatusCode(500);
            }
        }
        public IActionResult EzEgyKivetel()
        {
            throw new Exception("Itt a hiba"); //ezt a Startup.cs-ben beállított exception handler segít lekezelni

            //try
            //{
            //    throw new Exception("Itt a hiba");
            //}
            //catch (Exception)
            //{
            //    return RedirectToAction("kivetel","Errors");
            //}
        }
    }
}

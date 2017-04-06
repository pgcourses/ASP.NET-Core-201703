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

        public PhotoController(PhotoRepository repository, IMapper mapper)
        {
            if (repository == null)
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
            var pics = repository.GetAllPhotos(); //TODO: itt még a model megy ki a View-ra
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
            repository.DeletePhoto(viewModel.Id);
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int photoId)
        {
            var pic = repository.GetPicture(photoId);
            if (pic==null || pic.Picture==null)
            {
                return null;
            }

            return File(pic.Picture, pic.ContentType);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PhotoViewModel());
        }

        [HttpPost]
        //public IActionResult Create(string Title, string Description)
        public IActionResult Create(PhotoViewModel viewModel) //Itt az MVC modelbindere a bejövő paramétereket egyezteti a várt osztály propertyjeivel és ki is tölti
        {
            //Azon a Controller/Action-ön, ami model-t fogad, kötelező a validálás és eredményének az ellenőrzése
            //méghozzá a ModelState állapotának ellenőrzése, itt jelenik meg a validálás végeredménye
            //+ha tudjuk, akkor ValidationAttrubute-okon keresztül ellenőrizzünk

            if (!ModelState.IsValid)
            {
                //A View-t fel kell készíteni a hibainformációk
                //megjelenítésére
                return View(viewModel);
            }

            //több profile betöltése
            //var autoMapperCfg = new AutoMapper.MapperConfiguration(
            //    cfg =>
            //    {
            //        cfg.AddProfile(new PhotoProfile());
            //        cfg.AddProfile(new PhotoProfile());
            //        cfg.AddProfile(new PhotoProfile());
            //        cfg.AddProfile(new PhotoProfile());
            //        cfg.AddProfile(new PhotoProfile());
            //    });
            

            //El kell végezni a ViewModel=>Model transzformációt
            ////////////////////////////////////////////////////
            var model = mapper.Map<PhotoModel>(viewModel);

            repository.AddPhoto(model);

            //A kép elmentése után térjen vissza az Index oldalra
            return RedirectToAction("Index");
        }
    }
}

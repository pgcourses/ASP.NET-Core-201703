using AutoMapper;
using FamilyPhotos.Models;
using FamilyPhotos.Repository;
using FamilyPhotos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Controllers
{
    [FamilyPhotos.Filters.MyExceptionFilter2(2)] //Ha a ExceptionFilterAttribute-ot származtatjuk le,
    [FamilyPhotos.Filters.MyExceptionFilter3(Order=1)] // akkor nem kell sokat implementálni
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<PhotoController> logger;

        public PhotoController(IPhotoRepository repository, IMapper mapper,
            //Ha így vesszük át a naplózó osztályt, akkor a DI 
            //kitöltö nekünk a kategóriát automatikusan
            //+az egész típusos, és nem string-et írunk
            //különben így kéne: logger.CreateLogger("FamilyPhotos.Controllers.PhotoController")
            ILogger<PhotoController> logger)
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

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            this.logger = logger;
        }

        public IActionResult Index()
        {
            //Szintek:
            //Trace=0, legbővebb, csak a fejlesztők számára, 
            //         alapértelmezésben kiszűrve
            //         és nem szabad a Production környezetben engedélyezni
            //logger.LogTrace

            //Debug =1, 
            //Information =2, 
            //Warning =3, 
            //Error =4, 

            //Critical =5 Alkalmazás leállásához vezető hibák: 
            //            elfogyott a hely, elvesztek adatok, ilyesmi.
            // 

            logger.LogTrace("Meghívták az Index actiont");



            var pics = repository.GetAllPhotos(); //TODO: itt még a model megy ki a View-ra
            return View(pics);
        }

        public IActionResult Details(int id)
        {
            logger.LogDebug("Valaki a Details-t hívta ezzel a paraméterrel: {0}", id);

            var model = repository.GetPicture(id);

            var viewModel = mapper.Map<PhotoViewModel>(model);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            logger.LogInformation("Kép módosítás kezdődik: {0}", id);

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

        //példakód a StatusCodePage megoldásokhoz
        public IActionResult EzEgyHibasKod()
        {
            try
            {
                //innentől a UseStatusCodePages nem szereplő
                throw new Exception("Itt is van a hiba");
            }
            catch (Exception)
            {
                //ha lekezeljük a hibát, és csak a végeredményt jelezzük
                //akkor a StatusCodePage megjelenik a felhasználónál
                return StatusCode(500);
            }
        }

        public IActionResult EzEgyKivetel()
        {
            throw new Exception("Itt is van a hiba"); //Ezt a Startup.cs-ben beállított ExceptionHandler segít lekezelni.

            //try
            //{
            //    throw new Exception("Itt is van a hiba");
            //}
            //catch (Exception ex)
            //{
            //    return RedirectToAction("Kivetel", "Errors");
            //}
        }

    }
}

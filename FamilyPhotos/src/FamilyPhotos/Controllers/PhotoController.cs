using FamilyPhotos.Models;
using FamilyPhotos.Repository;
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

        public PhotoController(PhotoRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var pics = repository.GetAllPhotos();
            return View(pics);
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
            return View(new PhotoModel());
        }

        [HttpPost]
        //public IActionResult Create(string Title, string Description)
        public IActionResult Create(PhotoModel model) //Itt az MVC modelbindere a bejövő paramétereket egyezteti a várt osztály propertyjeivel és ki is tölti
        {
            //nagyon kezdetleges Adatvalidálás, ezt majd jól meg fogjuk haladni!
            //hiányzik még pár dolog, csak DEMO

            if (model.PictureFromBrowser==null || model.PictureFromBrowser.Length==0)
            {
                return View(model);
            }

            //Átírni az adatokat a model.PictureFromBrowser --> model.Picture
            //Készítünk egy fogadó byte tömböt, amiben a kép elfér
            model.Picture = new byte[model.PictureFromBrowser.Length];
            
            //Megnyitjuk és átmásoljuk a feltöltött állomány stream-jét a tömbbe
            using (var stream = model.PictureFromBrowser.OpenReadStream())
            {
                //figyelem, ehelyett a cast helyett buffer + ciklus, ez csak DEMO
                stream.Read(model.Picture, 0, (int)model.PictureFromBrowser.Length);
            }

            model.ContentType = model.PictureFromBrowser.ContentType;

            repository.AddPhoto(model);
            return View(model);
        }
    }
}

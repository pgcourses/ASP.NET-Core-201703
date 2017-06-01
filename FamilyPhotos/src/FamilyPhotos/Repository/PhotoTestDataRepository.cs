using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Repository
{
    public class PhotoTestDataRepository : IPhotoRepository
    {
        //Tesztadat az első lépéshez
        //private List<PhotoModel> data = new List<PhotoModel> { new PhotoModel { Id=1, Title = "Egy kép" } };

        //Figyelem: ez csak fejlesztői teszthez jó, nem feltétlenül szálbiztos a kódunk, amit írunk
        //Thread safe: https://en.wikipedia.org/wiki/Thread_safety
        private List<PhotoModel> data = new List<PhotoModel>();
        int id = 0;

        public IEnumerable<PhotoModel> GetAllPhotos()
        {
            return data;
        }

        public PhotoModel GetPicture(int photoId)
        {
            //ha megtalálja visszaadja
            //ha nem, akkor null-t ad
            return data.SingleOrDefault(x => x.Id == photoId);
        }

        //figyelem, csak DEMO, ezt ki fogjuk alaposan egészíteni még!!!!
        public void AddPhoto(PhotoModel model)
        {
            model.Id = id++;
            data.Add(model);
        }

        public void UpdatePhoto(PhotoModel model)
        {
            var oldModel = data.SingleOrDefault(x => x.Id == model.Id);
            if (oldModel!=null)
            {
                data.Remove(oldModel);
                data.Add(model);
            }
        }

        public void DeletePhoto(int id)
        {
            var oldModel = data.SingleOrDefault(x => x.Id == id);
            if (oldModel != null)
            {
                data.Remove(oldModel);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyPhotos.Models;
using FamilyPhotos.Data;

namespace FamilyPhotos.Repository
{
    public class PhotoEfCoreDataRepository : IPhotoRepository
    {
        private FamilyPhotosContext context;

        public PhotoEfCoreDataRepository(FamilyPhotosContext context)
        {
            this.context = context;
        }

        public void AddPhoto(PhotoModel model)
        {
            context.Photos.Add(model);
            context.SaveChanges();
        }

        public void DeletePhoto(int id)
        {
            var toRemovePhoto = Find(id);
            if (toRemovePhoto==null) { return; }
            context.Photos.Remove(toRemovePhoto);
            context.SaveChanges();
        }

        public IEnumerable<PhotoModel> GetAllPhotos()
        {
            return context.Photos.ToList();
        }

        public PhotoModel GetPicture(int photoId)
        {
            return Find(photoId);
        }

        private PhotoModel Find(int photoId)
        {
            return context.Photos
                          .SingleOrDefault(x => x.Id == photoId);
        }

        public void UpdatePhoto(PhotoModel model)
        {
            ////itt be kell mutatnunk az új jövevényt az EF-nek,
            ////hiszen ez csak egy memória példány egyelőre
            var toUpdatePhoto = Find(model.Id);

            toUpdatePhoto.Title = model.Title;
            toUpdatePhoto.Description = model.Description;
            toUpdatePhoto.ContentType = model.ContentType;
            toUpdatePhoto.Picture = model.Picture;

            context.Photos.Update(toUpdatePhoto);

            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyPhotos.Models;

namespace FamilyPhotos.Repository
{
    public class PhotoEfCoreRepository : IPhotoRepository
    {
        public void AddPhoto(PhotoModel model)
        {
            throw new NotImplementedException();
        }

        public void DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhotoModel> GetAllPhotos()
        {
            throw new NotImplementedException();
        }

        public PhotoModel GetPicture(int photoId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePhoto(PhotoModel model)
        {
            throw new NotImplementedException();
        }
    }
}

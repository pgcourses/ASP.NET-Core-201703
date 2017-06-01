using FamilyPhotos.Models;
using System.Collections.Generic;

namespace FamilyPhotos.Repository
{
    public interface IPhotoRepository
    {
        IEnumerable<PhotoModel> GetAllPhotos();
        PhotoModel GetPicture(int photoId);
        void AddPhoto(PhotoModel model);
        void UpdatePhoto(PhotoModel model);
        void DeletePhoto(int id);
    }
}
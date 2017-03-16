using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Repository
{
    public class PhotoRepository
    {
        //private List<PhotoModel> data = new List<PhotoModel> { new PhotoModel { Id=1, Title = "Egy kép" } };
        private List<PhotoModel> data = new List<PhotoModel>();

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
    }
}

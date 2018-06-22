using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Repository
{
    public class PhotoRepository
    {
        //private List<PhotoModel> data = new List<PhotoModel> { new PhotoModel {id=1, Title = "Egy Kép" } };
        private List<PhotoModel> data = new List<PhotoModel>();
        int id = 0;

        public IEnumerable<PhotoModel> GetAllPhotos()
        {
            return data;
        }

        public PhotoModel GetPicture(int photoId)
        {
          return data.SingleOrDefault(x => x.id == photoId);
        }

        public void AddPhoto(PhotoModel model)
        {
            model.id = id++;
            data.Add(model);
        }
    }
}

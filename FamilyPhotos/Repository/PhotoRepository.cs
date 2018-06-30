using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Repository
{
    public class PhotoTestDataRepository : IPhotoRepository
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

        public void UpdatePhoto(PhotoModel model)
        {
            var oldModel = data.SingleOrDefault(x => x.id == model.id);
            //oldModel.Title = model.Title;  ezt kellene, de az automapper megoldja. helyett:
            if (oldModel!=null)
            {
                data.Remove(oldModel);
                data.Add(model);
            }
        }

        internal void DeletePhoto(int id)
        {
            var oldModel = data.SingleOrDefault(x => x.id == id);
            if (oldModel != null)
            {
                data.Remove(oldModel);
            }
        }

        void IPhotoRepository.DeletePhoto(int id)
        {
            throw new NotImplementedException();
        }
    }
}

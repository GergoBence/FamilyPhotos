using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyPhotos.Data;
using FamilyPhotos.Models;

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
            if (toRemovePhoto==null)
            {
                return;
            }
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
            return context.Photos.SingleOrDefault(x => x.id == photoId);
        }

        public void UpdatePhoto(PhotoModel model)
        {
            var toUpdatePhoto = Find(model.id);
            toUpdatePhoto.Title = model.Title;
            toUpdatePhoto.Description = model.Description;
            toUpdatePhoto.ContentType = model.ContentType;
            toUpdatePhoto.Picture = model.Picture;
            context.Photos.Update(toUpdatePhoto);
            context.SaveChanges();

            //context.Entry(model);
            //context.Update(model);
            //context.SaveChanges();
        }
    }
}

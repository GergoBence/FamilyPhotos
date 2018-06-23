using AutoMapper;
using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.ViewModel
{
    public class PhotoProfile :Profile
    {
        public PhotoProfile()
        {
            //PhotoViewModel-ből csinál PhotoModel-t
            CreateMap<PhotoViewModel, PhotoModel>()   //Ezzerl az azonos nevű property-kkel megvagyunk
                    .ForMember(dest=>dest.ContentType,  //Jön a ContentType
                        options=>options.MapFrom(
                            src=>src.PictureFormBrowser == null 
                            ? null : src.PictureFormBrowser.ContentType))
                            .AfterMap((viewModel,model)=> {     //Ez pedig a Picture kiolvasása
                                model.Picture = new byte[viewModel.PictureFormBrowser.Length];

                                using (var stream = viewModel.PictureFormBrowser.OpenReadStream())
                                {
                                    stream.Read(model.Picture, 0, (int)viewModel.PictureFormBrowser.Length);
                                }
                            })
                ;

            CreateMap<PhotoModel, PhotoViewModel>();

            ////el kel lvégeznünk a viewModel => Model transzformációt
            //var model = new PhotoModel();

            //model.Title = viewModel.Title;
            //model.Description = viewModel.Description;
            //model.ContentType = viewModel.PictureFormBrowser.ContentType;


        }

    }
}

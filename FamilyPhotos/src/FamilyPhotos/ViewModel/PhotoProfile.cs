using AutoMapper;
using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.ViewModel
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoViewModel, PhotoModel>() //Ezzel az azonos nevű property-kkel megvagyunk
                .ForMember(dest=>dest.ContentType,  //Jön a ContentType
                                options=>options.MapFrom(
                                    src=>src.PictureFromBrowser == null 
                                            ? null 
                                            : src.PictureFromBrowser.ContentType))
                .AfterMap((viewModel, model) => {  //ez pedig a Picture kiolvasása
                    model.Picture = new byte[viewModel.PictureFromBrowser.Length];

                    using (var stream = viewModel.PictureFromBrowser.OpenReadStream())
                    {
                        stream.Read(model.Picture, 0, (int)viewModel.PictureFromBrowser.Length);
                    }
                })
                ;

            CreateMap<PhotoModel, PhotoViewModel>()
                ;

            CreateMap<PhotoModel, PhotoModel>()
                ;

        }
    }
}

using FamilyPhotos.Models;
using FamilyPhotos.ViewModel.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.ViewModel
{
    public class PhotoViewModel
    {
        public int id { get; set; }

        /// <summary>
        /// ---Beépített validációk:
        /// 
        /// Require
        /// Compare
        /// EmailAddress
        /// Phone
        /// Range
        /// StringLength
        /// Url
        /// RegularExpression
        /// 
        /// + saját validálás lehetőség
        /// </summary>


        [Required]      //kötelező kitölteni
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [FormFileLengthValidation]
        [ContentTypeValidation]
        [Display(Name = "Picture")]
        public IFormFile PictureFormBrowser { get; set; }

    }
}

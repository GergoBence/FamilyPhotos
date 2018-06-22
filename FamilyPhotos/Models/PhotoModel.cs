using FamilyPhotos.ViewModel.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Models
{
    public class PhotoModel
    {
        public int id { get; set; }

        /// <summary>
        /// ---Beépített validációk(data Validáci) 
        /// 
        /// Require
        ///
        /// StringLength
        ///
        /// + saját validálás lehetőség
        /// </summary>


        [Required]      //kötelező kitölteni
        [StringLength(40)] 
        public string Title { get; set; }

        [Required]
        public String Description { get; set; }

        //ezt fogjuk majd adatbázisba menteni

        public byte[] Picture { get; set; }

        public string ContentType { get;  set; }
    }
}

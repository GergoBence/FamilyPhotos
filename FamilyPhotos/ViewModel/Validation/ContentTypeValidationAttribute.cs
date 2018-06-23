﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Models
{
    public class ContentTypeValidationAttribute : ValidationAttribute
    {
        public List<string> EnabledContentType { get; set; }

        public ContentTypeValidationAttribute(List<string> enabledContentType, string errorMessage)
        {
            EnabledContentType = enabledContentType;
            ErrorMessage = errorMessage;
        }

        public ContentTypeValidationAttribute()
            : this(
             new List<string>{"image/jpeg", "image/png"}
             ,"Nem megfelelő kép formátum:{0}, ezekből lehet választani {1}."
             )
        { }


        public override bool IsValid(object value)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return false;
            }
            return EnabledContentType.Contains(file.ContentType);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, string.Join(",", EnabledContentType));
        }

    }
}

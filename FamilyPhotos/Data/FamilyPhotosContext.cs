using FamilyPhotos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Data
{
    /// <summary>
    /// A perzisztens adatok elérését végző osztály. Két szerepe van:
    /// 
    /// 1. Ezen keresztül beszélgetünk a perzisztens adatokkal ( az adatbázissal) EF: EntityFramework(Core)
    /// 2. Ennek a változásával tudunk az adatbázison módosításokat végezni Ef Code first
    /// </summary>

    public class FamilyPhotosContext : DbContext
    {
        public FamilyPhotosContext(DbContextOptions<FamilyPhotosContext> options) :base(options)
        {

        }


        DbSet<PhotoModel> Photos { get; set; }


    }
}

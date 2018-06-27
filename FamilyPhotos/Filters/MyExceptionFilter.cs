using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FamilyPhotos.Filters
{
    public class MyExceptionFilter : System.Web.Mvc.IExceptionFilter
    {
        public void OnException(System.Web.Mvc.ExceptionContext context)
        {
            //ha szeretnénk akkor itt a kivételeket el tudjuk menteni
            //context.ExceptionHandled = true;
        }

        public class MyExceptionFilter2 : ExceptionFilterAttribute
        {
            //filter sorrend szabályozás
            public MyExceptionFilter2(int Order)
            {
                base.Order = Order;
            }

            public override void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
            {
                base.OnException(context);
            }
        }
        public class MyExceptionFilter3 : ExceptionFilterAttribute
        {
            //az Order így is implementálva van csak máshogy kell meghívni
        }
    }
}

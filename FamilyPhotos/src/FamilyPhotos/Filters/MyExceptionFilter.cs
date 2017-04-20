using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Filters
{
    //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters
    public class MyExceptionFilter : IExceptionFilter //Ilyenkor nem tudjuk attributumként használni
    {
        public void OnException(ExceptionContext context)
        {
            //Ha szeretnénk, akkor itt a kivételeket el tudjuk menteni

            //ezzel pedig a teljes hibakezelést megállítjuk itt
            //context.ExceptionHandled = true;
        }
    }

    public class MyExceptionFilter2 : ExceptionFilterAttribute
    {
        //Ezzel meghatározhatjuk a sorrendjét a filter végrehajtásnak
        public MyExceptionFilter2(int Order) 
        {
            base.Order = Order;
        }
        public override void OnException(ExceptionContext context)
        {
            //elmentjük a részleteket

            base.OnException(context);
        }
    }

    public class MyExceptionFilter3 : ExceptionFilterAttribute
    { //Az Order így is implementálva van, csak máshogy kell hívni
    }

}

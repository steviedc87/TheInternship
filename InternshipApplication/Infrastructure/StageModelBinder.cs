﻿using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using InternshipApplication.Models.DAL;
using InternshipApplication.Models.Domain;

namespace InternshipApplication.Infrastructure
{
    public class StageModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                IStagebegeleiderRepository repos =
                    (IStagebegeleiderRepository)
                        DependencyResolver.Current.GetService(typeof (IStagebegeleiderRepository));
                return repos.FindByEmail(controllerContext.HttpContext.User.Identity.Name);
            }
            return null;
        }
    }
}
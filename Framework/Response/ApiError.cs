﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace DotNetCoreWebApi.Framework.Response
{
    public class ApiError
    {
        public ApiError()
        {

        }

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            Message = "Invalid parameters.";
            Detail = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
        }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Filters
{
    /// <summary>
    /// Attribut som söker efter apinyckel header och validerar mot nyckel i appsettings
    /// </summary>

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] // Specificerar var attributet används

    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string API_KEY_HEADER_NAME = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");

            if (!apiKey.Equals(potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next(); // Kör nästa middleware
        }
    }
}

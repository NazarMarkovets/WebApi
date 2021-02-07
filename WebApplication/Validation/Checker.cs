using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Validation
{
    public class Checker
    {
        public HttpResponseMessage IsValid(object value)
        {
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound); 
            }
            

            if(value is Comment comment &&
                   (comment.Content.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                    || comment.Content.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                    || comment.Content.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase)))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}
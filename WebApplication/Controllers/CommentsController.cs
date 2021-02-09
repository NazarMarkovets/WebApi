using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Renci.SshNet.Messages.Authentication;
using WebApplication.Factories;
using WebApplication.Models;
using WebApplication.Repository;
using WebApplication.Validation;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]/")]
    public class CommentsController : ControllerBase
    {
        
        private static CommentsRepository _commentsRepository = new CommentsRepository();
        
        
        
        [HttpGet("{article_id}")] //api/Comments/{id}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Comment>> GetCommentsById(int article_id)
        {
            try
            {
                var commentsList = new List<Comment>();
                commentsList = _commentsRepository.FindCommentsByArticleId(article_id);
                return Ok(commentsList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            finally
            {
                NotFound();
            }
            
        }
        

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostWriteComment(Comment comment)
        {
            Checker requestvalidator = new Checker();
            var responseMessage = requestvalidator.IsValid(comment);
            if (responseMessage.IsSuccessStatusCode)
            {
                _commentsRepository.InsertComment(comment);
                return StatusCode(201);
            }
            else
            {
                return BadRequest("Can not execute insert statement. Request validator rejected the request");
            }
        }

         
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteComment(int id)
        {

            try
            {
                _commentsRepository.DeleteComment(id);
                return NoContent();
            }
            catch
            {
                return BadRequest("Identifier is null, or has zero");
            }
            
        }

    }
}
﻿using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Abstract;
using MultiShop.Comment.Dtos.UserCommentDtos;
using MultiShop.Comment.Entities;
using System.Net;

namespace MultiShop.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IUserCommentService _service;

        public CommentsController(IUserCommentService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommentsByProductId([FromQuery] string productId, bool isApproved, int? rating)
        {
            List<UserComment> response = await _service.GetCommentsByProductIdAsync(productId, isApproved, rating);
            if (!response.Any())
            {
                return NoContent();
            }
            //List<UserCommentDto> comments = response.Select(comment => new UserCommentDto
            //{
            //    Id = comment.Id,
            //    Name = comment.Name,
            //    ImageUrl = comment.ImageUrl,
            //    Content = comment.Content,
            //    Email = comment.Email,
            //    CreatedDate = comment.CreatedDate,
            //    UpdatedDate = comment.UpdatedDate,
            //    IsApproved = comment.IsApproved,
            //    ProductId = comment.ProductId,
            //    Rating = comment.Rating
            //}).ToList();

            // Implicit operatör sayesinde dönüştürme işlemi otomatik yapılıyor..
            List<UserCommentDto> comments = response.Select(comment => (UserCommentDto)comment).ToList();

            return Ok(comments);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommentsByProductIdPageAndSize([FromQuery] string productId, bool isApproved, int page = 1, int size = 10)
        {
            List<UserComment> response = await _service.GetCommentsByProductIdAsync(productId, isApproved, page, size);
            if (!response.Any())
            {
                return NoContent();
            }

            // Implicit operatör sayesinde dönüştürme işlemi otomatik yapılıyor..
            List<UserCommentDto> comments = response.Select(comment => (UserCommentDto)comment).ToList();

            return Ok(comments);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = _service.GetAll();
            if (!comments.Any())
            {
                return NoContent();
            }
            return Ok(comments.Select(comment => (UserCommentDto) comment).ToList());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            UserComment comment = await _service.GetByIdAsync(id);
            if (comment is null)
            {
                return NoContent();
            }
            UserCommentDto dto = comment;
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateUserCommentDto dto)
        {
            UserComment userComment = dto;
            await _service.AddAsync(userComment);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete]
        //public IActionResult RemoveComment(UserCommentDto dto)
        public IActionResult RemoveComment(int id)
        {
            //UserComment userComment = dto;
            //_service.Remove(userComment);

            _service.Remove(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateComment(UpdateUserCommentDto dto)
        {
            UserComment userComment = dto;
            _service.Update(userComment);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> MarkAsApproved(int id)
        {
            _service.MarkAsApproved(id);
            return Ok();
        }
    }
}

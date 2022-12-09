﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfUploader.Contracts.V1;
using PdfUploader.Models;
using PdfUploader.Services;
using PdfUploader.Services.Interfaces;

namespace PdfUploader.Controllers.V1
{
    [Route(ApiRoutes.Categories.GetAll)]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        /// <summary>
        /// Get a list of all files categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var list = await _categoriesService.GetAll();
            return Ok(list);
        }

        /// <summary>
        /// Get a category item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            if (id == 0) return BadRequest();
            var category = await _categoriesService.GetById(id);
            if (category == null) return NotFound();
            return category;
        }
    }
}

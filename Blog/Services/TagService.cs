﻿using Blog.Models;
using Blog.Models.DTO;
using Blog.Models.Entities;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Blog.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;
        public TagService (ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public  List<TagDto> GetTegs()
        {
            var listGenreDtos = new List<TagDto>();
            var tagEntity = _context.Tags;
            foreach (var genreEntity in tagEntity)
            {
                var tagDto = new TagDto
                {
                    Id = genreEntity.Id,
                    name = genreEntity.Name
                };
                listGenreDtos.Add(tagDto);
            }

            return  listGenreDtos;
        }
    }
}
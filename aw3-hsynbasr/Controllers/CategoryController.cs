﻿using AutoMapper;
using Data.Context;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using SimApi.Schema;

namespace SimApi.Service.Controllers;

[Route("aw3/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private AppDbContext context;
    private IMapper mapper;
    public CategoryController(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }


    [HttpGet]
    public List<CategoryResponse> GetAll()
    {
        var list = context.Set<Category>().ToList();
        var mapped = mapper.Map<List<CategoryResponse>>(list);
        return mapped;
    }

    [HttpGet("{id}")]
    public CategoryResponse GetById(int id)
    {
        var row = context.Set<Category>().FirstOrDefault(x => x.Id == id);
        var mapped = mapper.Map<CategoryResponse>(row);
        return mapped;
    }


    [HttpPost]
    public CategoryResponse Post([FromBody] CategoryRequest request)
    {
        var entity = mapper.Map<Category>(request); 
        context.Set<Category>().Add(entity);
        context.SaveChanges();

        var mapped = mapper.Map<Category, CategoryResponse>(entity);
        return mapped;
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] CategoryRequest request)
    {
        request.Id = id;
        var entity = mapper.Map<Category>(request);
        context.Set<Category>().Update(entity);
        context.SaveChanges();
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var row = context.Set<Category>().Where(x => x.Id == id).FirstOrDefault();
        context.Set<Category>().Remove(row);
        context.SaveChanges();
    }

}

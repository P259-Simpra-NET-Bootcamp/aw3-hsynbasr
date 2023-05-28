using AutoMapper;
using Data.Context;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using SimApi.Schema;

namespace SimApi.Service.Controllers;

[Route("aw3/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private AppDbContext context;
    private IMapper mapper;
    public ProductController(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }


[HttpGet]
    public List<ProductResponse> GetAll()
    {
        var list = context.Set<Product>().ToList();
        var mapped = mapper.Map<List<ProductResponse>>(list);
        return mapped;
    }

    [HttpGet("{id}")]
    public ProductResponse GetById(int id)
    {
        var row = context.Set<Product>().FirstOrDefault(x => x.Id == id);
        var mapped = mapper.Map<ProductResponse>(row);
        return mapped;
    }
    [HttpGet("Product/{CategoryId}")]
    public List<ProductResponse> GetBtCategoryId(int CategoryId)
    {
        var list = context.Set<Product>().Where(x => x.CategoryId == CategoryId).ToList();
        var mapped = mapper.Map<List<ProductResponse>>(list);
        return mapped;
    }

    [HttpPost]
    public ProductResponse Post([FromBody] ProductRequest request)
    {
        var entity = mapper.Map<Product>(request);
        context.Set<Product>().Add(entity);
        context.SaveChanges();

        var mapped = mapper.Map<Product, ProductResponse>(entity);
        return mapped;
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] ProductResponse request)
    {
        request.Id = id;
        var entity = mapper.Map<Product>(request);
        context.Set<Product>().Update(entity);
        context.SaveChanges();
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        if (id!=null)
        {
            var row = context.Set<Product>().Where(x => x.Id == id).FirstOrDefault();
            context.Set<Product>().Remove(row);
            context.SaveChanges();
        }
      
    }

}

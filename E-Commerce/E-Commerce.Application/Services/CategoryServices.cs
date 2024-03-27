using E_Commerce.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using E_Commerce.Domain.DTOs.productDto;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Application.Services
{
    public class CategoryServices : icategoryServices
    {
        private readonly IUnitOfWork _unit;
        private readonly iproductService _productService;

        private readonly IMapper _mapper;
        public CategoryServices(IUnitOfWork _unit, IMapper mapper)
        {
            this._unit = _unit;
            _mapper = mapper;
        }

        public async Task<resultDto<CreateOrUpdateCategoryDto>> createAsync(CreateOrUpdateCategoryDto category)
        {
            bool ok = (await _unit.category.SeaechByName(category.NameEn));
            if (ok)
            {
                return new resultDto<CreateOrUpdateCategoryDto> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                category.id = Guid.NewGuid();
                category.CreatedAt = DateTime.Now;
                var Cat = _mapper.Map<Category>(category);
                var Newcategory = await _unit.category.CreateAsync(Cat);
                await _unit.category.SaveChangesAsync();
                var CatDto = _mapper.Map<CreateOrUpdateCategoryDto>(Newcategory);
                return new resultDto<CreateOrUpdateCategoryDto> { Entity = CatDto, IsSuccess = true, Message = "Created Successfully" };
            }
        }

        public async Task<List<getDto>> getAll()
        {
            var q = await _unit.category.GetAllAsync();

            // q.Include("Subcategories");
            List<getDto> categorys = new List<getDto>();
            categorys = _mapper.Map<List<getDto>>(q).ToList();
            for (int i = 0; i < categorys.Count; i++)
            {
                categorys[i].children = (await GetAllChildrenByCategoryId(categorys[i].Id));
            }
            return categorys;

        }
        public async Task<List<getDto>> getAll2()
        {
            var q = await _unit.category.GetAllAsync();
            // q.Include("Subcategories");
            q = q.Where(e => e.ParentCategoryId == null);
            List<getDto> categorys = new List<getDto>();
            categorys = _mapper.Map<List<getDto>>(q).ToList();
            for (int i = 0; i < categorys.Count; i++)
            {
                categorys[i].children = (await GetAllChildrenByCategoryId(categorys[i].Id));

                if (categorys[i].children != null)
                    for (int j = 0; j < categorys[i].children.Count; ++j)
                    {
                        categorys[i].children[j].children = (await GetAllChildrenByCategoryId(categorys[i].children[j].Id));

                    }
            }
            return categorys;

        }
        public async Task<List<getProductwithImage>> getAllProductsByCategoryId(Guid id)
        {
            var prod = await _unit.product.GetAllAsync();
            List<Product> products = new List<Product>();
            List<getProductwithImage> productsToReturn = new List<getProductwithImage>();

            var hasChild = await GetAllChildrenByCategoryId(id);

            if (hasChild.Count > 0)
            {
                foreach (var category in hasChild)
                {
                    var hasSubChild = await GetAllChildrenByCategoryId(category.Id);
                    if (hasSubChild.Count >0 )
                    {
                        foreach (var subcategory in hasSubChild)
                        {
                            products = await prod.Where(p => p.categoryId == subcategory.Id).Include(e => e.Images).ToListAsync();
                            var productsDto2 = _mapper.Map<List<getProductwithImage>>(products);
                            productsToReturn.AddRange(productsDto2);

                        }
                    }
                    products = await prod.Where(p => p.categoryId == category.Id).Include(e => e.Images).ToListAsync();
                    var productsDto = _mapper.Map<List<getProductwithImage>>(products);

                    productsToReturn.AddRange(productsDto);

                }
            }
            else
            {
                products = await prod.Where(p => p.categoryId == id).Include(e => e.Images).ToListAsync();
                productsToReturn = _mapper.Map<List<getProductwithImage>>(products);
            }
            return productsToReturn;

        }
        public async Task<List<getCategorywithProducts>> getAllCattegoriesWtihProducts()
        {

            var q = await _unit.category.GetAllAsync();
            var prod = await _unit.product.GetAllAsync();
            List<getCategorywithProducts> resultedcategoryproducts = new List<getCategorywithProducts>();
            foreach (var category in await q.ToListAsync())
            {

                List<Product> products = await prod.Where(p => p.categoryId == category.Id).Include(e => e.Images).Take(10).ToListAsync();
                getCategorywithProducts categoryproducts = new getCategorywithProducts()
                {
                    nameEn = category.nameEn,
                    nameAr = category.nameAr,
                    Products = _mapper.Map<List<getProductwithImage>>(products)
                };
                resultedcategoryproducts.Add(categoryproducts);
            }
            return resultedcategoryproducts;
        }

        public async Task<resultDto<getDto>> getById(Guid ID)
        {
            try
            {
                var category = await _unit.category.GetByIdAsync(ID);
                var Returnc = _mapper.Map<getDto>(category);


                return new resultDto<getDto> { Entity = Returnc, IsSuccess = true, Message = "there is exist" };
            }
            catch (Exception ex)
            {
                return new resultDto<getDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }

        }

        public async Task<List<getDto>> GetAllChildrenByCategoryId(Guid categoryId)
        {
            var q = await _unit.category.GetAllChildrenById(categoryId);

            List<getDto> categorys = new List<getDto>();
            categorys = _mapper.Map<List<getDto>>(q).ToList();
            return categorys;

        }
        public async Task<resultDto<ReadCategoryDto>> HardDeleteAsync(Guid category)
        {
            bool ok = true;
            ok = !(await _unit.category.CheckHasChildren(category));
            var categ = await _unit.category.GetByIdAsync(category);
            if (categ == null) ok = false;
            if (!ok)
            {
                return new resultDto<ReadCategoryDto>() { Entity = null, IsSuccess = false, Message = "Not Deleted" };
            }

            await _unit.category.HardDeleteAsync(categ);
            categ.deletedAt = DateTime.Now;
            await _unit.category.SaveChangesAsync();
            var Returnc = _mapper.Map<ReadCategoryDto>(categ);
            return new resultDto<ReadCategoryDto>() { Entity = Returnc, IsSuccess = true, Message = "Deleted Successfully" };


        }

        public async Task<resultDto<ReadCategoryDto>> softDeleteAsync(Guid category)
        {
            bool ok = true;
            ok = !(await _unit.category.CheckHasChildren(category));
            var categ = await _unit.category.GetByIdAsync(category);
            if (categ == null) ok = false;
            if (!ok)
            {
                return new resultDto<ReadCategoryDto>() { Entity = null, IsSuccess = false, Message = "Not Deleted" };
            }
            categ.IsDeleted = true;
            categ.deletedAt = DateTime.Now;
            await _unit.category.SaveChangesAsync();
            var Returnc = _mapper.Map<ReadCategoryDto>(categ);
            return new resultDto<ReadCategoryDto>() { Entity = Returnc, IsSuccess = true, Message = "Deleted Successfully" };

        }

        public async Task<resultDto<CreateOrUpdateCategoryDto>> updateAsync(CreateOrUpdateCategoryDto category)
        {
            var category2 = _mapper.Map<Category>(category);
            category2 = await _unit.category.UpdateAsync(category2);
            category2.updatedAt = DateTime.Now;
            await _unit.category.SaveChangesAsync();
            CreateOrUpdateCategoryDto categoryDto = _mapper.Map<CreateOrUpdateCategoryDto>(category2);
            return new resultDto<CreateOrUpdateCategoryDto>() { Entity = categoryDto, IsSuccess = true, Message = "Created Sucessfully" };

        }


    }
}

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
  
        private readonly IMapper _mapper;
        public CategoryServices(IUnitOfWork _unit, IMapper mapper)
        {
           this._unit = _unit;
            _mapper = mapper;
        }

        public async Task<resultDto<CreateOrUpdateCategoryDto>> createAsync(CreateOrUpdateCategoryDto category)
        {
            bool ok =( await _unit.category.SeaechByName(category.NameEn));
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
          for (int i=0;i< categorys.Count;i++)
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
            ok =! (await _unit.category.CheckHasChildren(category));
            var categ = await _unit.category.GetByIdAsync(category);
            if (categ == null) ok = false;
            if(!ok)
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

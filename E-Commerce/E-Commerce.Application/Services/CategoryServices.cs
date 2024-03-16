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
namespace E_Commerce.Application.Services
{
    public class CategoryServices : icategoryServices
    {

        private readonly icategoryRepository categoryRepository;
        private readonly IMapper _mapper;
        public CategoryServices(icategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<resultDto<CreateOrUpdateCategoryDto>> createAsync(CreateOrUpdateCategoryDto category)
        {
            bool ok =( await categoryRepository.SeaechByName(category.NameEn));
            if (ok)
            {
                return new resultDto<CreateOrUpdateCategoryDto> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                category.id = Guid.NewGuid();
                var Cat = _mapper.Map<Category>(category);
                var Newbook = await categoryRepository.CreateAsync(Cat);
                await categoryRepository.SaveChangesAsync();
                var CatDto = _mapper.Map<CreateOrUpdateCategoryDto>(Newbook);
                return new resultDto<CreateOrUpdateCategoryDto> { Entity = CatDto, IsSuccess = true, Message = "Created Successfully" };
            }


        }

        public Task<resultDto<ReadCategoryDto>> getAll()
        {
            throw new NotImplementedException();
        }

        public async Task<resultDto<getDto>> getById(Guid ID)
        {
            try
            {
                var category = await categoryRepository.GetByIdAsync(ID);
                var Returnc = _mapper.Map<getDto>(category);
             
        
                return new resultDto<getDto> { Entity = Returnc, IsSuccess = true, Message = "there is exist" };
            }
            catch (Exception ex)
            {
                return new resultDto<getDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
           
        }

        public async Task<List<Category>> GetAllChildrenByCategoryId(Guid categoryId)
        {
            return (List<Category>)await categoryRepository.GetAllChildrenById(categoryId);
        }
        public Task<resultDto<ReadCategoryDto>> hardDeleteAsync(ReadCategoryDto category)
        {
            throw new NotImplementedException();
        }

        public Task<resultDto<ReadCategoryDto>> softDeleteAsync(ReadCategoryDto category)
        {
            throw new NotImplementedException();
        }

        public Task<resultDto<CreateOrUpdateCategoryDto>> updateAsync(CreateOrUpdateCategoryDto category)
        {
            throw new NotImplementedException();
        }

       
    }
}

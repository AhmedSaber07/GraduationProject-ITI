using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.BrandDto;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class brandService : ibrandService
    {
        private readonly ibrandRepository brandRepository;
        private readonly IMapper _mapper;

        public brandService(ibrandRepository brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<resultDto<CreateDto>> createAsync(CreateDto brand)
        {
            bool ok = (await brandRepository.SeaechByName(brand.nameEn));
            if (ok)
            {
                return new resultDto<CreateDto> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                brand.id = Guid.NewGuid();
                var brMapper = _mapper.Map<Brand>(brand);
                var Newbrand = await brandRepository.CreateAsync(brMapper);
                await brandRepository.SaveChangesAsync();
                var brandDto = _mapper.Map<CreateDto>(Newbrand);
                return new resultDto<CreateDto> { Entity = brandDto, IsSuccess = true, Message = "Created Successfully" };
            }
        }

        public async Task<listResultDto<GetBrandDto>> getAll()
        {
            var q = (await brandRepository.GetAllAsync());
            listResultDto<GetBrandDto> brands = new listResultDto<GetBrandDto>();
            brands.entities = _mapper.Map<IEnumerable<GetBrandDto>>(q);
            brands.count = q.Count();
            return brands;
        }

        public Task<resultDto<GetBrandDto>> getById(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Task<resultDto<GetBrandDto>> softDeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

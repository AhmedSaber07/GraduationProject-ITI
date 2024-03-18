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
        iproductService productService;
        public brandService(ibrandRepository brandRepository, IMapper mapper , iproductService iproduct)
        {
            this.brandRepository = brandRepository;
            _mapper = mapper;
            productService = iproduct;
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

        public async Task<resultDto<GetBrandDto>> getById(Guid ID)
        {
            try
            {
                var brand = await brandRepository.GetByIdAsync(ID);
                var Returnc = _mapper.Map<GetBrandDto>(brand);


                return new resultDto<GetBrandDto> { Entity = Returnc, IsSuccess = true, Message = "there is exist" };
            }
            catch (Exception ex)
            {
                return new resultDto<GetBrandDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }

        public async Task<resultDto<GetBrandDto>> softDeleteAsync(Guid id)
        {

            bool ok = true;
            var x=await productService.getbybrandAsync(id);
            ok = x.count == 0 ? true : false;
            var brand = await brandRepository.GetByIdAsync(id);
            if (brand == null) ok = false;
            if (!ok)
            {
                return new resultDto<GetBrandDto>() { Entity = null, IsSuccess = false, Message = "Not Deleted" };
            }

            brand.IsDeleted = true;
            brand.deletedAt = DateTime.Now;
            await brandRepository.SaveChangesAsync();
            var Returnc = _mapper.Map<GetBrandDto>(brand);
            return new resultDto<GetBrandDto>() { Entity = Returnc, IsSuccess = true, Message = "Deleted Successfully" };


        }
        public async Task<resultDto<GetBrandDto>> HardDeleteAsync(Guid id)
        {

            bool ok = true;
            var x = await productService.getbybrandAsync(id);
            ok = x.count == 0 ? true : false;
            var brand = await brandRepository.GetByIdAsync(id);
            if (brand == null) ok = false;
            if (!ok)
            {
                return new resultDto<GetBrandDto>() { Entity = null, IsSuccess = false, Message = "Not Deleted" };
            }

            await brandRepository.HardDeleteAsync(brand);
            brand.deletedAt = DateTime.Now;
            await brandRepository.SaveChangesAsync();
            var Returnc = _mapper.Map<GetBrandDto>(brand);
            return new resultDto<GetBrandDto>() { Entity = Returnc, IsSuccess = true, Message = "Deleted Successfully" };


        }
    }
}

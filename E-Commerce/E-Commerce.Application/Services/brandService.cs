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
     
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
       
        public brandService(IUnitOfWork _unit, IMapper mapper )
        {
            this._unit = _unit;
            _mapper = mapper;
          
        }

        public async Task<resultDto<CreateDto>> createAsync(CreateDto brand)
        {
            bool ok = (await _unit.brand.SeaechByName(brand.nameEn));
            if (ok)
            {
                return new resultDto<CreateDto> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                brand.id = Guid.NewGuid();
                var brMapper = _mapper.Map<Brand>(brand);
                var Newbrand = await _unit.brand.CreateAsync(brMapper);
                await _unit.brand.SaveChangesAsync();
                var brandDto = _mapper.Map<CreateDto>(Newbrand);
                return new resultDto<CreateDto> { Entity = brandDto, IsSuccess = true, Message = "Created Successfully" };
            }
        }

        public async Task<listResultDto<GetBrandDto>> getAll()
        {
            var q = (await _unit.brand.GetAllAsync());
            listResultDto<GetBrandDto> brands = new listResultDto<GetBrandDto>();
            brands.entities = _mapper.Map<IEnumerable<GetBrandDto>>(q);
            brands.count = q.Count();
            return brands;
        }

        public async Task<resultDto<GetBrandDto>> getById(Guid ID)
        {
            try
            {
                var brand = await _unit.brand.GetByIdAsync(ID);
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
            var x = await _unit.product.GetAllAsync();
            x=x.Where(p => p.brandId == id);
            ok = x.Count() == 0 ? true : false;
            var brand = await _unit.brand.GetByIdAsync(id);
            if (brand == null) ok = false;
            if (!ok)
            {
                return new resultDto<GetBrandDto>() { Entity = null, IsSuccess = false, Message = "Not Deleted" };
            }

            brand.IsDeleted = true;
            brand.deletedAt = DateTime.Now;
            await _unit.brand.SaveChangesAsync();
            var Returnc = _mapper.Map<GetBrandDto>(brand);
            return new resultDto<GetBrandDto>() { Entity = Returnc, IsSuccess = true, Message = "Deleted Successfully" };


        }
        public async Task<resultDto<GetBrandDto>> HardDeleteAsync(Guid id)
        {

            bool ok = true;
            var x = await _unit.product.GetAllAsync();
            x = x.Where(p => p.brandId == id);
            ok = x.Count() == 0 ? true : false;
            var brand = await _unit.brand.GetByIdAsync(id);
            if (brand == null) ok = false;
            if (!ok)
            {
                return new resultDto<GetBrandDto>() { Entity = null, IsSuccess = false, Message = "Not Deleted" };
            }

            await _unit.brand.HardDeleteAsync(brand);
            brand.deletedAt = DateTime.Now;
            await _unit.brand.SaveChangesAsync();
            var Returnc = _mapper.Map<GetBrandDto>(brand);
            return new resultDto<GetBrandDto>() { Entity = Returnc, IsSuccess = true, Message = "Deleted Successfully" };


        }
    }
}

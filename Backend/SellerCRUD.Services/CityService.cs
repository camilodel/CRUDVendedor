using AutoMapper;
using SellerCRUD.Domain.Interfaces;
using SellerCRUD.Services.Common;
using SellerCRUD.Services.DTOS;
using SellerCRUD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellerCRUD.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public CityService(IMapper mapper, IUnitOfWork unitOfWork,ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerManager;
        }

        public async Task<ServiceResponse<List<CityDto>>> GetCitiesAsync()
        {
            try
            {
                var cities = await _unitOfWork.CityRepository.GetAllAsync();

                var citiesDto = _mapper.Map<List<CityDto>>(cities);

                return new ServiceResponse<List<CityDto>>(citiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CityService::GetCitiesAsync:: {ex.Message} ");
                throw;
            }
        }

        public async Task<ServiceResponse<CityDto>> CreateCityAsync(CreateCityDto createCity)
        {
            try
            {
                Domain.Entities.City cityEntity = _mapper.Map<Domain.Entities.City>(createCity);

                if (cityEntity.Description == null)
                    throw new ArgumentException("City Name, City identification Number or a required property is null");

                cityEntity.CreateDate = DateTime.Now;

                _unitOfWork.BeginTransaction();
                _unitOfWork.CityRepository.Create(cityEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<CityDto>(_mapper.Map<CityDto>(cityEntity))
                { Message = "Datos insertados correctamente" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CityService::CreateCityAsync:: {ex.Message} ");
                throw;
            }
        }

        public async Task<ServiceResponse<CityDto>> UpdateCityAsync(int id, CityDto city)
        {
            try
            {
                if (city == null || city.Description == null)
                    throw new ArgumentException("City or a required property is null");

                var cityEntity = await _unitOfWork.CityRepository.GetByIdAsync(id);

                if (cityEntity == null)
                    throw new ArgumentException($"City with id: {city.Id} doesnt exist on DB.");

                cityEntity.Description = city.Description;

                cityEntity.UpdateDate = DateTime.Now;

                _unitOfWork.BeginTransaction();
                _unitOfWork.CityRepository.Update(cityEntity);
                await _unitOfWork.CommitAsync();
                return new ServiceResponse<CityDto>(_mapper.Map<CityDto>(cityEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CityService::UpdateCityAsync:: {ex.Message}");
                return new ServiceResponse<CityDto>($"Error in CityService::UpdateCityAsync:: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<List<CityDto>>> DeleteCitiesAsync(IEnumerable<int> ids)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                List<CityDto> citiesDeleted = new List<CityDto>();
                foreach (var thirdPartyId in ids)
                {
                    var city = _unitOfWork.CityRepository.GetSingle(thirdPartyId);

                    if (city != null)
                    {
                        _unitOfWork.CityRepository.Delete(city);
                        citiesDeleted.Add(_mapper.Map<CityDto>(city));
                    }
                }

                await _unitOfWork.CommitAsync();
                return new ServiceResponse<List<CityDto>>(citiesDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en CityService::DeleteCitiesAsync:: {ex.Message}");
                return new ServiceResponse<List<CityDto>>($"Error en CityService::DeleteCitiesAsync:: {ex.Message}");
            }
        }
    }
}

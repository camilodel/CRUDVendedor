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
    public class SellerService : ISellerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public SellerService(IMapper mapper, IUnitOfWork unitOfWork,ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerManager;
        }

        public async Task<ServiceResponse<List<SellerDto>>> GetSellersAsync()
        {
            try
            {
                var sellers = await _unitOfWork.SellerRepository.GetAllAsync();

                var sellersDto = _mapper.Map<List<SellerDto>>(sellers);

                return new ServiceResponse<List<SellerDto>>(sellersDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SellerService::GetSellersAsync:: {ex.Message} ");
                throw;
            }
        }

        public async Task<ServiceResponse<SellerDto>> CreateSellerAsync(CreateSellerDto createSeller)
        {
            try
            {
                Domain.Entities.Seller sellerEntity = _mapper.Map<CreateSellerDto, Domain.Entities.Seller>(createSeller);

                if (sellerEntity.Name == null || sellerEntity.IdentificationNumber <= 0)
                    throw new ArgumentException("Seller Name, Seller identification Nummber or a required property is null");

                sellerEntity.CreateDate = DateTime.Now;

                _unitOfWork.BeginTransaction();
                _unitOfWork.SellerRepository.Create(sellerEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<SellerDto>(_mapper.Map<SellerDto>(sellerEntity))
                { Message = "Datos insertados correctamente" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CustomerService::CreateCustomersAsync:: {ex.Message} ");
                throw;
            }
        }

        public async Task<ServiceResponse<SellerDto>> UpdateSellerAsync(int id, SellerDto seller)
        {
            try
            {
                if (seller == null || seller.Name == null)
                    throw new ArgumentException("Seller or a required property is null");

                var sellerEntity = await _unitOfWork.SellerRepository.GetByIdAsync(id);

                if (sellerEntity == null)
                    throw new ArgumentException($"Customer with id: {seller.Id} doesnt exist on DB.");

                sellerEntity.Name = seller.Name;
                sellerEntity.LastName = seller.LastName;
                sellerEntity.IdentificationNumber = seller.IdentificationNumber;

                _unitOfWork.BeginTransaction();
                _unitOfWork.SellerRepository.Update(sellerEntity);
                await _unitOfWork.CommitAsync();
                return new ServiceResponse<SellerDto>(_mapper.Map<SellerDto>(sellerEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SellerService::UpdateSellerAsync:: {ex.Message}");
                return new ServiceResponse<SellerDto>($"Error in SellerService::UpdateSellerAsync:: {ex.Message}");
            }
        }
    }
}

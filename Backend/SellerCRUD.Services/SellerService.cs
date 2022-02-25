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

        /// <summary>
        /// Inyección de dependencias
        /// </summary>
        /// <param name="mapper">automapper</param>
        /// <param name="unitOfWork">unidad de trabajo de los repositorios</param>
        /// <param name="loggerManager">logger</param>
        public SellerService(IMapper mapper, IUnitOfWork unitOfWork,ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerManager;
        }
        /// <summary>
        /// Método para obtener todos los Vendedores
        /// </summary>
        /// <returns>Todos los vendedores creados en la BD</returns>
        public async Task<ServiceResponse<List<SellerDto>>> GetSellersAsync()
        {
            try
            {
                var sellers = await _unitOfWork.SellerRepository.GetSellersAsync();

                var sellersDto = _mapper.Map<List<SellerDto>>(sellers);

                return new ServiceResponse<List<SellerDto>>(sellersDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SellerService::GetSellersAsync:: {ex.Message} ");
                throw;
            }
        }
        /// <summary>
        /// Método para crear Vendedores
        /// </summary>
        /// <param name="createSeller">Objeto de la clase DTO de creación de vendedores</param>
        /// <returns>Creación de datos en la BD</returns>
        public async Task<ServiceResponse<SellerDto>> CreateSellerAsync(CreateSellerDto createSeller)
        {
            try
            {
                Domain.Entities.Seller sellerEntity = _mapper.Map<Domain.Entities.Seller>(createSeller);

                if (sellerEntity.Name == null || sellerEntity.IdentificationNumber <= 0)
                    throw new ArgumentException("Seller Name, Seller identification Number or a required property is null");

                sellerEntity.CreateDate = DateTime.Now;

                _unitOfWork.BeginTransaction();
                _unitOfWork.SellerRepository.Create(sellerEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<SellerDto>(_mapper.Map<SellerDto>(sellerEntity))
                { Message = "Datos insertados correctamente" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SellerService::CreateSellerAsync:: {ex.Message} ");
                throw;
            }
        }
        /// <summary>
        /// Método para actualizar Vendedores
        /// </summary>
        /// <param name="id">Id de Vendedor</param>
        /// <param name="seller">objeto de la clase DTO Vendedores</param>
        /// <returns>Actualziación de datos en BD</returns>
        public async Task<ServiceResponse<SellerDto>> UpdateSellerAsync(int id, SellerDto seller)
        {
            try
            {
                if (seller == null || seller.Name == null)
                    throw new ArgumentException("Seller or a required property is null");

                var sellerEntity = await _unitOfWork.SellerRepository.GetByIdAsync(id);

                if (sellerEntity == null)
                    throw new ArgumentException($"Seller with id: {seller.Id} doesnt exist on DB.");

                sellerEntity.Name = seller.Name;
                sellerEntity.LastName = seller.LastName;
                sellerEntity.IdentificationNumber = seller.IdentificationNumber;

                sellerEntity.UpdateDate = DateTime.Now;

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
        /// <summary>
        /// Método para eliminar varios registros en la BD
        /// </summary>
        /// <param name="ids">Lista de ids de Vendedores</param>
        /// <returns>Varios registros eliminados de la BD</returns>
        public async Task<ServiceResponse<List<SellerDto>>> DeleteSellersAsync(IEnumerable<int> ids)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                List<SellerDto> sellersDeleted = new ();
                foreach (var thirdPartyId in ids)
                {
                    var seller = _unitOfWork.SellerRepository.GetSingle(thirdPartyId);

                    if (seller != null)
                    {
                        _unitOfWork.SellerRepository.Delete(seller);
                        sellersDeleted.Add(_mapper.Map<SellerDto>(seller));
                    }
                }

                await _unitOfWork.CommitAsync();
                return new ServiceResponse<List<SellerDto>>(sellersDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en SellerService::DeleteSellersAsync:: {ex.Message}");
                return new ServiceResponse<List<SellerDto>>($"Error en SellerService::DeleteSellersAsync:: {ex.Message}");
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

using mks.DTOs;
using mks.Dtos;

namespace mks.Interfaces
{
    public interface IWorkerCategoryService
    {
        Task<ServiceResponse> AddWorkerCategoryAsync(AddWorkerCategoryDto dto);

        Task <ServiceResponse> DeleteWorkerCategoryAsync(DeleteWorkerCategoryDto dto);

        Task <ServiceResponse> RenameWorkerCategoryAsync(RenameWorkerCategoryDto dto);

        Task<ServiceResponse> ActivateWorkerCategoryAsync(ActivateWorkerCategoryDto dto);

        Task<ServiceResponse> DeactivateWorkerCategoryAsync(DeactivateWorkerCategoryDto dto);

        Task<ServiceResponse> ListAllWorkerCategoryAsync();

    }
}
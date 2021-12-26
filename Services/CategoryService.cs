using rest_api_m1.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rest_api_m1.Domains.Repositories;
using rest_api_m1.Domains.Communication;

namespace rest_api_m1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnityOfWork _unityOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnityOfWork unityOfWork)
        {
            _categoryRepository = categoryRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unityOfWork.CompleteAsync();

                return new CategoryResponse(category);
            }
            catch(Exception ex)
            {
                return new CategoryResponse($"An erro occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Category not found");

            existingCategory.Name = category.Name;

            try
            {
                _categoryRepository.Update(existingCategory);
                await _unityOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }catch(Exception ex)
            {
                return new CategoryResponse($"An error occured when updating category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Category not found");

            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unityOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }catch(Exception ex)
            {
                return new CategoryResponse($"An error occured when deleting category: {ex.Message}");
            }
        }


    }
}

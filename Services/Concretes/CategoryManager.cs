using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects.CategoryDataTransferObjects;
using Entities.Exceptions.NotFoundExceptions;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;

namespace Services.Concretes
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IDataShaper<CategoryDtoForRead> _dataShaper;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper, IDataShaper<CategoryDtoForRead> dataShaper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        public async Task CreateCategoryAsync(CategoryDtoForInsertion categoryDtoForInsertion)
        {
            var category = _mapper.Map<Category>(categoryDtoForInsertion);
            _categoryRepository.CreateCategory(category);
        }

        public async Task DeleteCategoryAsync(int id, bool trackChanges)
        {
            var category = await CheckAndReturnCategory(id, trackChanges);
            _categoryRepository.DeleteCategory(category);
        }

        public async Task<(IEnumerable<ExpandoObject> categoryDtosForRead, MetaData metaData)> GetAllCategoriesAsync(CategoryParams categoryParams)
        {
            var pagedListResults = await _categoryRepository.GetAllCategoriesAsync(categoryParams);
            var categoryDtosForRead = _mapper.Map<IEnumerable<CategoryDtoForRead>>(pagedListResults);
            var dataShaper = _dataShaper.ShapeData(categoryDtosForRead, categoryParams.Fields);
            return (categoryDtosForRead: dataShaper, metaData: pagedListResults.MetaData);
        }

        public async Task UpdateCategoryAsync(int id, bool trackChanges, CategoryDtoForUpdate categoryDtoForUpdate)
        {
            var category = await CheckAndReturnCategory(id, trackChanges);
            _mapper.Map(categoryDtoForUpdate, category);
            _categoryRepository.UpdateCategory(category);
        }

        private async Task<Category> CheckAndReturnCategory(int id, bool trackChanges)
        {
            var category = await _categoryRepository.GetOneCategoryByConditionAsync(c => c.Id.Equals(id), trackChanges);

            if (category is null)
                throw new CategoryNotFoundException(id);

            return category;
        }
    }
}

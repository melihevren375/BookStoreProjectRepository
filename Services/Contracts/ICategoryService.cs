﻿using Entities.DataTransferObjects.CategoryDataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryDtoForInsertion categoryDtoForInsertion);
        Task UpdateCategoryAsync(Guid id, bool trackChanges, CategoryDtoForUpdate categoryDtoForUpdate);
        Task DeleteCategoryAsync(Guid id, bool trackChanges);
        Task<(IEnumerable<ExpandoObject> categoryDtosForRead, MetaData metaData)> GetAllCategoriesAsync(CategoryParams categoryParams);
    }
}

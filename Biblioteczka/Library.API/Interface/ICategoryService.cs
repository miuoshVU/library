using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API.Interface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategory();
        bool UpdateCategory(int id, CategoryDto categoryDto);
        bool DeleteCategory(int id);
        Category CreateCategory(CategoryDto categoryDto);
        CategoryDto GetCategoryById(int id);
        IEnumerable<CategoryDto> GetCategoryByName(string name);
    }
}

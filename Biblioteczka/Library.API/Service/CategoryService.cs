using AutoMapper;
using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;

namespace Library.API.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Category CreateCategory(CategoryDto categoryDto)
        {
            var category = new Category();
            category = _mapper.Map<Category>(categoryDto);
            _dbContext.Add(category);
            _dbContext.SaveChanges();

            return category;
        }

        public bool DeleteCategory(int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null) return false;

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<CategoryDto> GetAllCategory()
        {
            var categories = _dbContext
                .Categories
                .ToList();

            if (categories is null)
                categories = new List<Category>();

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoriesDtos;
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public IEnumerable<CategoryDto> GetCategoryByName(string name) //Nieużywane
        {
            var categories = _dbContext
                .Categories
                .Where(c => c.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
                
            if (categories is null)
                categories = new List<Category>();

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }

        public bool UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                return false;

            category.Name = categoryDto.Name;
            category.Cover = categoryDto.Cover;

            _dbContext.SaveChanges();
            return true;
        }
    }
}

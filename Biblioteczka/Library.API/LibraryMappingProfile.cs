using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;

namespace Library.API
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.ISBN, c => c.MapFrom(s => s.ISBN))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description));
            
            CreateMap<Author, AuthorDto>()
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.LastName));

            CreateMap<AuthorDto, Author>()
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.LastName));

            CreateMap<BookInstance, BookInstanceDto>()
                .ForMember(m => m.Status, c => c.MapFrom(s => s.Status))
                .ForMember(m => m.Spot, c => c.MapFrom(s => s.Spot));

            CreateMap<BookInstanceDto, BookInstance>()
                .ForMember(m => m.Status, c => c.MapFrom(s => s.Status))
                .ForMember(m => m.Spot, c => c.MapFrom(s => s.Spot));

            CreateMap<Borrow, BorrowDto>()
                .ForMember(m => m.ReturnDate, c => c.MapFrom(s => s.ReturnDate))
                .ForMember(m => m.BorrowDate, c => c.MapFrom(s => s.BorrowDate))
                .ForMember(m=>m.Status,c=>c.MapFrom(s=>s.Status));

            CreateMap<Category, CategoryDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Cover, c => c.MapFrom(s => s.Cover));

            CreateMap<CategoryDto, Category>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Cover, c => c.MapFrom(s => s.Cover));
                
            CreateMap<ProposedBook, ProposedBookDto>()
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.UrlLink, c => c.MapFrom(s => s.UrlLink))
                .ForMember(m => m.Authors, c => c.MapFrom(s => s.Authors));

            CreateMap<ProposedBookDto, ProposedBook>()
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.UrlLink, c => c.MapFrom(s => s.UrlLink))
                .ForMember(m => m.Authors, c => c.MapFrom(s => s.Authors));

            CreateMap<Spot, SpotDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Floor, c => c.MapFrom(s => s.Floor));

            CreateMap<SpotDto, Spot>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Floor, c => c.MapFrom(s => s.Floor));

            CreateMap<User, UserDto>()
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.LastName));
            
            CreateMap<UserDto, User>()
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.LastName));
            CreateMap<BookInstance, BookInstanceSpotInfoDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Spot.Name))
                .ForMember(m => m.Building, c => c.MapFrom(s => s.Spot.Building))
                .ForMember(m => m.Floor, c => c.MapFrom(s => s.Spot.Floor))
                .ForMember(m => m.Status, c => c.MapFrom(s => s.Status))
                .ForMember(m => m.BookId, c => c.MapFrom(s => s.Book.Id));
            CreateMap<Borrow, AddNewBorrow>()
                .ForMember(m => m.Status, c => c.MapFrom(s => s.Status))
                .ForMember(m => m.bookInstance, c => c.MapFrom(s => s.BookInstance))
                .ForMember(m => m.BorrowDate, c => c.MapFrom(s => s.BorrowDate));

            CreateMap<Book, BorrowedBooksByUser>()
                .ForMember(m => m.Title, c => c.MapFrom(s => s.Title))
                .ForMember(m => m.Authors, c => c.MapFrom(s => s.Authors))
                .ForMember(m => m.Categories, c => c.MapFrom(s => s.Categories))
                .ForMember(m => m.BookInstances, c => c.MapFrom(s => s.BookInstances))
                .ForMember(m => m.Cover, c => c.MapFrom(s => s.Cover))
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id));

        }
    }
}


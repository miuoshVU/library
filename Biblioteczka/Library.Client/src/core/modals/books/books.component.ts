import { Component, OnInit } from '@angular/core';
import { faArrowLeft, faArrowRight, faMagnifyingGlass, faPencil, faTrash } from '@fortawesome/free-solid-svg-icons';
import { LibraryService } from 'src/app/services/LibraryService';
import { BookDto } from 'src/app/interfaces/BookDto';
import { AuthorService } from 'src/app/services/AuthorService';
import { CategoryService } from 'src/app/services/CategoryService';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { UpdateBookDto } from 'src/app/interfaces/UpdateBookDto';
import { Subscription } from 'rxjs';
import { DataService } from 'src/app/services/data.service';
import Swal from 'sweetalert2';

interface MultiSelect {
  item_id: number,
  item_text: string
}

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  faMagnifyingGlass = faMagnifyingGlass;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  faTrash = faTrash;
  faPencil = faPencil;
  books: BookDto[] = [];

  authors: MultiSelect[] = [];
  selectedAuthors: MultiSelect[] = [];
  categories: MultiSelect[] = [];
  selectedCategories: MultiSelect[] = [];

  dropdownSettings: IDropdownSettings = {};

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  constructor(private libraryService: LibraryService, private authorService: AuthorService, 
    private categoryService: CategoryService, private dataService: DataService) { }

  ngOnInit(): void {
    this.authors = [];
    this.categories = [];
    this.libraryService.getAllBooks().subscribe(resp => {
      this.books = resp;
    }
    );
    this.authorService.getAuthors().subscribe((resp => {
      resp.forEach((res) => {
        this.authors = this.authors.concat({
          item_id: res.id!,
          item_text: res.firstName! + ' ' + res.lastName!
        })
      })
    }))
    this.categoryService.getAllCategories().subscribe((resp => {
      resp.forEach((res) => {
        this.categories = this.categories.concat({
          item_id: res.id!,
          item_text: res.name
        })
      })
    }))
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Wybierz wszystko',
      unSelectAllText: 'Odznacz wszystko',
      searchPlaceholderText: 'Szukaj',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
  }

  getInfo(book: BookDto) {
      this.selectedAuthors = [];
      book.authors.forEach((resp) => {
        this.selectedAuthors = this.selectedAuthors.concat({
        item_id: resp.id!,
        item_text: resp.firstName! + ' ' + resp.lastName!
      })
    }
      );
      this.selectedCategories = [];
      book.categories.forEach((resp) => {
        this.selectedCategories = this.selectedCategories.concat({
        item_id: resp.id!,
        item_text: resp.name!
      })
    }
      );
  }

  deleteBook(bookId: number) {
    Swal.fire({
      title: 'Czy na pewno chcesz usunąć tę książkę?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Usuń',
    }).then((result) => {
      if (result.isConfirmed) {
        this.libraryService.deleteBook(bookId).subscribe((resp) => {
          this.libraryService.getAllBooks().subscribe((res) => {
            this.books = res;
          })
        });
      }
    }
    );
  
  }

  editBook(book: BookDto) {

    let authorsIds: number[] = []
    let categoriesIds: number[] = []
    this.selectedAuthors.forEach((resp) => {
        authorsIds.push(resp.item_id);
    })  
    this.selectedCategories.forEach((resp)=> {
      categoriesIds.push(resp.item_id);
  })  
    let book1 = {
      title: book.title,
      iSBN: book.iSBN,
      yearOfPublication: book.yearOfPublication,
      description: book.description,
      publisher: book.publisher,
      cover: book.cover,
      pages: book.pages,
      categoryIds: categoriesIds,
      authorIds: authorsIds
    }
   
      this.libraryService.editBook(book.id, book1).subscribe((resp) => {
        this.libraryService.getAllBooks().subscribe((resp) => {
          this.books = resp;
        });
      });
  }

}

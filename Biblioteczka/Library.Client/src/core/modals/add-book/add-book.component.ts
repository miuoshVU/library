import { Component, OnInit } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { AddNewBookDto } from 'src/app/interfaces/AddNewBookDto';
import { BookDto } from 'src/app/interfaces/BookDto';
import { AuthorService } from 'src/app/services/AuthorService';
import { CategoryService } from 'src/app/services/CategoryService';
import { DataService } from 'src/app/services/data.service';
import { LibraryService } from 'src/app/services/LibraryService';

interface MultiSelect {
  item_id: number,
  item_text: string
}

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {

  authors: MultiSelect[] = [];
  selectedAuthors: MultiSelect[] = [];
  categories: MultiSelect[] = [];
  selectedCategories: MultiSelect[] = [];
  book = {} as BookDto;

  dropdownSettings: IDropdownSettings = {};

  constructor(private authorService: AuthorService, private categoryService: CategoryService,
    private libraryService: LibraryService, private dataService: DataService) { }

  ngOnInit() {
    this.authors = [];
    this.categories = [];
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

  addBook(book: BookDto) {
    let authorsIds: number[] = []
    let categoriesIds: number[] = []
    this.selectedAuthors.forEach((resp) => {
      authorsIds.push(resp.item_id);
    })
    this.selectedCategories.forEach((resp) => {
      categoriesIds.push(resp.item_id);
    })
    let book1: AddNewBookDto = {
      title: book.title,
      iSBN: book.iSBN,
      yearOfPublication: book.yearOfPublication,
      description: book.description,
      publisher: book.publisher,
      cover: book.cover,
      pages: book.pages,
      authorIds: authorsIds,
      categoryIds: categoriesIds
    }
    this.libraryService.addBook(book1).subscribe(() => {
      this.notifyForChange();
    });
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

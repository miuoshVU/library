import { Component, OnInit } from '@angular/core';
import { ProposedBooksDto } from 'src/app/interfaces/ProposedBooksDto';
import { ProposedBookService } from 'src/app/services/ProposedBookService';
import { IDropdownSettings } from 'ng-multiselect-dropdown'
import { AuthorService } from 'src/app/services/AuthorService';
import { CategoryService } from 'src/app/services/CategoryService';
import { DataService } from 'src/app/services/data.service';

interface MultiSelect {
  item_id: number,
  item_text: string
}

@Component({
  selector: 'app-add-proposed-book',
  templateUrl: './add-proposed-book.component.html',
  styleUrls: ['./add-proposed-book.component.css']
})
export class AddProposedBookComponent implements OnInit {

  proposedBook = {} as ProposedBooksDto;
  authors: MultiSelect[] = [];
  selectedAuthors: MultiSelect[] = [];
  categories: MultiSelect[] = [];
  selectedCategories: MultiSelect[] = [];

  dropdownSettings: IDropdownSettings = {};

  constructor(private authorService: AuthorService, private categoryService: CategoryService,
     private proposedBooksService: ProposedBookService, private dataService: DataService) { }

  ngOnInit(): void {
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
      unSelectAllText: 'Odznacz wszystko',
      searchPlaceholderText: 'Szukaj',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false
    };
  }

  proposeBook(proposedBook: ProposedBooksDto) {
    this.proposedBooksService.addProposedBook(proposedBook).subscribe();
    this.notifyForChange();
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

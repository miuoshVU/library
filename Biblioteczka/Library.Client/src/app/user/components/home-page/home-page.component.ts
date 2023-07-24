import { Component, OnInit } from '@angular/core';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';
import { BookDto } from 'src/app/interfaces/BookDto';
import { BookInstanceDto } from 'src/app/interfaces/BookInstanceDto';
import { CategoryDto } from 'src/app/interfaces/CategoryDto';
import { BookInstanceService } from 'src/app/services/BookInstanceService';
import { CategoryService } from 'src/app/services/CategoryService';
import { DataService } from 'src/app/services/data.service';
import { LibraryService } from 'src/app/services/LibraryService';

interface Book {
  title: string,
  id: number
}

declare var $: any;

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})


export class HomePageComponent implements OnInit {
  faMagnifyingGlass = faMagnifyingGlass;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  input: string = "";
  bookNames: BookDto[] = [];
  books: BookDto[] | undefined;
  categories: CategoryDto[] | undefined;
  categoryBooks: BookDto[] | undefined;
  backId: string | undefined;
  userId: string = this.cookieService.get('userId');
  name: string = this.cookieService.get('name');
  borrowedBooks: BookInstanceDto[] | undefined;
  bookInstances: BookInstanceDto[] | undefined;
  days: number = 0;

  constructor(private libraryService: LibraryService, private cookieService: CookieService, private dataService: DataService,
    private categoryService: CategoryService, private bookInstanceService: BookInstanceService) { }

    notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
      this.ngOnInit();
    });


  ngOnInit(): void {
    this.libraryService.getAllBooks().subscribe(resp => {
      this.books = resp;
    }
    );
    this.categoryService.getAllCategories().subscribe(resp => {
      this.categories = resp;
    }
    );
    this.libraryService.borrowedBooks(this.userId).subscribe((resp) => {
      this.borrowedBooks = resp;
      console.log(this.borrowedBooks)
    })
  }

  getAllBooksCategory(categoryId: number) {
    this.libraryService.getAllBooksFromCategory(categoryId).subscribe(resp => {
      this.categoryBooks = resp;
    })
  }

  getBookInstance(bookId: number) {
    this.bookInstanceService.getBookInstancesById(bookId).subscribe((resp) => {
      this.bookInstances = resp;
      console.log('book instances', this.bookInstances)
    }
    );
  }

  searchBook(input: any) {
    this.bookNames = [];
    if (input.value === '') {
      document.getElementById('search-bar')!.style.borderRadius = "0.375rem";
    }
    else {
      document.getElementById('search-bar')!.style.borderBottomLeftRadius = "unset";
      document.getElementById('search-bar')!.style.borderBottomRightRadius = "unset";
    }
    this.libraryService.searchBooks(input.value).subscribe((resp: any) => {
      resp.forEach((element: any) => {
        if (this.bookNames?.find(x => x.title === element.title)) {
        }
        else {
          this.bookNames?.push(element);
        }
      });
    });
  }

  setBackId(page: string) {
    this.backId = page;
  }

  returnRemainingDays(borrowedBook: BookInstanceDto) {
      this.libraryService.howManyDaysToReturnBook(1).subscribe((resp) => {
        this.days = resp;
      });
  }

  prolongBook(borrowedID: number) {
      this.libraryService.renewABook(borrowedID).subscribe();
  }


}

import { Component, OnInit } from '@angular/core';
import { faMagnifyingGlass, faTrash } from '@fortawesome/free-solid-svg-icons';
import { faArrowLeft, faArrowRight, faPencil } from '@fortawesome/free-solid-svg-icons';
import { fromEvent } from "rxjs";
import { BookInstanceDto } from 'src/app/interfaces/BookInstanceDto';
import { BookInstanceService } from 'src/app/services/BookInstanceService';
import { Status } from 'src/app/interfaces/Status'
import { DataService } from 'src/app/services/data.service';
import { Subscription } from 'rxjs';
import { IDropdownSettings } from 'ng-multiselect-dropdown'
import { SpotService } from 'src/app/services/SpotService';
import { LibraryService } from 'src/app/services/LibraryService';
import Swal from 'sweetalert2';

declare var $: any
declare var bootstrap: any

interface MultiSelect {
  item_id: number,
  item_text: string
}

@Component({
  selector: 'app-admin-home-page',
  templateUrl: './admin-home-page.component.html',
  styleUrls: ['./admin-home-page.component.css']
})
export class AdminHomePageComponent implements OnInit {

  faMagnifyingGlass = faMagnifyingGlass;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  faPencil = faPencil;
  faTrash = faTrash;
  bookInstances: BookInstanceDto[] | undefined;
  statusEnum: typeof Status = Status;

  titles: MultiSelect[] = [];
  selectedTitle: MultiSelect[] = [];
  spots: MultiSelect[] = [];
  selectedSpot: MultiSelect[] = [];
  states: MultiSelect[] = [];
  selectedState: MultiSelect[] = [];
  closeDropdownSelection = false;

  dropdownSettings: IDropdownSettings = {};

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  constructor(private bookInstanceService: BookInstanceService,
    private dataService: DataService, private spotService: SpotService,
    private libraryService: LibraryService) {
  }

  ngOnInit(): void {
    this.spots = [];
    this.titles = [];
    this.states = [];
    this.bookInstanceService.getBookInstances().subscribe((resp) => {
      this.bookInstances = resp;
    });
    this.spotService.getSpots().subscribe((resp) => {
      resp.forEach((res) => {
        this.spots = this.spots.concat({
          item_id: res.id!,
          item_text: res.name!
        })
      })
    })
    this.libraryService.getAllBooks().subscribe((resp) => {
      resp.forEach((res) => {
        this.titles = this.titles.concat({
          item_id: res.id!,
          item_text: res.title!
        })
      })
    })
    const keys = Object.keys(Status).filter((v) => isNaN(Number(v)));

    keys.forEach((key, index) => {
      this.states = this.states.concat({
        item_id: index,
        item_text: key
      })
    });
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

  getInfo(book: BookInstanceDto) {

    this.selectedSpot = [];
    let i = 1;
    this.spots.forEach((resp) => {
      if (this.spots.find((x) => x.item_text === this.selectedSpot[0].item_text)) {
        this.selectedSpot = this.selectedSpot.concat({
          item_id: resp.item_id!,
          item_text: resp.item_text
        })
      }
    }
    );

  }

  openNav() {
    document.getElementById('mySidenav')!.style.width = "250px";
    document.getElementById('fade')!.classList.add('fade');
    document.getElementById('disableClick')!.style.pointerEvents = "none";
  }

  closeNav() {
    document.getElementById('mySidenav')!.style.width = "0px";
    document.getElementById('fade')!.classList.remove('fade');
    document.getElementById('disableClick')!.style.pointerEvents = "unset";
  }

  onItemSelect(item: any) {
    this.selectedTitle = [item];
  }

  onItemSelect1(item: any) {
    this.selectedSpot = [item];
  }

  onItemSelect2(item: any) {
    this.selectedState = [item];
  }

  editBook(owner: string) {
    this.bookInstanceService.editBookInstance({
      bookID: this.selectedTitle[0].item_id,
      spotID: this.selectedSpot[0].item_id,
      owner: owner,
      status: this.selectedState[0].item_id
    }).subscribe();
  }

  deleteBook(bookId: number) {
    Swal.fire({
      title: 'Czy na pewno chcesz usunąć tę instancję?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Usuń',
    }).then((result) => {
      if (result.isConfirmed) {
        this.bookInstanceService.deleteBookInstance(bookId).subscribe(() => {
          this.bookInstanceService.getBookInstances().subscribe((resp) => {
            this.bookInstances = resp;
          })
        })
      }
    }
    );
  }
}



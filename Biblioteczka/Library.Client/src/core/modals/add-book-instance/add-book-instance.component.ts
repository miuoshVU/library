import { Component, OnInit } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { BookInstanceService } from 'src/app/services/BookInstanceService';
import { LibraryService } from 'src/app/services/LibraryService';
import { Status } from 'src/app/interfaces/Status'
import { SpotService } from 'src/app/services/SpotService';
import { BookInstanceDto } from 'src/app/interfaces/BookInstanceDto';
import { DataService } from 'src/app/services/data.service';

interface MultiSelect {
  item_id: number,
  item_text: string
}

declare var $: any;

@Component({
  selector: 'app-add-book-instance',
  templateUrl: './add-book-instance.component.html',
  styleUrls: ['./add-book-instance.component.css']
})
export class AddBookInstanceComponent implements OnInit {

  titles: MultiSelect[] = [];
  selectedTitle: MultiSelect[] = [];
  spots: MultiSelect[] = [];
  selectedSpot: MultiSelect[] = [];
  owner: string = '';
  states: MultiSelect[] = [];
  selectedState: MultiSelect[] = [];
  closeDropdownSelection = false;
  bookInstances: BookInstanceDto[] = [];

  dropdownSettings: IDropdownSettings = {};

  constructor(private libraryService: LibraryService,
    private bookInstanceService: BookInstanceService,
    private spotService: SpotService, private dataService: DataService) { }

  ngOnInit(): void {
    this.spots = [];
    this.titles = [];
    this.states = [];
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

  onItemSelect(item: any) {
    this.selectedTitle = [item];
  }

  onItemSelect1(item: any) {
    this.selectedSpot = [item];
  }

  onItemSelect2(item: any) {
    this.selectedState = [item];
  }

  addBook(owner: string) {

    console.log(this.selectedTitle[0].item_id);

    console.log(this.selectedSpot[0].item_id);

    console.log(this.selectedState[0].item_id);
    console.log('owner')
    
    this.bookInstanceService.addBookInstance({
      bookID: this.selectedTitle[0].item_id,
      spotID: this.selectedSpot[0].item_id,
      owner: owner,
      status: this.selectedState[0].item_id
    }).subscribe(() => {
      $('.modal-backdrop').remove();
      this.notifyForChange();
    });
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

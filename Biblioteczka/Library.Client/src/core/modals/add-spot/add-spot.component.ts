import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { SpotDto } from 'src/app/interfaces/SpotDto';
import { DataService } from 'src/app/services/data.service';
import { SpotService } from 'src/app/services/SpotService';

declare var $: any;
@Component({
  selector: 'app-add-spot',
  templateUrl: './add-spot.component.html',
  styleUrls: ['./add-spot.component.css']
})
export class AddSpotComponent implements OnInit {


  spot = {} as SpotDto;

  constructor(private spotService: SpotService, private dataService: DataService) { }

  ngOnInit(): void {
  }

  addSpot(spot: SpotDto) {
    console.log(spot)
      this.spotService.addSpot(spot).subscribe(() => {
        $('.modal-backdrop').remove();
        this.notifyForChange();
      });
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

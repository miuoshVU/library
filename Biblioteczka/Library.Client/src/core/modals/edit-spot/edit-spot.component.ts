import { Component, OnInit } from '@angular/core';
import { SpotDto } from 'src/app/interfaces/SpotDto';
import { DataService } from 'src/app/services/data.service';
import { SpotService } from 'src/app/services/SpotService';

@Component({
  selector: 'app-edit-spot',
  templateUrl: './edit-spot.component.html',
  styleUrls: ['./edit-spot.component.css']
})
export class EditSpotComponent implements OnInit {
  spots: SpotDto[] | undefined;

  constructor(private spotService: SpotService, private dataService: DataService) { }

  ngOnInit(): void {
    this.spotService.getSpots().subscribe((resp) => {
      this.spots = resp;
    })
  }

  editSpot(spot: SpotDto) {
    this.spotService.editSpot(spot).subscribe(() => {
        this.notifyForChange();
    }
    );
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

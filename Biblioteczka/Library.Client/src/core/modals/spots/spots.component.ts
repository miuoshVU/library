import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SpotDto } from 'src/app/interfaces/SpotDto';
import { DataService } from 'src/app/services/data.service';
import { SpotService } from 'src/app/services/SpotService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-spots',
  templateUrl: './spots.component.html',
  styleUrls: ['./spots.component.css']
})
export class SpotsComponent implements OnInit {

  spots: SpotDto[] | undefined;

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  constructor(private spotService: SpotService, private dataService: DataService) { }

  ngOnInit(): void {
      this.spotService.getSpots().subscribe((resp) => {
        this.spots = resp;
      })
  }

  deleteSpot(spotId: number) {
    Swal.fire({
      title: 'Czy na pewno chcesz usunąć to miejsce?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Usuń',
    }).then((result) => {
      if (result.isConfirmed) {
        this.spotService.deleteSpot(spotId).subscribe(() => {
          this.spotService.getSpots().subscribe((resp) => {
            this.spots = resp;
          })
        });
      }
    }
    );
  }

}

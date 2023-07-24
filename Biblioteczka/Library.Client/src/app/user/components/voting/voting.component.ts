import { Component, OnInit } from '@angular/core';
import { faCirclePlus } from '@fortawesome/free-solid-svg-icons';
import { ProposedBooksDto } from 'src/app/interfaces/ProposedBooksDto';
import { ProposedBookService } from 'src/app/services/ProposedBookService';
import { interval, Subscription } from 'rxjs';
import { DataService } from 'src/app/services/data.service';
import { CookieService } from 'ngx-cookie-service';

export var $: any;

@Component({
  selector: 'app-voting',
  templateUrl: './voting.component.html',
  styleUrls: ['./voting.component.css']
})
export class VotingComponent implements OnInit {

  faCirclePlus = faCirclePlus;
  time: string = "";
  proposedBooks: ProposedBooksDto[] = [];
  runInterval: any;
  votes: number = parseInt(this.cookiesService.get('votes'));

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  constructor(private proposedBookService: ProposedBookService, private cookiesService: CookieService, private dataService: DataService) { }

  ngOnInit(): void {
    this.runInterval = setInterval(() => {
      this.setClockWithCurrentTime();
    }, 1)
    interval;
    this.setClockWithCurrentTime();
    this.proposedBookService.getProposedBooks().subscribe((resp) => {
      this.proposedBooks = resp;
    })
  }

  ngOnDestroy() {
    if (this.runInterval) {
      clearInterval(this.runInterval);
    }
  }

  currentTime() {

    let date = new Date();
    let hh = date.getHours();
    let mm = date.getMinutes();
    let ss = date.getSeconds();
    let session = "AM";

    if (hh === 0) {
      hh = 12;
    }
    if (hh > 12) {
      hh = hh - 12;
      session = "PM";
    }

    hh = (hh < 10) ? 0 + hh : hh;
    mm = (mm < 10) ? 0 + mm : mm;
    ss = (ss < 10) ? 0 + ss : ss;
    this.time = hh + ":" + mm + ":" + ss + " " + session;

  }

 updateClock(hours: number, minutes: number, seconds: number) {

  var hourDegrees = hours * 30;
  var minuteDegrees = minutes * 6;
  var secondDegrees = seconds * 6;

  if (document.querySelector('.hour-hand') !== null) {
  (document.querySelector('.hour-hand') as HTMLElement).style.transform = `rotate(${hourDegrees}deg)`;
  (document.querySelector('.minute-hand') as HTMLElement).style.transform = `rotate(${minuteDegrees}deg)`;
  (document.querySelector('.second-hand') as HTMLElement).style.transform = `rotate(${secondDegrees}deg)`;
  }

}

 setClockWithCurrentTime() {
    var date = new Date();

  var hours = ((date.getHours() + 11) % 12 + 1);
  var minutes = date.getMinutes();
  var seconds = date.getSeconds();

  this.updateClock(hours, minutes, seconds);
}


}

import { Component, OnInit, HostListener, Renderer2 } from '@angular/core';
import { Router } from '@angular/router'
import { fromEvent, timeout } from "rxjs";

import {
  faCoffee, faArrowUp, faArrowDown, faArrowRightFromBracket,
  faTrophy, faUser, faBook, faCamera, faHouse
} from '@fortawesome/free-solid-svg-icons';

import { ViewportScroller } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';
import Swal from 'sweetalert2';

declare var bootstrap: any;
declare var $: any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  faCoffee = faCoffee;
  faArrowUp = faArrowUp;
  faArrowDown = faArrowDown;
  faArrowRightFromBracket = faArrowRightFromBracket;
  faTrophy = faTrophy;
  faUser = faUser;
  faBook = faBook;
  faCamera = faCamera;
  faHouse = faHouse;
  role: string | null = this.cookieService.get('role');

  constructor(private viewportScroller: ViewportScroller,
    private _router: Router, private cookieService: CookieService) { }


  public onClick(elementId: string): void {
    this.viewportScroller.scrollToAnchor(elementId);
  }

  logoutConfirm() {
    Swal.fire({
      title: 'Czy na pewno chcesz się wylogować?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Wyloguj',
    }).then((result) => {
      if (result.isConfirmed) {
          this._router.navigate(['/']);
          this.logout();
      }
    }
    );
  }

  logout() {
    this.cookieService.deleteAll();
    document.getElementById('nav')!.style.display = 'none';
    Swal.fire({
      icon: 'success',
      title: 'Sukces!',
      text: 'Udało Ci się wylogować!',
      confirmButtonColor: 'rgba(55, 168, 60)'
    }
    )
  }

  ngOnInit(): void {
  }
}

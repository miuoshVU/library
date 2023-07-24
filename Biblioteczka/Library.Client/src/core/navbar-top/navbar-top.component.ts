import { Component, OnInit, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-navbar-top',
  templateUrl: './navbar-top.component.html',
  styleUrls: ['./navbar-top.component.css']
})
export class NavbarTopComponent implements OnInit {

  constructor(private renderer: Renderer2, private cookieService: CookieService,
    private _router: Router) { }

  backgroundChangeOnScroll(nav: HTMLElement) {
    if (window.scrollY >= 100) {
      this.renderer.addClass(nav, 'nav-black');
    }
    else {
      this.renderer.removeClass(nav, 'nav-black');
    }
  }

  checkByRole() {
    if (this.cookieService.get('token')) {
        if (this.cookieService.get('role') === 'admin')
            return '/adminHomePage'
        if (this.cookieService.get('role') === 'user')
            return '/home'
    }
    return
  }

  ngOnInit(): void {
  }

}

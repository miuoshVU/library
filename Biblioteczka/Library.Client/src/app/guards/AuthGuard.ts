import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private router: Router, private jwtHelper: JwtHelperService,
    private cookieService: CookieService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = this.cookieService.get('token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      if (route.data[0] === 'admin' && this.cookieService.get('role') === 'admin')
        return true;
      else if (route.data[0] === 'user' && this.cookieService.get('role') === 'user')
        return true;
      else if (route.data[0] === 'user, admin')
        return true;
      console.log(this.router.url)
      this.router.navigate([`${this.router.url}`])
      return false;
    }
    return false;
  }
}
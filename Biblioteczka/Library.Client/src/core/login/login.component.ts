import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/AuthenticationService";
import { UserDto } from "src/app/interfaces/UserDto";
import Swal from 'sweetalert2';
import { DataService } from "src/app/services/data.service";
import { CookieService } from "ngx-cookie-service";
import StandardCharsets from "@zxing/library/esm/core/util/StandardCharsets";

export var User = {} as UserDto;

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  email: string = ""
  password: string = "";

  constructor(private _router: Router, private dataService: DataService,
    private authenticationService: AuthenticationService, private cookieService: CookieService) {
  }

  ngOnInit(): void {
    let token = this.cookieService.get('token');
    if (token) {
      if (this.cookieService.get('role') === 'user')
        this._router.navigate(['/home'])
      else if (this.cookieService.get('role') === 'admin')
        this._router.navigate(['/adminHomePage'])
    }
  }

  onSubmit() {
    this.authenticationService.getUser(this.email, this.password).subscribe((resp) => {
      if (resp !== null) {
        this.cookieService.set('token', resp.token!, {
          expires: 1 / parseInt(resp.exp!),
          path: '/',
          sameSite: 'Strict'
        });
        this.cookieService.set('role', resp.user!.role, {
          expires: 1 / parseInt(resp.exp!),
          path: '/',
          sameSite: 'Strict'
        });
        this.cookieService.set('userId', resp.user!.id, {
          expires: 1 / parseInt(resp.exp!),
          path: '/',
          sameSite: 'Strict'
        });
        this.cookieService.set('name', `${resp.user!.firstName} ${resp.user!.lastName}`, {
          expires: 1 / parseInt(resp.exp!),
          path: '/',
          sameSite: 'Strict'
        });
        this.cookieService.set('email', `${resp.user!.email}`, {
          expires: 1 / parseInt(resp.exp!),
          path: '/',
          sameSite: 'Strict'
        });
        this.cookieService.set('votes', `${resp.user!.remainingVotes}`, {
          expires: 1 / parseInt(resp.exp!),
          path: '/',
          sameSite: 'Strict'
        });

        // User = resp.user!;
        this.notifyForChange();
        if (resp.user!.role !== "admin")
          return this._router.navigate(['/home']);
        else
          return this._router.navigate(['/adminHomePage']);
      }
      return
    }, error => Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: 'Niepoprawne dane logowania!',
      confirmButtonColor: '#F71735'
    })
    );
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

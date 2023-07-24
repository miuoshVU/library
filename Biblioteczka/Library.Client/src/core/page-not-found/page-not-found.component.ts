import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
// import { IUserDto } from 'src/app/Dto/IUserDto';
// import { UserService } from 'src/app/services/userService';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.css']
})
export class PageNotFoundComponent implements OnInit {
  constructor(private _router: Router) { }

  ngOnInit(): void {
  }

  redirectTo() {
    return this._router.navigate(['/']);
  }


}
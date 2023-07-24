import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { UpdateUserDto } from 'src/app/interfaces/UpdateUserDto';
import { UserDto } from 'src/app/interfaces/UserDto';
import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/UserService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-settings',
  templateUrl: './account-settings.component.html',
  styleUrls: ['./account-settings.component.css']
})
export class AccountSettingsComponent implements OnInit {

  paswd: string = '';
  paswd1: string = '';
  email1: string = '';
  userId: string = this.cookieService.get('userId');
  name: string = this.cookieService.get('name');
  email: string = this.cookieService.get('email');
  votes: number = parseInt(this.cookieService.get('votes'));
  role: string = this.cookieService.get('role');

  constructor(private userService: UserService, private dataService: DataService, private cookieService: CookieService) { }

  ngOnInit(): void {
  }

  editLoginData() {
      let user1 = {} as UpdateUserDto;
      if (this.paswd === this.paswd1 && this.email === this.email1) {
      user1 = {
        firstName: this.name.split(" ")[0],
        lastName: this.name.split(" ")[1],
        email: this.email,
        paswd: this.paswd,
        remainingVotes: this.votes,
        role: this.role,
        avatar: null,
        proposedBooks: [],
        borrows: []
      }
      this.userService.updateUser(this.userId, user1).subscribe(() => {
        Swal.fire({
          icon: 'success',
          title: 'Sukces!',
          text: 'Udało Ci się zmienić dane!',
          confirmButtonColor: 'rgba(55, 168, 60)'
        });
      });
    }
    else {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'E-mail lub hasło nie są takie same!',
        confirmButtonColor: '#F71735'
      })
    }
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

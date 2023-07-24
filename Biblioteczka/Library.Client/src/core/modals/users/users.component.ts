import { Component, OnInit } from '@angular/core';
import { faCirclePlus, faMagnifyingGlass, faTractor, faTrash } from '@fortawesome/free-solid-svg-icons';
import { faArrowLeft, faArrowRight, faPencil } from '@fortawesome/free-solid-svg-icons';
import { UserDto } from 'src/app/interfaces/UserDto';
import { UserService } from 'src/app/services/UserService';
import { DataService } from '../../../app/services/data.service';
import { Subscription } from 'rxjs';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { UpdateUserDto } from 'src/app/interfaces/UpdateUserDto';
import Swal from 'sweetalert2';
import { CookieService } from 'ngx-cookie-service';

interface MultiSelect {
  item_id: number,
  item_text: string
}

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: UserDto[] = [];

  faMagnifyingGlass = faMagnifyingGlass;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  faPencil = faPencil;
  faTrash = faTrash;
  faCirclePlus = faCirclePlus;
  user1: UserDto | undefined;
  roles: MultiSelect[] = [];
  selectedRole: MultiSelect[] = [];
  passwd: string = '';
  email: string = this.cookieService.get('email');

  dropdownSettings: IDropdownSettings = {};

  constructor(private userService: UserService, private dataService: DataService, private cookieService: CookieService) { }

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  ngOnInit(): void {
    this.roles = [];
    this.userService.getAllUsers().subscribe((resp) => {
      this.users = resp;
      let i = 1;
      resp.forEach((res) => {
        if (this.roles.find((x) => res.role === x.item_text)) {
        }
        else {
          this.roles = this.roles.concat({
            item_id: i,
            item_text: res.role!
          })
          i = i + 1;
        }
      }

      );
      this.users = this.users.filter((res) => res.email !== this.email);
    })
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      enableCheckAll: false
    };
  }

  deleteUser(userId: string) {
    Swal.fire({
      title: 'Czy na pewno chcesz usunąć tego użytkownika?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Usuń',
    }).then((result) => {
      if (result.isConfirmed) {
        this.userService.deleteUser(userId).subscribe((resp) => {
          this.ngOnInit();
        });
      }
    }
    );
  }

  getInfo(user: UserDto) {
    this.selectedRole = [];
    
    this.roles.forEach((resp) => {
      if (resp.item_text === user.role) {
        this.selectedRole = this.selectedRole.concat({
          item_id: resp.item_id!,
          item_text: resp.item_text
        })
      }

    }
    )
  }

  onItemSelect(item: any) {
    this.selectedRole = [item];
  }

  editUser(user: UserDto) {
    let updatedUser: UpdateUserDto = {
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      role: this.selectedRole[0].item_text,
      paswd: this.passwd,
      avatar: null,
      remainingVotes: user.remainingVotes,
      borrows: [],
      proposedBooks: []
    };
        console.log('uipdated user', updatedUser)
    this.userService.updateUser(user.id, updatedUser).subscribe(() => {
      this.notifyForChange();
    });
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

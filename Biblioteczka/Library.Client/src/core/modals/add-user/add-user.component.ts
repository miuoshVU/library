import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/interfaces/UserDto';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { UserService } from 'src/app/services/UserService';
import { UpdateUserDto } from 'src/app/interfaces/UpdateUserDto';
import { DataService } from '../../../app/services/data.service'

interface MultiSelect {
  item_id: number,
  item_text: string
}

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  roles: MultiSelect[] = [];
  selectedRole: MultiSelect[] = [];
  passwd: string = '';
  user = {} as UserDto;

  dropdownSettings: IDropdownSettings = {};

  constructor(private userService: UserService, private dataService: DataService) { }

  ngOnInit(): void {
    this.roles = [];
    this.userService.getAllUsers().subscribe((resp) => {
      let i: number = 1;
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
      });
    })
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      enableCheckAll: false
    };
  }

  onItemSelect(item: any) {
    this.selectedRole = [item];
  }

  addUser(user: UserDto) {
    let user1: UpdateUserDto = {
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      role: this.selectedRole[0].item_text,
      paswd: this.passwd,
      avatar: null,
      remainingVotes: 5,
      borrows: [],
      proposedBooks: []
    };
    this.userService.addUser(user1).subscribe(() => {
      this.notifyForChange();
    });
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }


}

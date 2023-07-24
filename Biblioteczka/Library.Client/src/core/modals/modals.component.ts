import { Component, OnInit } from '@angular/core';
import { faPencil, faTrash } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-modals',
  templateUrl: './modals.component.html',
  styleUrls: ['./modals.component.css']
})


export class ModalsComponent implements OnInit {


  faPencil = faPencil;
  faTrash = faTrash;

  constructor(){}
  

  ngOnInit(): void {

  }

}

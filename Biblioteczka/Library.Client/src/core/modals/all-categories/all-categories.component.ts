import { Component, OnInit } from '@angular/core';
import { faPencil, faTrash } from '@fortawesome/free-solid-svg-icons';
import { fromEvent } from "rxjs";
import { CategoryDto } from 'src/app/interfaces/CategoryDto';
import { CategoryService } from 'src/app/services/CategoryService';
import { DataService } from 'src/app/services/data.service';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';

declare var $: any;

@Component({
  selector: 'app-all-categories',
  templateUrl: './all-categories.component.html',
  styleUrls: ['./all-categories.component.css']
})
export class AllCategoriesComponent implements OnInit {

  faPencil = faPencil;
  faTrash = faTrash;
  categories: CategoryDto[] = [];

  constructor(private categoryService: CategoryService, private dataService: DataService) { }

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe((resp) => {
      this.categories = resp;
    })
  }

  deleteCategory(category: CategoryDto) {
    Swal.fire({
      title: 'Czy na pewno chcesz usunąć tę kategorię?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Usuń',
    }).then((result) => {
      if (result.isConfirmed) {
        this.categoryService.deleteCategory(category.id!).subscribe(() => {
          this.categoryService.getAllCategories().subscribe((resp) => {
            this.categories = resp;
          })
        });
      }
    }
    );
  }

}

import { Component, OnInit } from '@angular/core';
import { CategoryDto } from 'src/app/interfaces/CategoryDto';
import { CategoryService } from 'src/app/services/CategoryService';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {

  categories: CategoryDto[] = [];

  constructor(private categoryService: CategoryService, private dataService: DataService) { }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe((resp) => {
      this.categories = resp;
    })
  }

  editCategory(category: CategoryDto) {
    this.categoryService.editCategory(category).subscribe(() => {
      this.ngOnInit();
      this.notifyForChange();
    })
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

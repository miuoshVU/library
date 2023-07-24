import { Component, OnInit } from '@angular/core';
import { CategoryDto } from 'src/app/interfaces/CategoryDto';
import { CategoryService } from 'src/app/services/CategoryService';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  category = {} as CategoryDto;

  constructor(private categoryService: CategoryService, private dataService: DataService) { }

  ngOnInit(): void {
  }

  addCategory(category: CategoryDto) {
    this.categoryService.addCategory(category).subscribe(() => {
      this.notifyForChange();
    })
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

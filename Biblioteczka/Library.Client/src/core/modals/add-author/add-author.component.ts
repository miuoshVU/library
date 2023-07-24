import { Component, OnInit } from '@angular/core';
import { AuthorDto } from 'src/app/interfaces/AuthorDto';
import { AuthorService } from 'src/app/services/AuthorService';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-add-author',
  templateUrl: './add-author.component.html',
  styleUrls: ['./add-author.component.css']
})
export class AddAuthorComponent implements OnInit {

  author = {} as AuthorDto;

  constructor(private authorService: AuthorService, private dataService: DataService) { }

  ngOnInit(): void {
  }

  addAuthor(author: AuthorDto) {
    this.authorService.addAuthor(author).subscribe(() => {
      this.notifyForChange();
    });
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

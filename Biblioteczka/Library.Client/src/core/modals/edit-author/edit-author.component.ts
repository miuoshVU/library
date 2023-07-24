import { Component, OnInit } from '@angular/core';
import { AuthorDto } from 'src/app/interfaces/AuthorDto';
import { AuthorService } from 'src/app/services/AuthorService';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-edit-author',
  templateUrl: './edit-author.component.html',
  styleUrls: ['./edit-author.component.css']
})
export class EditAuthorComponent implements OnInit {

  authors: AuthorDto[] = [];

  constructor(private authorService: AuthorService, private dataService: DataService) { }

  ngOnInit(): void {
    this.authorService.getAuthors().subscribe((resp) => {
      this.authors = resp;
    })
  }

  editAuthor(author: AuthorDto) {
    this.authorService.editAuthor(author).subscribe(() => {
      this.notifyForChange();
    });
  }

  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

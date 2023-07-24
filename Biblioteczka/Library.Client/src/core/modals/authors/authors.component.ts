import { Component, OnInit } from '@angular/core';
import { AuthorDto } from 'src/app/interfaces/AuthorDto';
import { AuthorService } from 'src/app/services/AuthorService';
import { DataService } from 'src/app/services/data.service';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {

  authors: AuthorDto[] = [];

  constructor(private authorService: AuthorService, private dataService: DataService) { }

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  ngOnInit(): void {
    this.authorService.getAuthors().subscribe((resp) => {
      this.authors = resp;
    })
  }

  deleteAuthor(author: AuthorDto) {
    Swal.fire({
      title: 'Czy na pewno chcesz usunąć tego autora?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Usuń',
    }).then((result) => {
      if (result.isConfirmed) {
        this.authorService.deleteAuthor(author.id!).subscribe(() => {
          this.ngOnInit()
        });
      }
    }
    );
   
  }

}

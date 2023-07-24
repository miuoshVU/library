import { Component, OnInit } from '@angular/core';
import { faLink, faPencil, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ProposedBooksDto } from 'src/app/interfaces/ProposedBooksDto';
import { Subscription } from 'rxjs';
import { ProposedBookService } from 'src/app/services/ProposedBookService';
import { DataService } from 'src/app/services/data.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown'
import { UpdateProposedBookDto } from 'src/app/interfaces/UpdateProposedBookDto';
import Swal from 'sweetalert2';

declare var $: any;

@Component({
  selector: 'app-proposed-books',
  templateUrl: './proposed-books.component.html',
  styleUrls: ['./proposed-books.component.css']
})
export class ProposedBooksComponent implements OnInit {

  faPencil = faPencil;
  faTrash = faTrash;
  faLink = faLink;
  proposedBooks: ProposedBooksDto[] = [];

  notifierSubscription: Subscription = this.dataService.subjectNotifier.subscribe(notified => {
    this.ngOnInit();
  });

  constructor(private proposedBookService: ProposedBookService, private dataService: DataService) { }

  ngOnInit(): void {
    this.proposedBookService.getProposedBooks().subscribe((resp) => {
      this.proposedBooks = resp;
    })
  }

  openConfirmationModal(){
    $("#confirmationModal").modal("show");
    // document.getElementById('confirmationModal')!.style.background = "red";
  }

  deleteProposedBook(bookId: number) {
    Swal.fire({
      title: 'Czy na pewno chcesz usunąć tę książkę?',
      showCancelButton: true,
      confirmButtonColor: 'rgba(55, 168, 60)',
      cancelButtonText: "Anuluj",
      confirmButtonText: 'Usuń',
    }).then((result) => {
      if (result.isConfirmed) {
        this.proposedBookService.deleteProposedBook(bookId).subscribe(() => {
          this.proposedBookService.getProposedBooks().subscribe((resp) => {
            this.proposedBooks = resp;
          })
        })
      }
    }
    );
  }

  editProposedBook(proposedBook1: ProposedBooksDto) {
    let updateProposedBook: UpdateProposedBookDto = {
      id: proposedBook1.id,
      title: proposedBook1.title,
      urlLink: proposedBook1.urlLink,
      points: proposedBook1.points,
      authors: proposedBook1.authors,
      categories: proposedBook1.categories
    }
    this.proposedBookService.editProposedBook(updateProposedBook).subscribe(()=> {
        this.proposedBookService.getProposedBooks().subscribe((resp) => {
          this.proposedBooks = resp;
        })
    })
  }
  

}

import { Component, OnInit } from '@angular/core';

import { ViewportScroller } from '@angular/common';
import { LibraryService } from 'src/app/services/LibraryService';
import Swal from 'sweetalert2';
import { CookieService } from 'ngx-cookie-service';
import { DataService } from 'src/app/services/data.service';

declare var bootstrap: any;
declare var $: any;

@Component({
  selector: 'app-scanner',
  templateUrl: './scanner.component.html',
  styleUrls: ['./scanner.component.css']
})
export class ScannerComponent implements OnInit {

  scanResult: any = "";
  display: boolean = false;
  role: string | null = localStorage.getItem('role');
  userId: string = this.cookieService.get('userId');
  status: string = ''
  count: number = 0;
  bookQRCode: string = '';
  spotQRCode: string = '';

  constructor(private libraryService: LibraryService, private dataService: DataService, private viewportScroller: ViewportScroller, private cookieService: CookieService) { }

  onCodeResult(result: string) {
    this.display = false;
    this.scanResult = result;
    if (this.status === 'borrow') {
      $('#scanner').modal('hide');
      $('.modal-backdrop').remove();
      // document.querySelector('.modal-open')!.removeAttribute('style');
      // document.querySelector('.modal-open')!.classList.remove('modal-open');
      this.libraryService.borrowBook(result, this.userId).subscribe(() => {
        this.notifyForChange();
      });
      Swal.fire({
        icon: 'success',
        title: 'Gratulacje!',
        text: 'Udało Ci się wypożyczyć książkę!',
        confirmButtonColor: 'rgba(55, 168, 60)',
        backdrop: `
        rgb(67, 67, 67, 0.8)
          url("https://sweetalert2.github.io/images/nyan-cat.gif")
          left top
          no-repeat
        `
      })      
      // Swal.fire({
      //   icon: 'success',
      //   title: 'Sukces!',
      //   text: 'Udało Ci się wypożyczyć książkę!',
      //   confirmButtonColor: 'rgba(55, 168, 60)'
      // }
      // )
    }
    else {
      if (this.status === 'return' && this.count === 0) {
        this.bookQRCode = result;
        this.hideToastInfo();
        Swal.fire( {
          icon: 'success',
          title: 'Sukces!',
          text: 'Udało Ci się zeskanować książkę!',
          confirmButtonColor: 'rgba(55, 168, 60)'
        }
        ).then((result) => {
          if (result.dismiss || result.isConfirmed) {
            this.display = true;
            this.count += 1
            this.showToastInfo1();
          }
        });
      }
      else if (this.status === 'return' && this.count === 1) {
        this.spotQRCode = result;
        this.hideToastInfo1();
        Swal.fire({
          icon: 'success',
          title: 'Sukces!',
          text: 'Udało Ci się zeskanować miejsce!',
          confirmButtonColor: 'rgba(55, 168, 60)'
        }
        ).then((result) => {
          if (result.dismiss || result.isConfirmed) {
            $('#scanner').hide();
            $('.modal-backdrop').remove();
            document.querySelector('.modal-open')!.removeAttribute('style');
            document.querySelector('.modal-open')!.classList.remove('modal-open');
            this.libraryService.returnBook(this.userId, this.bookQRCode, this.spotQRCode).subscribe(() => {
              this.notifyForChange();
            });
            Swal.fire({
              icon: 'success',
              title: 'Gratulacje!',
              text: 'Udało Ci się zwrócić książkę!',
              confirmButtonColor: 'rgba(55, 168, 60)',
              backdrop: `
              rgb(67, 67, 67, 0.8)
                url("https://sweetalert2.github.io/images/nyan-cat.gif")
                left top
                no-repeat
              `
            })  
          }
        });
      }
    }
  }

  showToastInfo() {
    const toastLiveExample = document.getElementById('liveToastInfo');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.show()
  }

  showToastInfo1() {
    const toastLiveExample = document.getElementById('liveToastInfo1');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.show()
  }

  hideToastInfo() {
    const toastLiveExample = document.getElementById('liveToastInfo');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.hide()
  }

  hideToastInfo1() {
    const toastLiveExample = document.getElementById('liveToastInfo1');
    const toast = new bootstrap.Toast(toastLiveExample);
    toast.hide()
  }

  public onClick(elementId: string): void {
    this.viewportScroller.scrollToAnchor(elementId);
  }

  ngOnInit(): void {
    const myModalEl = document.getElementById('scanner') as HTMLElement;
    myModalEl.addEventListener('show.bs.modal', event => {
      this.bookQRCode = '';
      this.spotQRCode = '';
      this.count = 0;
      Swal.fire({
        title: 'Wybierz jedną z opcji',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonColor: 'rgba(55, 168, 60)',
        cancelButtonText: "Zamknij",
        confirmButtonText: 'Wypożycz',
        denyButtonText: `Zwróć`,
      }).then((result) => {
        // Wypozycz
        if (result.isConfirmed) {
          this.showToastInfo();
          this.display = true;
          this.status = 'borrow';
          // Zwróć
        } else if (result.isDenied) {
          this.showToastInfo();
          this.display = true;
          this.status = 'return'
        }
        // Anuluj
        else {
          this.display = false;
          $('#scanner').hide();
          $('.modal-backdrop').remove();
          document.querySelector('.modal-open')!.removeAttribute('style');
          document.querySelector('.modal-open')!.classList.remove('modal-open');
        }
      })
    })
    myModalEl.addEventListener('hide.bs.modal', event => {
      const toastLiveInfo = document.getElementById('liveToastInfo');
      const toastInfo = new bootstrap.Toast(toastLiveInfo);
      toastInfo.hide();
      this.display = false;
    })
  }
  notifyForChange() {
    this.dataService.notifyAboutChange();
  }

}

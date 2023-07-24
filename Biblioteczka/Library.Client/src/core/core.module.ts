import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavbarTopComponent } from './navbar-top/navbar-top.component';
import { ModalsComponent } from './modals/modals.component';
import { AllCategoriesComponent } from './modals/all-categories/all-categories.component';
import { LogsModalComponent } from './modals/logs-modal/logs-modal.component';
import { AuthorsComponent } from './modals/authors/authors.component';
import { SpotsComponent } from './modals/spots/spots.component';
import { AddAuthorComponent } from './modals/add-author/add-author.component';
import { AddSpotComponent } from './modals/add-spot/add-spot.component';
import { EditAuthorComponent } from './modals/edit-author/edit-author.component';
import { AddCategoryComponent } from './modals/add-category/add-category.component';
import { AddBookComponent } from './modals/add-book/add-book.component';
import { EditCategoryComponent } from './modals/edit-category/edit-category.component';
import { EditSpotComponent } from './modals/edit-spot/edit-spot.component';
import { ZXingScannerComponent, ZXingScannerModule } from '@zxing/ngx-scanner';
import { ScannerComponent } from './scanner/scanner.component';
import { BooksComponent } from './modals/books/books.component';
import { UsersComponent } from './modals/users/users.component';
import { AddUserComponent } from './modals/add-user/add-user.component';
import { NgMultiSelectDropDownModule } from "ng-multiselect-dropdown";
import { AddBookInstanceComponent } from './modals/add-book-instance/add-book-instance.component';
import { AddProposedBookComponent } from './modals/add-proposed-book/add-proposed-book.component';

@NgModule({
  declarations: [
    FooterComponent,
    PageNotFoundComponent,
    LoginComponent,
    NavbarComponent,
    NavbarTopComponent,
    ModalsComponent,
    AllCategoriesComponent,
    LogsModalComponent,
    AuthorsComponent,
    SpotsComponent,
    AddAuthorComponent,
    AddSpotComponent,
    EditAuthorComponent,
    AddCategoryComponent,
    EditCategoryComponent,
    AddBookComponent,
    EditCategoryComponent,
    EditSpotComponent,
    ScannerComponent,
    BooksComponent,
    UsersComponent,
    AddUserComponent,
    AddBookInstanceComponent,
    AddProposedBookComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    AddSpotComponent,
    EditAuthorComponent,
    AddBookComponent,
    EditSpotComponent,
    AddCategoryComponent,
    AddAuthorComponent,
    NavbarTopComponent,
    AuthorsComponent,
    SpotsComponent,
    LogsModalComponent,
    ModalsComponent,
    ZXingScannerComponent,
    AllCategoriesComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgMultiSelectDropDownModule,
    FormsModule,
    HttpClientModule,
    ZXingScannerModule,
    FontAwesomeModule
  ],
  providers: [
  ]
})

export class CoreModule {
}


export function InitApp(): () => Promise<any> {
  return async () => {
  };
}

export function getBaseHref(): string {
  return document.getElementsByTagName('base')[0].href;
}

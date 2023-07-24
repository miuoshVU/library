import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from "./app-routing.module";
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CoreModule } from "src/core/core.module";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HomePageComponent } from './user/components/home-page/home-page.component';
import { AccountSettingsComponent } from '../core/account-settings/account-settings.component';
import { VotingComponent } from './user/components/voting/voting.component';
import { AdminHomePageComponent } from './admin/components/admin-home-page/admin-home-page.component';
import { ProposedBooksComponent } from './admin/components/proposed-books/proposed-books.component';
import { LibraryService } from './services/LibraryService';
import { UserService } from './services/UserService';
import { CategoryService } from './services/CategoryService';
import { AuthenticationService } from './services/AuthenticationService';
import { AuthorService } from './services/AuthorService';
import { BookInstanceService } from './services/BookInstanceService'
import { SpotService } from './services/SpotService';
import { DataService } from './services/data.service';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { ProposedBookService } from './services/ProposedBookService';
import { JwtInterceptor } from './services/JwtInterceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './guards/AuthGuard';
import { CookieService } from 'ngx-cookie-service';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    AccountSettingsComponent,
    VotingComponent,
    AdminHomePageComponent,
    ProposedBooksComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgMultiSelectDropDownModule,
    FormsModule,
    FontAwesomeModule,
    CoreModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter
      }
    })
    ],
  providers: [LibraryService, UserService, CategoryService, AuthenticationService,
  AuthorService, BookInstanceService, SpotService, DataService, ProposedBookService, AuthGuard,
  { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }, CookieService
],
  bootstrap: [AppComponent]
})
export class AppModule { }

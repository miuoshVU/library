import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './user/components/home-page/home-page.component';
import { LoginComponent } from 'src/core/login/login.component';
import { PageNotFoundComponent } from 'src/core/page-not-found/page-not-found.component';
import { AccountSettingsComponent } from '../core/account-settings/account-settings.component';
import { VotingComponent } from './user/components/voting/voting.component';
import { AdminHomePageComponent } from './admin/components/admin-home-page/admin-home-page.component';
import { ProposedBooksComponent } from './admin/components/proposed-books/proposed-books.component';
import { AuthGuard } from './guards/AuthGuard';


const routes: Routes = [
  { path: '', component: LoginComponent, title: "Login" },
  {
    path: 'home', component: HomePageComponent, title: "Home", canActivate: [AuthGuard], data:
      ['user']
  },
  {
    path: 'userSettings', component: AccountSettingsComponent, title: "User settings", canActivate: [AuthGuard], data:
      ['user, admin']
  },
  {
    path: 'voting', component: VotingComponent, title: "Voting", canActivate: [AuthGuard], data:
      ['user']
  },
  {
    path: 'adminHomePage', component: AdminHomePageComponent, title: "Home Page", canActivate: [AuthGuard], data: ['admin']
  },
  {
    path: 'proposedBooks', component: ProposedBooksComponent, title: "Proposed books", canActivate: [AuthGuard], data:
      ['admin']
  },
  { path: '**', component: PageNotFoundComponent, title: "404" },

];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes, { anchorScrolling: 'enabled', scrollPositionRestoration: 'enabled', useHash: true }),
    CommonModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

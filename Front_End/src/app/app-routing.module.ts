import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component'; 
import { authGuard } from './guards/auth-guard/auth.guard';
import { loginGuardGuard } from './guards/login-guard/login-guard.guard';
import { AdminDashboardComponent } from './pages/admin/admin-dashboard/admin-dashboard/admin-dashboard.component';
import { AddBookComponent } from './pages/admin/add-book/add-book/add-book.component';
import { adminGuard } from './guards/admin-guard/admin.guard';
import { BookDetailsComponent } from './pages/book-details/book-details.component';

const routes: Routes = [
  {
    path: "register",
    component: RegisterComponent,
    canActivate: [loginGuardGuard]
  },
  {
    path: "login",
    component: LoginComponent,
    canActivate: [loginGuardGuard]
  },
  {
    path: "",
    component: HomeComponent,
    canActivate: [authGuard]
  },
  {
    path: "dashboard",
    component: AdminDashboardComponent,
    canActivate: [adminGuard]
  },
  {
    path: "add-book",
    component: AddBookComponent,
    canActivate: [adminGuard]
  },
  {
    path: "add-book/add-book",
    redirectTo: "add-book"
  },
  {
    path: "add-book/dashboard",
    redirectTo: "dashboard"

  },
  {
    path: "dashboard/add-book",
    redirectTo: "add-book"
  },
  {
    path: 'book-details/:id',
    component : BookDetailsComponent
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

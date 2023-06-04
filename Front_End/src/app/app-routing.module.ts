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
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

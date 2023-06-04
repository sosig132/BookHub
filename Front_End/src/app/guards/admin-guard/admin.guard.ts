import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

export const adminGuard: CanActivateFn = (route, state) => {
  const role = inject(JwtHelperService).decodeToken().role;
  const router = inject(Router);
  if (role !== 'Admin') {
    return router.navigate(['']);
  }
  return true;
};

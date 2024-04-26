import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../Services/user.service';
export const authGuard: CanActivateFn = (route, state) => {
  const _userService = inject(UserService);
  const _route = inject(Router); 
  if(_userService.isAuthenticated())
  {
  return true;
  }
  else{
    localStorage.setItem('redirectUrl', state.url);
    _route.navigate(['/login']);
      return false;
  }
};

import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'lesson',
    loadChildren: () => import('./lessonView/tabs/tabs.routes').then((m) => m.routes),
  },
  {
    path: '',
    loadComponent: () => import('./login/login.page').then((m) => m.LoginPage)
  },
  {
    path: 'home',
    loadComponent: () => import('./home/home.page').then( m => m.HomePage)
  },
];

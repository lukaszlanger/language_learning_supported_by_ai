import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./login/login.page').then((m) => m.LoginPage)
  },
  {
    path: 'lessons',
    loadComponent: () => import('./lessons/lessons.page').then(m => m.LessonsPage)
  },
  {
    path: 'lesson/:id',
    loadChildren: () => import('./lesson/tabs/tabs.routes').then((m) => m.routes),
  },
  {
    path: '**',
    redirectTo: '',
  },
];

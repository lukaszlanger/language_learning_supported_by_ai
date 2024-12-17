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
    path: 'lessons',
    loadComponent: () => import('./lessons/lessons.page').then(m => m.LessonsPage)
  },
];

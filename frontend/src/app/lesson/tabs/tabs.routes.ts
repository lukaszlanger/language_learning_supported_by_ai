import { Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

export const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
      {
        path: '',
        redirectTo: 'flashcards',
        pathMatch: 'full',
      },
      {
        path: 'flashcards',
        loadComponent: () =>
          import('../flashcard/flashcard.page').then((m) => m.FlashcardPage),
      },
      {
        path: 'quiz',
        loadComponent: () =>
          import('../quiz/quiz.page').then((m) => m.QuizPage),
      },
      {
        path: 'stats',
        loadComponent: () =>
          import('../tab3/tab3.page').then((m) => m.Tab3Page),
      },
    ],
  },
];

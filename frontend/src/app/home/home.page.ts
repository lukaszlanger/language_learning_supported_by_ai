import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonIcon, IonHeader, IonTitle, IonToolbar } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { arrowForward } from 'ionicons/icons';
import { IonicModule } from '@ionic/angular';
import { ToolbarComponent } from '../toolbar/toolbar.component';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: true,
  imports: [IonContent, IonHeader, IonTitle, IonToolbar, CommonModule, FormsModule, IonicModule, ToolbarComponent, IonIcon]
})
export class HomePage{
  lessons = [
    { id: 1, title: 'Podstawy Angielskiego', quizzes: 5, flashcards: 20 },
    { id: 2, title: 'Czasowniki nieregularne', quizzes: 8, flashcards: 15 },
    { id: 3, title: 'Wyrażenia codzienne', quizzes: 4, flashcards: 12 },
    { id: 4, title: 'Lekcja testowa', quizzes: 5, flashcards: 12 },
  ];

  constructor(
    private router: Router, 
    private authService: AuthService) {
    addIcons({ arrowForward });
  }

  navigateToLesson() {
    this.router.navigate(['/lesson']);
  }
}

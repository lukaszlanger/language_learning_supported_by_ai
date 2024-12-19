import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonIcon, IonHeader, IonTitle, IonToolbar, IonSpinner } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { arrowForward, close } from 'ionicons/icons';
import { IonicModule } from '@ionic/angular';
import { ToolbarComponent } from '../toolbar/toolbar.component';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { LessonService } from '../services/lesson.service';
import { LessonDto } from '../dtos/lesson.dto';

@Component({
  selector: 'app-lessons',
  templateUrl: './lessons.page.html',
  styleUrls: ['./lessons.page.scss'],
  imports: [IonContent, IonHeader, IonTitle, IonToolbar, CommonModule, FormsModule, IonicModule, ToolbarComponent, IonIcon, IonSpinner]
})
export class LessonsPage implements OnInit {
  lessons: LessonDto[] = [];
  loading: boolean = false;
  errorMessage: string = '';
  isModalOpen = false;

  constructor(
    private router: Router, 
    private authService: AuthService,
    private lessonService: LessonService) {
    addIcons({ arrowForward, close });
  }

  ngOnInit(): void {
    this.loading = true;
    const userId = this.authService.user?.id;
  
    if (!userId) {
      this.errorMessage = 'Nie znaleziono ID użytkownika.';
      return;
    }
  
    this.lessonService.getAllByUserId(userId).subscribe({
      next: (lessons) => {
        this.lessons = lessons;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Wystąpił błąd podczas pobierania lekcji.';
        this.loading = false;
      }
    });
  }
  
  navigateToLesson(lessonId: number) {
    this.router.navigate(['/lesson', lessonId]).catch((err) => {
      console.error('Błąd podczas nawigacji:', err);
    });
  }

  openModal() {
    this.isModalOpen = true;
  }
  
  closeModal() {
    this.isModalOpen = false;
  }
}

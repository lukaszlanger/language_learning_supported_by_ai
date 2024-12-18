import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonIcon, IonHeader, IonTitle, IonToolbar } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { arrowForward } from 'ionicons/icons';
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
  standalone: true,
  imports: [IonContent, IonHeader, IonTitle, IonToolbar, CommonModule, FormsModule, IonicModule, ToolbarComponent, IonIcon]
})
export class LessonsPage implements OnInit {
  lessons: LessonDto[] = [];

  constructor(
    private router: Router, 
    private authService: AuthService,
    private lessonService: LessonService) {
    addIcons({ arrowForward });
  }

  ngOnInit(): void {
    const userId = this.authService.user?.id;
  
    if (!userId) {
      console.error('Nie znaleziono ID użytkownika.');
      return;
    }
  
    this.lessonService.getAllByUserId(userId).subscribe({
      next: (lessons) => {
        this.lessons = lessons;
      },
      error: (err) => {
        console.error('Błąd podczas pobierania lekcji dla użytkownika:', err);
      }
    });
  }
  
  navigateToLesson(lessonId: number) {
    this.router.navigate(['/lesson', lessonId]).catch((err) => {
      console.error('Błąd podczas nawigacji:', err);
    });
  }
}

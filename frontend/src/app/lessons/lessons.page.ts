import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonContent, IonIcon, IonHeader, IonTitle, IonToolbar, IonSpinner, IonSelect } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { arrowForward, close } from 'ionicons/icons';
import { IonicModule } from '@ionic/angular';
import { ToolbarComponent } from '../toolbar/toolbar.component';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { LessonService } from '../services/lesson.service';
import { LessonDto } from '../dtos/lesson.dto';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-lessons',
  templateUrl: './lessons.page.html',
  styleUrls: ['./lessons.page.scss'],
  imports: [IonContent, IonHeader, IonTitle, IonToolbar, CommonModule, FormsModule, IonicModule, ToolbarComponent, IonIcon, IonSpinner, FormsModule, ReactiveFormsModule, IonSelect]
})
export class LessonsPage implements OnInit {
  lessons: LessonDto[] = [];
  isLoading: boolean = false;
  errorMessage: string = '';
  isModalOpen = false;
  lessonForm: FormGroup;
  availableLanguages: string[] = ['English', 'Polish', 'German', 'French', 'Spanish', 'Italian'];
  languageFlags: { [key: string]: string } = {
    English: '🇬🇧',
    Polish: '🇵🇱',
    German: '🇩🇪',
    French: '🇫🇷',
    Spanish: '🇪🇸',
    Italian: '🇮🇹'
  };

  constructor(
    private router: Router, 
    private authService: AuthService,
    private lessonService: LessonService
  ) {
    addIcons({ arrowForward, close });
    this.lessonForm = new FormGroup({
      topic: new FormControl('', [Validators.required]),
      difficultyLevel: new FormControl(null, [Validators.required]),
      learningLanguage: new FormControl(null, [Validators.required])
    });
  }

  ngOnInit(): void {
    this.isLoading = true;
    const userId = this.authService.user?.id;
  
    if (!userId) {
      this.errorMessage = 'Nie znaleziono ID użytkownika.';
      return;
    }

    this.loadLessons();
  }

  loadLessons(): void {
    if(this.authService.user?.id) {
      this.lessonService.getAllByUserId(this.authService.user?.id).subscribe({
        next: (lessons) => {
          this.lessons = lessons;
          this.isLoading = false;
        },
        error: (err) => {
          this.errorMessage = 'Wystąpił błąd podczas pobierania lekcji.';
          this.isLoading = false;
        }
      });
    }
  }

  async submitLesson() {
    if (this.lessonForm.valid) {
      const currentUser = this.authService.user;
      const lessonData: LessonDto = {
        ...this.lessonForm.value,
        userId: currentUser?.id
      };

      try {
        await firstValueFrom(this.lessonService.createLesson(lessonData));
        this.closeModal();
        this.loadLessons();
      } catch (error) {
        console.error('Error occured while adding lesson:', error);
      }
    }
  }
  
  navigateToLesson(lessonId: number) {
    this.router.navigate(['/lesson', lessonId]).catch((err) => {
      console.error('Error occured during route navigation:', err);
    });
  }

  openModal() {
    this.isModalOpen = true;
  }
  
  closeModal() {
    this.isModalOpen = false;
  }

  getFlagForLanguage(language: string | undefined): string {
    if (!language || !this.languageFlags[language]) {
      return '🏳️';
    }
    return this.languageFlags[language];
  }  
}

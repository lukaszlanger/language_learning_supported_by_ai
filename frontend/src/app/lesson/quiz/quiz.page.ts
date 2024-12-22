import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonHeader, IonToolbar, IonTitle, IonContent, IonIcon, IonModal, IonSpinner, IonSelect } from '@ionic/angular/standalone';
import { ToolbarComponent } from 'src/app/toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { ActivatedRoute } from '@angular/router';
import { QuizService } from 'src/app/services/quiz.service';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
    selector: 'app-quiz',
    templateUrl: 'quiz.page.html',
    styleUrls: ['quiz.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal, IonSpinner, FormsModule, ReactiveFormsModule, IonSelect]
})
export class QuizPage implements OnInit {
  title: string = 'Lekcja';
  smallTitle: string = 'Fiszki';
  isLoading: boolean = false;
  errorMessage: string = '';
  lessonId: number | null = null;
  quizzes: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private quizService: QuizService,
    private lessonService: LessonService) {}

  ngOnInit() {
    this.isLoading = true;
    this.lessonId = this.getRouteParam('id');
    if (this.lessonId) {
      this.loadQuizzes(this.lessonId);
      this.lessonService.getById(this.lessonId).subscribe({
        next: (lesson) => {
          this.title = lesson.topic!;
          this.smallTitle = lesson.learningLanguage!;
        },
        error: (err) => {
          this.errorMessage = 'Nie znaleziono lekcji.';
        },
      });
    }
  }

  loadQuizzes(lessonId: number): void {
    this.quizService.getAllByLessonId(lessonId).subscribe({
      next: (quizzes) => {
        this.quizzes = quizzes;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Nie znaleziono quizów dla tej lekcji.';
        this.isLoading = false;
      },
    });
  }

  private getRouteParam(param: string): number | null {
    let route = this.route.root;
    while (route.firstChild) {
      route = route.firstChild;
      const paramValue = route.snapshot.paramMap.get(param);
      if (paramValue) {
        return Number(paramValue);
      }
    }
    return null;
  }

}

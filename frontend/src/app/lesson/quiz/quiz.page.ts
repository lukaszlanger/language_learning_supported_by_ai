import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonHeader, IonToolbar, IonTitle, IonContent, IonIcon, IonModal, IonSpinner, IonSelect } from '@ionic/angular/standalone';
import { ToolbarComponent } from 'src/app/toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { ActivatedRoute } from '@angular/router';
import { QuizService } from 'src/app/services/quiz.service';
import { LessonService } from 'src/app/services/lesson.service';
import { QuizDto } from 'src/app/dtos/quiz.dto';

@Component({
    selector: 'app-quiz',
    templateUrl: 'quiz.page.html',
    styleUrls: ['quiz.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal, IonSpinner, FormsModule, ReactiveFormsModule, IonSelect]
})
export class QuizPage implements OnInit {
  title: string = 'Lekcja';
  smallTitle: string = 'Quiz';
  isLoading: boolean = false;
  errorMessage: string = '';
  lessonId: number | null = null;
  quizzes: QuizDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private quizService: QuizService,
    private lessonService: LessonService) {}

  ngOnInit() {
    this.isLoading = true;
    this.isLoading = false;
    // this.lessonId = this.getRouteParam('id');
    // if (this.lessonId) {
    //   this.loadQuizzes(this.lessonId);
    //   this.lessonService.getById(this.lessonId).subscribe({
    //     next: (lesson) => {
    //       this.title = lesson.topic!;
    //       this.smallTitle = lesson.learningLanguage!;
    //     },
    //     error: (err) => {
    //       this.errorMessage = 'Błąd podczas pobierania lekcji.';
    //     },
    //   });
    // }
  }

  loadQuizzes(lessonId: number): void {
    this.quizzes = [
      {
        id: 1,
        lessonId: lessonId,
        quizQuestions: [
          {
            id: 1,
            quizId: 1,
            question: 'What is the capital of France?',
            answers: ['Paris', 'London', 'Berlin', 'Madrid'],
            correctAnswer: 'Paris',
            userAnswer: undefined,
            isCorrect: undefined
          },
          {
            id: 2,
            quizId: 1,
            question: 'What is 2 + 2?',
            answers: ['3', '4', '5', '6'],
            correctAnswer: '4',
            userAnswer: undefined,
            isCorrect: undefined
          },
          {
            id: 3,
            quizId: 1,
            question: 'What is the largest planet in our solar system?',
            answers: ['Earth', 'Mars', 'Jupiter', 'Saturn'],
            correctAnswer: 'Jupiter',
            userAnswer: undefined,
            isCorrect: undefined
          }
        ]
      },
      {
        id: 2,
        lessonId: lessonId,
        quizQuestions: [
          {
            id: 4,
            quizId: 2,
            question: 'What is the capital of Germany?',
            answers: ['Paris', 'London', 'Berlin', 'Madrid'],
            correctAnswer: 'Berlin',
            userAnswer: undefined,
            isCorrect: undefined
          },
          {
            id: 5,
            quizId: 2,
            question: 'What is 3 + 3?',
            answers: ['5', '6', '7', '8'],
            correctAnswer: '6',
            userAnswer: undefined,
            isCorrect: undefined
          },
          {
            id: 6,
            quizId: 2,
            question: 'What is the smallest planet in our solar system?',
            answers: ['Earth', 'Mars', 'Mercury', 'Venus'],
            correctAnswer: 'Mercury',
            userAnswer: undefined,
            isCorrect: undefined
          }
        ]
      }
    ];
    this.isLoading = false;


    // this.quizService.getAllByLessonId(lessonId).subscribe({
    //   next: (quizzes) => {
    //     this.quizzes = quizzes;
    //     this.isLoading = false;
    //   },
    //   error: (err) => {
    //     this.errorMessage = 'Nie znaleziono quizów dla tej lekcji.';
    //     this.isLoading = false;
    //   },
    // });
  }

  private getRouteParam(param: string): number | null {
    let paramValue = this.route.root.firstChild?.snapshot.paramMap.get(param);
    return paramValue ? Number(paramValue) : null;
  }

}

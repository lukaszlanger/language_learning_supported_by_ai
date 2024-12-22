import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonHeader, IonToolbar, IonTitle, IonContent, IonIcon, IonModal, IonSpinner, IonSelect, IonSegment, IonSegmentButton, IonProgressBar, IonRadio, IonList } from '@ionic/angular/standalone';
import { ToolbarComponent } from 'src/app/toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { ActivatedRoute } from '@angular/router';
import { QuizService } from 'src/app/services/quiz.service';
import { LessonService } from 'src/app/services/lesson.service';
import { QuizDto } from 'src/app/dtos/quiz.dto';
import { addIcons } from 'ionicons';
import { arrowBack, arrowForward, barbell, basket, browsers, call, globe, heart, helpCircleOutline, home, person, pin, star, trash } from 'ionicons/icons';
import { QuizQuestionDto } from 'src/app/dtos/quiz-question.dto';

@Component({
    selector: 'app-quiz',
    templateUrl: 'quiz.page.html',
    styleUrls: ['quiz.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal, IonSpinner, FormsModule, ReactiveFormsModule, IonSelect, IonSegment, IonSegmentButton, IonProgressBar, IonRadio, IonList]
})
export class QuizPage implements OnInit {
  title: string = 'Lekcja';
  smallTitle: string = 'Quiz';
  isLoading: boolean = false;
  errorMessage: string = '';
  lessonId: number | null = null;
  quizzes: QuizDto[] = [];
  selectedQuizId: number | null = null;
  selectedQuiz: QuizDto | undefined;
  quizForm: FormGroup;
  selectedAnswers: { [key: number]: string } = {};
  currentQuestion: QuizQuestionDto | undefined;
  currentIndex = 0;

  get isLastQuestion(): boolean {
    return this.currentIndex === (this.selectedQuiz?.quizQuestions?.length || 0) - 1;
  }

  constructor(
    private route: ActivatedRoute,
    private quizService: QuizService,
    private lessonService: LessonService,
    private formBuilder: FormBuilder) {
      addIcons({ arrowForward, arrowBack });
      this.quizForm = this.formBuilder.group({
        answers: ['', Validators.required]
      });
    }

  ngOnInit() {
    this.isLoading = true;
    this.loadQuizzes(2);
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
    if (this.quizzes.length > 0) {
      this.selectedQuizId = this.quizzes[0].id!;
      this.selectedQuiz = this.quizzes[0];
    }
    this.updateCurrentQuestion();

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

  onSegmentChange(event: any) {
    const selectedId = event.detail.value;
    this.selectedQuiz = this.quizzes.find(quiz => quiz.id === selectedId) || undefined;
  }

  updateCurrentQuestion() {
    if (this.selectedQuiz) {
      this.currentQuestion = this.selectedQuiz?.quizQuestions?.[this.currentIndex];
      this.quizForm.controls['answers'].setValue(this.selectedAnswers[this.currentIndex] || '');
    }
  }

  selectAnswer(event: any) {
    this.selectedAnswers[this.currentIndex] = event.detail.value;
  }

  nextQuestion() {
    this.saveAnswer();
    if (this.currentIndex < (this.selectedQuiz?.quizQuestions?.length || 0) - 1) {
      this.currentIndex++;
      this.updateCurrentQuestion();
    }
  }

  previousQuestion() {
    this.saveAnswer();
    if (this.currentIndex > 0) {
      this.currentIndex--;
      this.updateCurrentQuestion();
    }
  }

  saveAnswer() {
    const currentAnswer = this.quizForm.controls['answers'].value;
    if (currentAnswer) {
      this.selectedAnswers[this.currentIndex] = currentAnswer;
    }
  }

  goToQuestion(index: number): void {
    this.saveAnswer();
    this.updateCurrentQuestion();
    this.currentIndex = index;
  }
  

  onSubmit() {
    this.saveAnswer();
    console.log('User answers:', this.selectedAnswers);
  }

}

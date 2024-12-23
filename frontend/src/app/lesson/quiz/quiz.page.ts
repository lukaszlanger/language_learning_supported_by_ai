import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonHeader, IonToolbar, IonTitle, IonContent, IonIcon, IonModal, IonSpinner, IonSelect, IonSegment, IonSegmentButton, IonProgressBar, IonRadio, IonList } from '@ionic/angular/standalone';
import { ToolbarComponent } from 'src/app/toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { ActivatedRoute } from '@angular/router';
import { QuizService } from 'src/app/services/quiz.service';
import { LessonService } from 'src/app/services/lesson.service';
import { QuizDto } from 'src/app/dtos/quiz.dto';
import { addIcons } from 'ionicons';
import { arrowBack, arrowForward, checkmarkCircle } from 'ionicons/icons';
import { QuizQuestionDto } from 'src/app/dtos/quiz-question.dto';
import { switchMap } from 'rxjs';

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
  isGeneratingQuiz: boolean = false;
  isCreateModalOpen: boolean = false;
  modalMessage: string = '';
  errorMessage: string = '';
  lessonId: number | null = null;
  quizzes: QuizDto[] = [];
  selectedQuizId: number | null = null;
  selectedQuiz: QuizDto | undefined;
  quizForm: FormGroup;
  selectedAnswers: { [key: number]: string } = {};
  currentQuestion: QuizQuestionDto | null = null;
  currentIndex = 0;

  get isLastQuestion(): boolean {
    return this.currentIndex === (this.selectedQuiz?.questions?.length || 0) - 1;
  }

  constructor(
    private route: ActivatedRoute,
    private quizService: QuizService,
    private lessonService: LessonService,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef) {
      addIcons({ arrowForward, arrowBack, checkmarkCircle });
      this.quizForm = this.formBuilder.group({
        answers: ['', Validators.required]
      });
    }

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
          this.errorMessage = 'Błąd podczas pobierania lekcji.';
        },
      });
    }
  }

  loadQuizzes(lessonId: number): void {
    this.quizService.getAllByLessonId(lessonId).subscribe({
      next: (quizzes) => {
        this.quizzes = quizzes;
        this.isLoading = false;
        if (this.quizzes.length > 0) {
          this.selectedQuiz = this.quizzes[0];
          this.selectedQuizId = this.selectedQuiz.id!;
          this.currentIndex = 0;
          this.updateCurrentQuestion();
          this.cdr.detectChanges();
        } else {
          this.errorMessage = 'Brak quizów dla tej lekcji.';
        }
      },
      error: (err) => {
        this.errorMessage = 'Nie znaleziono quizów dla tej lekcji.';
        this.isLoading = false;
      },
    });
  }

  private getRouteParam(param: string): number | null {
    let paramValue = this.route.root.firstChild?.snapshot.paramMap.get(param);
    return paramValue ? Number(paramValue) : null;
  }

  onSegmentChange(event: any) {
    const selectedId = event.detail.value;
    const selectedQuiz = this.quizzes.find(quiz => quiz.id === selectedId);
  
    if (selectedQuiz) {
      this.selectedQuizId = selectedId;
      this.selectedQuiz = selectedQuiz;
      this.currentIndex = 0;
      this.selectedAnswers = {};
      this.updateCurrentQuestion();
    }
  }  
  
  updateCurrentQuestion() {
    if (this.selectedQuiz) {
      const totalQuestions = this.selectedQuiz.questions?.length || 0;
      
      if (this.currentIndex >= totalQuestions) {
        this.currentIndex = totalQuestions - 1;
      } else if (this.currentIndex < 0) {
        this.currentIndex = 0;
      }
  
      this.currentQuestion = this.selectedQuiz.questions?.[this.currentIndex] || null;
  
      this.quizForm.reset();
      this.quizForm.controls['answers'].setValue(
        this.selectedAnswers[this.currentIndex] || ''
      );
    }
  }

  selectAnswer(event: any) {
    this.selectedAnswers[this.currentIndex] = event.detail.value;
  }

  nextQuestion() {
    this.saveAnswer();
  
    if (this.selectedQuiz && this.currentIndex < (this.selectedQuiz.questions?.length || 0) - 1) {
      this.currentIndex++;
      this.updateCurrentQuestion();
    }
  }  

  previousQuestion() {
    this.saveAnswer();
  
    if (this.selectedQuiz && this.currentIndex > 0) {
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
  
    if (this.selectedQuiz && index >= 0 && index < (this.selectedQuiz.questions?.length || 0)) {
      this.currentIndex = index;
      this.updateCurrentQuestion();
    } else {
      console.error('Indeks pytania poza zakresem:', index);
    }
  }  

  onSubmit() {
    this.saveAnswer();
    console.log('User answers:', this.selectedAnswers);
  }

  generateQuizWithAI() {
    if (this.lessonId) {
      this.isCreateModalOpen = true;
      this.isGeneratingQuiz = true;
      this.modalMessage = 'Generuję nowy quiz...';
  
      this.lessonService.getById(this.lessonId).pipe(
        switchMap(lesson => {
          return this.quizService.generateQuizWithAI(this.lessonId!);
        })
      ).subscribe({
        next: () => {
          this.modalMessage = 'Stworzono nowy quiz!';
          this.errorMessage = '';
          this.loadQuizzes(this.lessonId!);
        },
        error: (err) => {
          console.error('Błąd podczas generowania quizu:', err);
          this.modalMessage = 'Nie udało się stworzyć nowego quizu.';
        },
        complete: () => {
          this.isGeneratingQuiz = false;
          setTimeout(() => {
            this.isCreateModalOpen = false;
          }, 2000);
        }
      });
    }
  }
}

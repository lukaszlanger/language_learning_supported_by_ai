import { Component, OnInit } from '@angular/core';
import { IonIcon, IonHeader, IonToolbar, IonTitle, IonContent, IonModal, IonSpinner, IonSelect } from '@ionic/angular/standalone';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from '../../toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { bookmark, chatbubbles, informationCircle, close, language, colorWandOutline } from 'ionicons/icons';
import { ActivatedRoute } from '@angular/router';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { FlashcardDto } from 'src/app/dtos/flashcard.dto';
import { LessonService } from 'src/app/services/lesson.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
    selector: 'app-flashcard',
    templateUrl: 'flashcard.page.html',
    styleUrls: ['flashcard.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal, IonSpinner, FormsModule, ReactiveFormsModule, IonSelect]
})
export class FlashcardPage implements OnInit {
  title: string = 'Lekcja';
  smallTitle: string = 'Fiszki';
  isFlashcardModalOpen = false;
  isCreateModalOpen = false;
  selectedFlashcard: FlashcardDto | null = null;
  flashcards: FlashcardDto[] = [];
  lessonId: number | null = null;
  loading: boolean = false;
  errorMessage: string = '';
  flashcardForm: FormGroup;

  constructor(
    private flashcardService: FlashcardService,
    private lessonService: LessonService,
    private route: ActivatedRoute,
  ) {
    addIcons({ bookmark, close, informationCircle, chatbubbles, language, colorWandOutline });
    this.flashcardForm = new FormGroup({
      term: new FormControl('', [Validators.required]),
      translation: new FormControl(''),
      details: new FormControl(''),
      usage: new FormControl(''),
      });
  }

  ngOnInit() {
    this.loading = true;
    this.lessonId = this.getRouteParam('id');
    if (this.lessonId) {
      this.loadFlashcards(this.lessonId);
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

  loadFlashcards(lessonId: number): void {
    this.flashcardService.getAllByLessonId(lessonId).subscribe({
      next: (flashcards) => {
        this.flashcards = flashcards;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Nie znaleziono fiszek dla tej lekcji.';
        this.loading = false;
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

  openFlashcardModal(flashcard: FlashcardDto) {
    this.selectedFlashcard = flashcard;
    this.isFlashcardModalOpen = true;
  }
  
  closeFlashcardModal() {
    this.isFlashcardModalOpen = false;
    this.selectedFlashcard = null;
  }

  openCreateModal() {
    this.isCreateModalOpen = true;
  }
  
  closeCreateModal() {
    this.isCreateModalOpen = false;
    this.selectedFlashcard = null;
  }

  submitFlashcard() {
    if (this.flashcardForm.valid) {
      const flashcardData: FlashcardDto = {
        ...this.flashcardForm.value,
        lessonId: this.lessonId,
      };
      this.flashcardService.createFlashcard(flashcardData).subscribe({
        next: () => {
          this.loadFlashcards(this.lessonId!);
          this.closeCreateModal();
        },
        error: (err) => {
          console.error('Błąd podczas dodawania fiszki:', err);
        },
      });
    }
  }
}

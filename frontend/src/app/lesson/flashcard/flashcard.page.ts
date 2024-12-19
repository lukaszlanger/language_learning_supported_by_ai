import { Component, OnInit } from '@angular/core';
import { IonIcon, IonHeader, IonToolbar, IonTitle, IonContent, IonModal, IonSpinner } from '@ionic/angular/standalone';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from '../../toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { bookmark, chatbubbles, informationCircle, close, language } from 'ionicons/icons';
import { ActivatedRoute } from '@angular/router';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { FlashcardDto } from 'src/app/dtos/flashcard.dto';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
    selector: 'app-flashcard',
    templateUrl: 'flashcard.page.html',
    styleUrls: ['flashcard.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal, IonSpinner]
})
export class FlashcardPage implements OnInit {
  title: string = 'Lekcja';
  isModalOpen = false;
  selectedFlashcard: FlashcardDto | null = null;
  flashcards: FlashcardDto[] = [];
  lessonId: number | null = null;
  loading: boolean = false;
  errorMessage: string = '';

  constructor(
    private flashcardService: FlashcardService,
    private lessonService: LessonService,
    private route: ActivatedRoute,
  ) {
    addIcons({ bookmark, close, informationCircle, chatbubbles, language});
  }

  ngOnInit() {
    this.loading = true;
    this.lessonId = this.getRouteParam('id');
    if (this.lessonId) {
      this.loadFlashcards(this.lessonId);
      this.lessonService.getById(this.lessonId).subscribe({
        next: (lesson) => {
          this.title = lesson.topic!;
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

  openModal(flashcard: FlashcardDto) {
    this.selectedFlashcard = flashcard;
    this.isModalOpen = true;
  }
  
  closeModal() {
    this.isModalOpen = false;
    this.selectedFlashcard = null;
  }
}

import { Component, OnInit } from '@angular/core';
import { IonIcon, IonHeader, IonToolbar, IonTitle, IonContent, IonModal } from '@ionic/angular/standalone';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from '../../toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { bookmark, close } from 'ionicons/icons';
import { ActivatedRoute, Router } from '@angular/router';
import { FlashcardService } from 'src/app/services/flashcard.service';
import { FlashcardDto } from 'src/app/dtos/flashcard.dto';

@Component({
    selector: 'app-flashcard',
    templateUrl: 'flashcard.page.html',
    styleUrls: ['flashcard.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal]
})
export class FlashcardPage implements OnInit {
  isModalOpen = false;
  selectedFlashcard: any = null;
  flashcards: FlashcardDto[] = [];
  lessonId: number | null = null;

  constructor(
    private router: Router,
    private flashcardService: FlashcardService,
    private route: ActivatedRoute,
  ) {
    addIcons({ bookmark, close});
  }

  ngOnInit() {
    this.lessonId = this.getRouteParam('id');
    if (this.lessonId) {
      this.loadFlashcards(this.lessonId);
    } else {
      console.error("Błąd ładowania fiszek dla tej lekcji.");
    }
  }

  loadFlashcards(lessonId: number): void {
    this.flashcardService.getAllByLessonId(lessonId).subscribe({
      next: (flashcards) => {
        this.flashcards = flashcards;
      },
      error: (err) => {
        console.error('Nie znaleziono fiszek dla tej lekcji:', err);
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

  openModal(flashcard: any) {
    this.selectedFlashcard = flashcard;
    this.isModalOpen = true;
  }
  
  closeModal() {
    this.isModalOpen = false;
    this.selectedFlashcard = null;
  }
}

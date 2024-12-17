import { Component } from '@angular/core';
import { IonIcon, IonHeader, IonToolbar, IonTitle, IonContent, IonModal } from '@ionic/angular/standalone';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from '../../toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { bookmark, close } from 'ionicons/icons';

@Component({
    selector: 'app-flashcard',
    templateUrl: 'flashcard.page.html',
    styleUrls: ['flashcard.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal]
})
export class FlashcardPage {
  flashcards = [
    { word: 'Apple' },
    { word: 'Banana' },
    { word: 'Cherry' },
    { word: 'Date' },
    { word: 'Elderberry' },
    { word: 'Fig' },
    { word: 'Grape' },
    { word: 'Honeydew' },
    { word: 'Apple' },
    { word: 'Banana' },
    { word: 'Cherry' },
    { word: 'Date' },
    { word: 'Elderberry' },
  ];

  isModalOpen = false;
  selectedFlashcard: any = null;

  constructor() {
    addIcons({ bookmark, close});
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

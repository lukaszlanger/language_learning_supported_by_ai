import { Component, OnInit } from '@angular/core';
import { IonIcon, IonHeader, IonToolbar, IonTitle, IonContent, IonModal } from '@ionic/angular/standalone';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from '../../toolbar/toolbar.component';
import { IonicModule, ModalController } from '@ionic/angular';
import { addIcons } from 'ionicons';
import { addCircle, addCircleOutline, bookmark, close, bookmarks, personCircle, personCircleOutline, settings, settingsOutline } from 'ionicons/icons';

@Component({
    selector: 'app-tab1',
    templateUrl: 'tab1.page.html',
    styleUrls: ['tab1.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule, IonIcon, IonModal]
})
export class Tab1Page {
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

  constructor(private modalController: ModalController) {
    addIcons({ bookmark, close});
  }

  openModal(flashcard: any) {
    console.log('Opening modal with:', flashcard); // Debugowanie
    this.selectedFlashcard = flashcard;
    this.isModalOpen = true;
  }
  
  closeModal() {
    console.log('Closing modal'); // Debugowanie
    this.isModalOpen = false;
    this.selectedFlashcard = null;
  }
  
}

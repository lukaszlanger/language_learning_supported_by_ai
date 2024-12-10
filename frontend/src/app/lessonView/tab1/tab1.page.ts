import { Component } from '@angular/core';
import { IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/angular/standalone';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from '../../toolbar/toolbar.component';
import { IonicModule } from '@ionic/angular';

@Component({
    selector: 'app-tab1',
    templateUrl: 'tab1.page.html',
    styleUrls: ['tab1.page.scss'],
    imports: [IonHeader, IonToolbar, IonTitle, IonContent, CommonModule, ToolbarComponent, IonicModule]
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
    // Add more flashcards as needed
  ];

  constructor() {}
}

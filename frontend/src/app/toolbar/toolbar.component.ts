import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { IonIcon, IonButton } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { addCircleOutline, settingsOutline } from 'ionicons/icons';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
  imports: [IonicModule, IonButton, IonIcon, CommonModule]
})
export class ToolbarComponent {
  @Input() page: string = '';

  constructor() {
    addIcons({ addCircleOutline, settingsOutline });
  }
}

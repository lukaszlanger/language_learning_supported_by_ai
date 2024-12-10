import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { IonIcon, IonButton } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { addCircle, addCircleOutline, settings, settingsOutline } from 'ionicons/icons';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
  imports: [IonicModule, IonButton, IonIcon, CommonModule]
})
export class ToolbarComponent {
  @Input() page: string = '';
  @Input() title: string = 'Language Learning';
  @Input() titleSmall: string = 'powered by AI';

  constructor() {
    addIcons({ addCircleOutline, addCircle, settingsOutline, settings });
  }
}

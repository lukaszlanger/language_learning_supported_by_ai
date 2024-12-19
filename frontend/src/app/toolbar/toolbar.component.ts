import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { IonIcon, IonButton } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { addCircle, addCircleOutline, personCircle, personCircleOutline, settings, settingsOutline } from 'ionicons/icons';
import { AuthService } from '../services/auth.service';

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
  @Input() addButtonItemName: string = '';
  @Output() leftButtonClicked = new EventEmitter<void>();
  @Output() rightButtonClicked = new EventEmitter<void>();

  constructor(
    private authService: AuthService
  ) {
    addIcons({ addCircleOutline, addCircle, settingsOutline, settings, personCircle });
  }

  getUserFirstName() {
    return this.authService.user?.firstName ?? 'Użytkownik';
  }
}

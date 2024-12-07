import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { IonIcon, IonInput, IonButton, IonLabel, IonRouterLink, IonRouterOutlet, IonList, IonItem, IonAvatar } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { arrowForwardOutline, eye } from 'ionicons/icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  imports: [IonicModule, FormsModule, IonIcon, ReactiveFormsModule, IonInput, IonLabel, IonRouterLink, IonRouterOutlet, IonList, IonItem, IonAvatar]
})
export class LoginPage {
  constructor(private router: Router) {
    addIcons({ arrowForwardOutline, eye });
  }

  navigateToNextPage() {
    this.router.navigate(['x/tabs/tab1']);
  }
}
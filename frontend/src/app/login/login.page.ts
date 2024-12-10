import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { IonIcon, IonInput, IonLabel, IonRouterLink, IonRouterOutlet, IonList, IonItem, IonAvatar } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { arrowForwardOutline, eye } from 'ionicons/icons';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from '../toolbar/toolbar.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  imports: [
    CommonModule,
    IonicModule,
    FormsModule,
    IonIcon,
    ReactiveFormsModule,
    IonInput,
    IonLabel,
    IonRouterLink,
    IonRouterOutlet,
    IonList,
    IonItem,
    IonAvatar,
    ToolbarComponent
  ],
})
export class LoginPage {
  loginForm: FormGroup;
  errorMessage: string | null = null;
  avatarSymbol: string = ':)';
  welcomeMessage: string = 'Zaloguj się!';

  constructor(
    private router: Router,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    addIcons({ arrowForwardOutline, eye });

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  nextTest() {
    this.router.navigate(['home']);
  }

  async onSubmit() {
    this.errorMessage = null;

    if (this.loginForm.valid) {
      try {
        await this.authService.loginAndSetUser(this.loginForm.value);

        this.welcomeMessage = `Witaj, ${this.authService.user?.firstName}!`;
        this.avatarSymbol = this.authService.user?.firstName?.charAt(0).toUpperCase() || this.avatarSymbol;

        this.router.navigate(['home']);
      } catch (error) {
        this.errorMessage = 'Nie udało się zalogować. Spróbuj ponownie.';
      }
    } else {
      this.errorMessage = 'Proszę wypełnić wszystkie pola poprawnie.';
    }
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}

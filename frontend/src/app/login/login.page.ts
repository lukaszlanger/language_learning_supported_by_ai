import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { IonIcon, IonInput, IonLabel, IonRouterLink, IonRouterOutlet, IonList, IonItem, IonAvatar, IonSpinner } from '@ionic/angular/standalone';
import { addIcons } from 'ionicons';
import { arrowForward } from 'ionicons/icons';
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
    ToolbarComponent,
    IonSpinner
  ],
})
export class LoginPage {
  loginForm: FormGroup;
  errorMessage: string | null = null;
  avatarSymbol: string = ':)';
  welcomeMessage: string = 'Zaloguj się!';
  isSubmitting: boolean = false;
  isLoggingIn: boolean = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {
    addIcons({ arrowForward });

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
    
    this.setMessages();
  }

  async onSubmit() {
    this.errorMessage = null;

    if (this.loginForm.valid) {
      this.isSubmitting = true;
      try {
        await this.authService.loginAndSetUser(this.loginForm.value);
        this.setMessages();
        this.router.navigate(['lessons']);
      } catch (error) {
        this.errorMessage = 'Nie udało się zalogować. Spróbuj ponownie.';
      } finally {
        this.isSubmitting = false;
      }
    } else {
      this.errorMessage = 'Proszę wypełnić wszystkie pola poprawnie.';
    }
  }

  onLogin() {
    this.isLoggingIn = true;
    this.setMessages();
    this.router.navigate(['lessons']);
    this.isLoggingIn = false;
  }

  setMessages() {
    this.welcomeMessage = this.authService.user ? `Witaj, ${this.authService.user?.firstName}!` : 'Zaloguj się!';
    this.avatarSymbol = this.authService.user?.firstName?.charAt(0).toUpperCase() || ':)';
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logout();
    this.setMessages();
  }
}

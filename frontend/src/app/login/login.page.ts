import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  imports: [IonicModule, FormsModule],
})
export class LoginPage {
  constructor(private router: Router) {}

  navigateToNextPage() {
    this.router.navigate(['/']);
  }
}
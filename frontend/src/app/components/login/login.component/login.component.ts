import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
  this.errorMessage = '';
  console.log('Sending login request:', {
    username: this.username,
    password: this.password
  });

  this.authService.login({ username: this.username, password: this.password })
    .subscribe({
      next: (res) => {
        console.log('Login success:', res);
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('Login error:', err);
        this.errorMessage = 'Invalid username or password';
      }
    });
  }
}

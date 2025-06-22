import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app';
import { provideHttpClient } from '@angular/common/http'; // Add this

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(), // Modern HTTP client setup
    // Other providers...
  ]
});
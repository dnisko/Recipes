import { bootstrapApplication } from '@angular/platform-browser';
//import { LayoutComponent } from './app/components/layout/layout.component';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http'; // Add this
import { routes } from './app/app.routes';
import { provideRouter } from '@angular/router';
//import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { appConfig } from './app/app.config';

// bootstrapApplication(AppComponent, {
//   providers: [
//     provideRouter(routes),
//     provideHttpClient()
//   ]
// });

bootstrapApplication(AppComponent, appConfig);
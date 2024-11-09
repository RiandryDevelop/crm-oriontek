import { Routes } from '@angular/router';

const routes: Routes = [
  { path: 'clients', loadChildren: () => import('./features/client/client.module').then(m => m.ClientModule) },
  // Add other feature routes
  { path: '', redirectTo: '/clients', pathMatch: 'full' },
  { path: '**', redirectTo: '/clients' }
];

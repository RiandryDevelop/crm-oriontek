import { APP_INITIALIZER, Provider } from '@angular/core';
import { Chart, registerables } from 'chart.js';

export function initializeApp(): () => Promise<void> {
  return () =>
    new Promise((resolve) => {
      console.log('App Initialization Logic Here');
      setTimeout(() => {
        console.log('App initialized successfully');
        resolve();
      }, 1000); // Simulating an async operation
    });
}

export function provideAppInitializer(): Provider {
  return {
    provide: APP_INITIALIZER,
    useFactory: initializeApp,
    multi: true,
  };
}

export function provideCharts(registerables: any): Provider {
  return {
    provide: 'CHARTS',
    useValue: registerables, // Pass chart.js or similar registerables
  };
}
export function provideAnimationsAsync(): Provider {
  return {
    provide: 'ENABLE_ANIMATIONS',
    useValue: true, // Set to `false` if you want to disable animations
  };
}

export function provideApiUrl(): Provider {
  return {
    provide: 'API_URL',
    useValue: 'https://api.example.com', // Replace with your actual base URL
  };
}



export function withDefaultRegisterables(): any {
  Chart.register(...registerables); // Register default chart.js components
  return registerables;
}

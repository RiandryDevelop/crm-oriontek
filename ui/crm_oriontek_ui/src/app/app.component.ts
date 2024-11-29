import { Component } from '@angular/core';

@Component({
  selector: 'app-root', // Nombre del selector que se usa en el HTML (en index.html)
  templateUrl: './app.component.html', // Enlace al archivo HTML de la plantilla
  styleUrls: ['./app.component.css'], // Enlace al archivo CSS de estilos
  standalone: true,
})
export default class AppComponent {
  title = 'CRM Application'; // Variable que puedes usar en el HTML (interpolación)
}

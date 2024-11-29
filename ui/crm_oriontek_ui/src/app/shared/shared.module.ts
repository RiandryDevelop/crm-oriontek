import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    // Componentes compartidos como NavbarComponent, FooterComponent, etc.
  ],
  imports: [
    CommonModule, // Para directivas comunes como *ngIf o *ngFor
  ],
  exports: [
    // Exporta componentes reutilizables como SpinnerComponent o Pipes
  ],
})
export class SharedModule {}

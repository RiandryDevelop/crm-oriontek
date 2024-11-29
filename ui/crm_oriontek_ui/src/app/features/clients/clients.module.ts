import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms'; // Manejo de formularios
import { ClientsRoutingModule } from './clients-routing.module'; // Rutas específicas de clientes
import { ClientListComponent } from './pages/client-list/client-list.component'; // Página de lista de clientes
import { ClientFormComponent } from './components//client-form/client-form.component'; // Componente para agregar/editar clientes

@NgModule({
  declarations: [
    ClientListComponent,
    ClientFormComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ClientsRoutingModule,
  ],
})
export class ClientsModule {}

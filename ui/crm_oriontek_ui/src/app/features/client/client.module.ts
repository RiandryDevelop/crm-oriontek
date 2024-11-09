import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientListComponent } from './components/client-list/client-list.component';
import { ClientService } from './services/client.service';
import { ClientRoutingModule } from './client-routing.module';

@NgModule({
  declarations: [ClientListComponent],
  imports: [
    CommonModule,
    ClientRoutingModule
  ],
  providers: [ClientService]
})
export class ClientModule { }

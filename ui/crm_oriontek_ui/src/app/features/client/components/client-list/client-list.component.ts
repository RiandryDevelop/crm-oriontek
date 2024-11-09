import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { Client } from '../../../../core/models/client.model';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {
  clients: Client[] = [];
  filteredClients: Client[] = [];
  searchTerm: string = '';
  page: number = 1;
  size: number = 10;

  constructor(private clientService: ClientService) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    // this.clientService.getClientsPaginated(this.page, this.size, this.searchTerm).subscribe((clients) => {
    //   this.clients = clients;
    //   this.filteredClients = clients;
    // })
    console.log('Loading clients...');
  }

  onSearch(): void {
    this.filteredClients = this.clients.filter(client =>
      client.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      || client.email.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  onAddClient(client: Client): void {
    // Code to open a form or modal for adding a new client
    console.log('Add Client button clicked');
    // this.clientService.createClient(client);

  }

  onViewClient(client: Client): void {
    // Code to open a detailed view for the selected client
    console.log('Viewing client:', client);
    // this.clientService.getClientById(client.clientId);
  }

  onEditClient(client: Client): void {
    // Code to open an edit form or modal for the selected client
    console.log('Editing client:', client);
    // this.clientService.updateClient(client);
  }

  onDeleteClient(clientId: number): void {
    if (confirm('Are you sure you want to delete this client?')) {
    //   this.clientService.deleteClientById(clientId).subscribe(() => {
    //     this.clients = this.clients.filter(client => client.clientId !== clientId);
    //     this.filteredClients = this.filteredClients.filter(client => client.clientId !== clientId);
    //     console.log('Client deleted:', clientId);
    //   });
    console.log('Client deleted:', clientId);
    }
  }
}

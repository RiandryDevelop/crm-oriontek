import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client } from '../../../core/models/client.model';
import { environment } from '../../../../environments/environment';

@Injectable()
export class ClientService {
  private apiUrl = `${environment.apiUrl}/clients`;

  constructor(private http: HttpClient) {}

  

  // CRUD methods
  // Create a new client
  createClient(client: Client): Observable<Client> {
    return this.http.post<Client>(this.apiUrl, client);
  }
  //Retrieve a client by ID

  getClientById(id: number): Observable<Client> {
    return this.http.get<Client>(`${this.apiUrl}/${id}`);
  }
  // Retrieve all clients paginated
  getClientsPaginated(page: number, size: number, searchTerm: string): Observable<Client[]> {
    return this.http.get<Client[]>(`${this.apiUrl}?page=${page}&size=${size}&searchTerm=${searchTerm}`);
  }

  // Update an existing client
  updateClient(client: Client): Observable<Client> {
    return this.http.put<Client>(`${this.apiUrl}/${client.clientId}`, client);
  }

// Delete a client by ID
  deleteClientById(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

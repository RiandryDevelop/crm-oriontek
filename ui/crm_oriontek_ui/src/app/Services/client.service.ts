// src/app/services/client.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Client } from '../models/client/client.model';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ClientService {
  constructor(private http: HttpClient) {}

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>('/api/clients').pipe(
      catchError((error) => {
        console.error('Error fetching clients:', error);
        return of([]); // Devuelve un arreglo vacío en caso de error
      })
    );
  }

  getClientById(id: number): Observable<Client> {
    return this.http.get<Client>(`/api/clients/${id}`).pipe(
      catchError((error) => {
        console.error(`Error fetching client with id ${id}:`, error);
        return of(null as any); // Devuelve null en caso de error
      })
    );
  }

  createClient(client: Client): Observable<Client> {
    return this.http.post<Client>('/api/clients', client).pipe(
      catchError((error) => {
        console.error('Error creating client:', error);
        return of(null as any); // Devuelve null en caso de error
      })
    );
  }

  updateClient(client: Client): Observable<Client> {
    return this.http.put<Client>(`/api/clients/${client.clientId}`, client).pipe(
      catchError((error) => {
        console.error(`Error updating client with id ${client.clientId}:`, error);
        return of(null as any); // Devuelve null en caso de error
      })
    );
  }

  deleteClient(id: number): Observable<void> {
    return this.http.delete<void>(`/api/clients/${id}`).pipe(
      catchError((error) => {
        console.error(`Error deleting client with id ${id}:`, error);
        return of(undefined); // Devuelve undefined en caso de error
      })
    );
  }

  getClientsPaginated(page: number, size: number): Observable<Client[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('size', size.toString());

    return this.http.get<Client[]>('/api/clients/paginated', { params }).pipe(
      catchError((error) => {
        console.error('Error fetching paginated clients:', error);
        return of([]); // Devuelve un arreglo vacío en caso de error
      })
    );
  }
}

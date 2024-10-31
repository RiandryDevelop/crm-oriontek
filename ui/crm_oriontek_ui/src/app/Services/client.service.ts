import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client } from '../models/client.model';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private baseUrl = environment.apiUrl; // Replace with your API URL

  constructor(private http: HttpClient) {}

  getClientsPaginated(page: number, size: number, searchData: string): Observable<any> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('size', size.toString())
      .set('searchData', searchData || '');

    return this.http.get<any>(`${this.baseUrl}Client/GetClientPaginate`, { params });
  }

  getClientById(id: number): Observable<Client> {
    return this.http.get<Client>(`${this.baseUrl}Client/GetOneClient`, { params: { id } });
  }

  createClient(client: Client): Observable<Client> {
    return this.http.post<Client>(`${this.baseUrl}Client/CreateClient`, client);
  }

  updateClient(client: Client): Observable<Client> {
    return this.http.put<Client>(`${this.baseUrl}Client/UpdateClient`, client);
  }

  deleteClient(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}Client/DeleteClient`, { params: { id } });
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customers } from '../models/customers';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = 'https://localhost:7134/api/Customers';

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Customers[]> {
    return this.http.get<Customers[]>(this.apiUrl);
  }

  addCustomer(customer: Customers): Observable<Customers> {
    return this.http.post<Customers>(this.apiUrl, customer);
  }

  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
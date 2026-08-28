import {
  ChangeDetectorRef,
  Component,
  OnInit
} from '@angular/core';

import { RouterLink } from '@angular/router';
import { Customers } from '../models/customers';
import { CustomerService } from '../services/customer.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './customer.html',
  styleUrl: './customer.css'
})
export class Customer implements OnInit {

  customers: Customers[] = [];
  isAdmin = false;

  constructor(
    private customerService: CustomerService,
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.isAdmin = this.authService.isAdmin();

    console.log('Customer component initialized');

    this.customerService.getCustomers().subscribe({

      next: (data: Customers[]) => {

        console.log('API data:', data);

        this.customers = data;

        console.log(
          'Component customers length:',
          this.customers.length
        );

        // Force Angular to update the view
        this.cdr.detectChanges();
      },

      error: (error) => {
        console.error('Failed to load customers:', error);
      }

    });

  }

  deleteCustomer(id: number): void {

    this.customerService.deleteCustomer(id).subscribe({
      next: () => {
        this.customers = this.customers.filter(c => c.id !== id);
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Failed to delete customer:', error);
      }
    });

  }
}
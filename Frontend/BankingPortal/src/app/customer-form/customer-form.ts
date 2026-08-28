import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerService } from '../services/customer.service';
import { Customers } from '../models/customers';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './customer-form.html',
  styleUrl: './customer-form.css',
})
export class CustomerForm {

  customerForm;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private customerService: CustomerService
  ) {

    this.customerForm = this.fb.group({

      name: ['', Validators.required],

      email: ['', [Validators.required, Validators.email]],

      phone: ['', Validators.required],

      risk: ['Medium', Validators.required]

    });

  }

  saveCustomer() {

  if(this.customerForm.invalid){
    return;
  }


  this.customerService.addCustomer(
    this.customerForm.value as Customers
  );


  alert("Customer Added Successfully");


  this.router.navigate(['/customer']);

}

  cancel() {

    this.router.navigate(['/customer']);

  }

}
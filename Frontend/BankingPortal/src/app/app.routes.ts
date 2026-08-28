import { Routes } from '@angular/router';

import { Login } from './login/login';
import { Dashboard } from './dashboard/dashboard';
import { Customer } from './customer/customer';
import { CustomerForm } from './customer-form/customer-form';
import { AiChat } from './ai-chat/ai-chat';

export const routes: Routes = [

  {
    path: '',
    component: Login
  },

  {
    path: 'dashboard',
    component: Dashboard
  },

  {
    path: 'customer',
    component: Customer
  },

  {
    path: 'customer/add',
    component: CustomerForm
  },

  {
    path: 'ai-chat',
    component: AiChat
  },

  {
    path: '**',
    redirectTo: ''
  }

];
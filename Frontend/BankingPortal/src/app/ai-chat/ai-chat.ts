import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import {
AIService,
AIChatResponse
} from '../services/ai.service';

@Component({
selector: 'app-ai-chat',
standalone: true,
imports: [
FormsModule,
CommonModule
],
templateUrl: './ai-chat.html',
styleUrl: './ai-chat.css'
})
export class AiChat {

question = '';
answer = '';
loading = false;

constructor(private aiService: AIService) {}

askAI(): void {


console.log('Button clicked');
console.log('Question:', this.question);

if (!this.question.trim()) {
  return;
}

this.loading = true;
this.answer = '';

this.aiService.askQuestion(this.question)
  .subscribe({

    next: (response: AIChatResponse) => {

      console.log('Response received from .NET:');
      console.log(response);

      this.answer = response.answer;

      console.log('Answer variable:', this.answer);

      this.loading = false;

      console.log('Loading:', this.loading);
    },

    error: (error) => {

      console.error('ERROR calling .NET API');
      console.error(error);

      this.answer =
        'Unable to connect to the backend API.';

      this.loading = false;
    }

  });


}
}

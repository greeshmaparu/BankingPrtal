import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface AIChatResponse {
  question: string;
  answer: string;
}

@Injectable({
  providedIn: 'root'
})
export class AIService {

  private apiUrl = 'https://localhost:7134/api/AI/chat';

  constructor(private http: HttpClient) {}

  askQuestion(question: string): Observable<AIChatResponse> {

    console.log('================================');
    console.log('Calling .NET API');
    console.log('URL:', this.apiUrl);
    console.log('Question:', question);
    console.log('Request JSON:', JSON.stringify(question));
    console.log('================================');

    return this.http.post<AIChatResponse>(
      this.apiUrl,
      JSON.stringify(question),
      {
        headers: {
          'Content-Type': 'application/json',
          'Accept': '*/*'
        }
      }
    );
  }
}
import { Component } from '@angular/core';
import { Sidebar } from '../shared/sidebar/sidebar';
import { AiChat } from '../ai-chat/ai-chat';

@Component({
selector: 'app-dashboard',
imports: [Sidebar, AiChat],
templateUrl: './dashboard.html',
styleUrl: './dashboard.css',
})
export class Dashboard {

}

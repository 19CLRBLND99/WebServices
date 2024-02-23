//Defines the logic for the application's root component, named AppComponent. 
//The view associated with this root component becomes the root of the view 
//hierarchy as you add components and services to your application.

import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'WebServicesFrontend';
  httpClient = inject(HttpClient);
  rooms:any = [];
  fetchAllRooms():void{
      this.httpClient.get('https://localhost:32772/GetAllRooms').subscribe((data:any)=>{
        this.rooms = data;
        console.log(this.rooms);
      });
  }

  ngOnInit():void{
    this.fetchAllRooms();
  }

}

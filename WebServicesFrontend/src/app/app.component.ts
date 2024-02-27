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
      this.httpClient.get('https://localhost:32770/GetAllRooms').subscribe((data:any)=>{
        this.rooms = data;
        console.log(this.rooms);
      });
  }

  openAddWindow(){
    console.log("test");
    var body = document.body;
    
    // Erstelle ein neues Textfeld
    var textfeld = document.createElement("input");
    textfeld.type = "text";
    textfeld.id = "nameField";
    
    // Erstelle einen neuen Button
    var button = document.createElement("button");
    button.innerHTML = "Hinzufügen!";
    button.addEventListener('click', () => {
      this.addRoom(textfeld.value);
    });
    
    // Füge das Textfeld und den Button dem Body des Dokuments hinzu
    body.appendChild(textfeld);
    body.appendChild(button);
    // [].forEach.call(document.querySelectorAll('main'),
    // function (e) {
      
    //     e.innerHTML = e.innerHTML +//the user fills the fields, which then will be used by the php code
    //         "<div class='addWindow'><div class='addForm'>"+
    //         "Name<br><input class='textFieldAddW' type='text' name='topicName' id='nameField' required><br>" +
    //         //confirms the action
    //         "<button name='Add' (click)=addRoom()>Add Topic</button>" +
    //         //cancels the action
    //         "<button class='cancelBtt' type='submit' name='close' onclick='closeWindow(\"topicNameField\")'>Cancel</button></div></div>";
    // });
}
addRoom(name):void{
    this.httpClient.post<any>("https://localhost:32770/AddRoom?roomName="+name,null).subscribe(response => {
      console.log(response); // Hier erhältst du die Antwort von der API
      // Füge hier die Logik hinzu, die du nach dem Hinzufügen des Raums ausführen möchtest
    });
    console.log(name);
}

  ngOnInit():void{
    this.fetchAllRooms();
  }

}

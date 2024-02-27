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
  rooms: any = [];
  fetchAllRooms(): void {
    this.httpClient.get('https://localhost:32770/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
    });
  }

  openAddWindow() {
    console.log("test");
    var body = document.body;

    // Erstelle ein neues Textfeld
    var textfeld = document.createElement("input");
    textfeld.type = "text";
    textfeld.placeholder = "Raumname(Pflicht)";

    var idTextArea = document.createElement("input");
    idTextArea.type = "text";
    idTextArea.placeholder = "Thermostat Id(Optional)";


    // Erstelle einen neuen Button
    var button = document.createElement("button");
    button.innerHTML = "Hinzufügen!";
    button.disabled = true;
    button.addEventListener('click', () => {
      this.addRoom(textfeld.value, idTextArea.value);
    });
    textfeld.addEventListener("input", function () {
      button.disabled = !textfeld.value;
    });
    idTextArea.onkeyup = event => { this.checkForFreeId(idTextArea.value, button, textfeld.value) };
    textfeld.onkeyup = event => { this.checkForFreeId(idTextArea.value, button, textfeld.value) };
    // Füge das Textfeld und den Button dem Body des Dokuments hinzu
    body.appendChild(textfeld);
    body.appendChild(idTextArea);
    body.appendChild(button);
  }
  addRoom(name, id): void {
    let newRoomId;
    this.httpClient.post<number>("https://localhost:32770/AddRoom?roomName=" + name, null).subscribe((response: number) => {
      newRoomId = response;
      this.httpClient.post<any>("https://localhost:32770/AssignThermostatToRoom?roomId=" + newRoomId + "&thermostatId=" + id, null).subscribe(response => {
        console.log(response); // Hier erhältst du die Antwort von der API
      });
    });
  }

  checkForFreeId(givenId, button, textarea) {
    var taken = false;

    if (givenId != "") {
      if (textarea != "") {
        this.httpClient.get("https://localhost:32770/CheckThermostatId?thermostatId=" + givenId).subscribe((data: TupleResponse) => {
          console.log(data);
          taken = data.item1.valueOf();
          if (data.item2 != null) {
            var listItems = data.item2.join(",");
            console.log("Thermostat id is already taken! Unused Thermostats: " + listItems);
          }
          button.disabled = !taken;
        });
      } else {
        button.disabled = true;
      }
    }
  }

  ngOnInit(): void {
    this.fetchAllRooms();
  }

}

export interface TupleResponse {
  item1: boolean;
  item2: any[]; // Hier kannst du den Datentyp der Liste spezifizieren, den deine API zurückgibt
}

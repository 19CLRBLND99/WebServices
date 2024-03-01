//Defines the logic for the application's root component, named AppComponent. 
//The view associated with this root component becomes the root of the view 
//hierarchy as you add components and services to your application.

import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { RoomDialogComponent } from './room-dialog/room-dialog.component';

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
  thermostats: any = [];
  temperature: any = [];
  roomsWithTemperature: any = [];
  baseUrl: String = 'http://localhost:50000';

  getAllRooms(): void {
    this.httpClient.get(this.baseUrl+'/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
      this.getTemperaturesForRooms();
    });
  }

  getTemperaturesForRooms(): void {
    this.rooms.forEach((room) => {
      this.httpClient.get(this.baseUrl+`/GetRoomWithThermostatByRoomId?roomId=${room.roomId}`).subscribe((temperatureData: any) => {
        room.temperature = temperatureData.thermostat.temperature;
      });
    });
  }

  openAddWindow() {
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
    this.httpClient.post<number>(this.baseUrl+"/AddRoom?roomName=" + name, null).subscribe((response: number) => {
      newRoomId = response;
      if (id != "") {
        this.httpClient.post<any>(this.baseUrl+"/AssignThermostatToRoom?roomId=" + newRoomId + "&thermostatId=" + id, null).subscribe(response => {
          console.log(response); // Hier erhältst du die Antwort von der API
        });
      }
    });
  }

  checkForFreeId(givenId, button, textarea) {
    var taken = false;

    if (givenId != "") {
      if (textarea != "") {
        this.httpClient.get(this.baseUrl+"/CheckThermostatId?thermostatId=" + givenId).subscribe((data: TupleResponse) => {
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
    }else{
      if (textarea != ""){
        button.disabled = false;
      }
    }

  }

  ngOnInit(): void {
    this.getAllRooms();
  }

  constructor(public dialog: MatDialog) { }

  openRoomDialog(roomId: number): void {
    const dialogRef = this.dialog.open(RoomDialogComponent, {
      width: '400px',
      data: {roomId: roomId} // Hier können Sie Raumdaten übergeben, wenn erforderlich
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('Das Dialogfenster wurde geschlossen');
    });
  }

}

export interface TupleResponse {
  item1: boolean;
  item2: any[]; // Hier kannst du den Datentyp der Liste spezifizieren, den deine API zurückgibt
}

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
  thermostats: any = [];
  temperature: any = [];
  roomsWithTemperature: any = [];

  getAllRooms(): void {
    this.httpClient.get('https://localhost:32772/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
      this.getTemperaturesForRooms();
    });
  }

  deleteRoom(roomId: number): void {
    this.httpClient.delete(`https://localhost:32772/DeleteRoom?roomId=${roomId}`).subscribe((data: any) => {
      console.log(`Raum mit der ID ${roomId} wurde erfolgreich gelöscht.`);
      this.getAllRooms();
    });
  }

  getTemperaturesForRooms(): void {
    this.rooms.forEach((room) => {
      this.httpClient.get(`https://localhost:32772/GetRoomWithThermostatByRoomId?roomId=${room.roomId}`).subscribe((temperatureData: any) => {
        room.temperature = temperatureData.thermostat.temperature;
      });
    });
  }

  changeTemperature(roomId: number, newTemperature: number): void {

    this.httpClient.post<any>(`https://localhost:32772/UpdateRoomTemperature?roomId=${roomId}&newTemperature=${newTemperature}`, null).subscribe((data: any) => {
      console.log('Temperature changed successfully');
      this.getAllRooms();
    }, (error) => {
      console.error('Error while changing temperature:', error);
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

  openChangeTemperatureWindow(roomId: number) {
    const newTemperature = prompt('Geben Sie die neue Temperatur ein:');
    if (newTemperature !== null) {
      const parsedTemperature = parseFloat(newTemperature);
      if (!isNaN(parsedTemperature)) {
        this.changeTemperature(roomId, parsedTemperature);
      } else {
        alert('Ungültige Eingabe! Bitte geben Sie eine numerische Temperatur ein.');
      }
    }
  }

  addRoom(name, id): void {
    let newRoomId;
    this.httpClient.post<number>("https://localhost:32772/AddRoom?roomName=" + name, null).subscribe((response: number) => {
      newRoomId = response;
      if (id != "") {
        this.httpClient.post<any>("https://localhost:32772/AssignThermostatToRoom?roomId=" + newRoomId + "&thermostatId=" + id, null).subscribe(response => {
          console.log(response); // Hier erhältst du die Antwort von der API
        });
      }
    });
  }

  updateRoomName(roomId: number, newName: string): void {
    this.httpClient.post<any>(`https://localhost:32772/UpdateRoomName?roomId=${roomId}&newRoomName=${newName}`, null).subscribe(() => {
      console.log(`Room name updated successfully for room ID ${roomId}`);
      this.getAllRooms(); // Update the room list after the name change
    }, (error) => {
      console.error('Error while updating room name:', error);
    });
  }

  openChangeRoomNameWindow(roomId: number, currentName: string): void {
    const newName = prompt(`Geben Sie den neuen Namen für Raum ${currentName} ein:`);
    if (newName !== null) {
      this.updateRoomName(roomId, newName);
    }
  }

  checkForFreeId(givenId, button, textarea) {
    var taken = false;

    if (givenId != "") {
      if (textarea != "") {
        this.httpClient.get("https://localhost:32772/CheckThermostatId?thermostatId=" + givenId).subscribe((data: TupleResponse) => {
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

  addThermostatAndAssignToRoom(roomId: number): void {
    this.httpClient.post<any>('https://localhost:32772/AddThermostat', null).subscribe((thermostatId: number) => {
      this.httpClient.post<any>(`https://localhost:32772/AssignThermostatToRoom?roomId=${roomId}&thermostatId=${thermostatId}`, null)
        .subscribe(() => {
          console.log(`Thermostat added and assigned to room ID ${roomId}`);
          this.getAllRooms(); // Update the room list after adding the thermostat
        }, (error) => {
          console.error('Error while assigning thermostat to room:', error);
        });
    }, (error) => {
      console.error('Error while adding thermostat:', error);
    });
  }

  ngOnInit(): void {
    this.getAllRooms();
  }
}

export interface TupleResponse {
  item1: boolean;
  item2: any[]; // Hier kannst du den Datentyp der Liste spezifizieren, den deine API zurückgibt
}

//Defines the logic for the application's root component, named AppComponent. 
//The view associated with this root component becomes the root of the view 
//hierarchy as you add components and services to your application.

import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { PopupService } from './popup/popup.service';

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
  constructor(private popupService: PopupService) {}

  openPopup() {
    this.popupService.openPopup();
  }

  getAllRooms(): void {
    this.httpClient.get('https://localhost:32770/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
      this.getTemperaturesForRooms();
    });
  }

  getTemperaturesForRooms(): void {
    this.rooms.forEach((room) => {
      this.httpClient.get(`https://localhost:32770/GetRoomWithThermostatByRoomId?roomId=${room.roomId}`).subscribe((temperatureData: any) => {
        room.temperature = temperatureData.thermostat.temperature;
      });
    });
  }

  //Funktioniert nicht optimal, da nicht einzelne Items sondern eine Liste ausgegeben wird
  getAllThermostatIds(): void {
    this.httpClient.get('https://localhost:32770/GetAllThermostatIds').subscribe((data: any) => {
      this.thermostats = data;
      console.log(this.thermostats);
    });
  }

  changeTemperature(roomId: number, newTemperature: number): void {

    this.httpClient.post<any>(`https://localhost:32770/UpdateRoomTemperature?roomId=${roomId}&newTemperature=${newTemperature}`, null).subscribe((data: any) => {
      console.log('Temperature changed successfully');
      // Optional: Aktualisieren Sie die Liste der Räume, um die aktualisierten Daten anzuzeigen
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
    this.getAllRooms();
    this.getAllThermostatIds();
  }

}

export interface TupleResponse {
  item1: boolean;
  item2: any[]; // Hier kannst du den Datentyp der Liste spezifizieren, den deine API zurückgibt
}

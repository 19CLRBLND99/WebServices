//Defines the logic for the application's root component, named AppComponent. 
//The view associated with this root component becomes the root of the view 
//hierarchy as you add components and services to your application.

import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { RoomDialogComponent } from './room-dialog/room-dialog.component';
import { AddRoomDialogComponent } from './add-room-dialog/add-room-dialog.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'WebServicesFrontend';
  httpClient = inject(HttpClient);
  public rooms: any = [];
  thermostats: any = [];
  temperature: any = [];
  roomsWithTemperature: any = [];
  baseUrl: String = 'http://localhost:50000';

  constructor(public dialog: MatDialog) { }

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

  openAddRoomDialog(): void {
    const dialogRef = this.dialog.open(AddRoomDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Raumname:', result);
      }
    });
  }

  ngOnInit(): void {
    this.getAllRooms();
  }

  openRoomDialog(room: any): void {
    const dialogRef = this.dialog.open(RoomDialogComponent, {
      width: '400px',
      data: {room: room} // Hier können Sie Raumdaten übergeben, wenn erforderlich
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('Das Dialogfenster wurde geschlossen');
    });
  }

  hasThermostat(room: any): boolean {
    return room.thermostatId !== null;
  }

}
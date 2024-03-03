import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { RoomDialogComponent } from './room-dialog/room-dialog.component';
import { AddRoomDialogComponent } from './add-room-dialog/add-room-dialog.component';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppErrorDialogComponent } from './app-error-dialog/app-error-dialog.component';

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

  ngOnInit(): void {
    this.getAllRooms();
  }

  //Method to fetch all Rooms 
  getAllRooms(): void {
    this.httpClient.get(this.baseUrl+'/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
      this.getTemperaturesForRooms();
    });
  }

  //Method to fetch the temperatures of the rooms
  getTemperaturesForRooms(): void {
    this.rooms.forEach((room) => {
      this.httpClient.get(this.baseUrl+`/GetRoomWithThermostatByRoomId?roomId=${room.roomId}`).subscribe((temperatureData: any) => {
        room.temperature = temperatureData.thermostat.temperature;
      });
    });
  }

  //open Pop Up Window for adding a Room
  openAddRoomDialog(): void {
    this.getRoomCount().subscribe((roomCount: number) => {
      console.log("Roomcount:"+roomCount)
      //when there are 25 rooms: error Pop up shows
    if (roomCount >= 25) {
      this.dialog.open(AppErrorDialogComponent, {
        width: '400px',
        data: { errorMessage: 'Es dürfen nur 25 Räume hinzugefügt werden!' }
      });
      //when there are less then 25 rooms: add room window
    } else {
      const dialogRef = this.dialog.open(AddRoomDialogComponent, {
        width: '400px',
        data: { roomCount: roomCount } // Übergeben der Anzahl der Räume
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          console.log('Raumname:', result);
        }
      });
    }
  });
  }

  //open Pop up window for Room Configurations
  openRoomDialog(room: any): void {
    const dialogRef = this.dialog.open(RoomDialogComponent, {
      width: '400px',
      data: {room: room}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('Das Dialogfenster wurde geschlossen');
    });
  }

  //get the number of rooms 
  getRoomCount(): Observable<number> {
    return this.httpClient.get<any[]>(this.baseUrl + '/GetAllRooms').pipe(
      map(rooms => rooms.length)
    );
  }

}
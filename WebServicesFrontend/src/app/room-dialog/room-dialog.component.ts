// room-dialog.component.ts

import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { EditTemperatureDialogComponent } from '../edit-temperature-dialog/edit-temperature-dialog.component';


@Component({
  selector: 'app-room-dialog',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './room-dialog.component.html',
  styleUrls: ['./room-dialog.component.css']
})
export class RoomDialogComponent {
  baseUrl: String = 'http://localhost:50000';
  rooms: any = [];

  constructor(
    public dialogRef: MatDialogRef<RoomDialogComponent>,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private httpClient: HttpClient
  ) {  }

  getAllRooms(): void {
    this.httpClient.get(this.baseUrl+'/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
      this.getTemperaturesForRooms();
    });
  }

  deleteRoom(): void {
    this.httpClient.delete(this.baseUrl+`/DeleteRoom?roomId=${this.data.roomId}`).subscribe((data: any) => {
      console.log(`Raum mit der ID ${this.data.roomId} wurde erfolgreich gelöscht.`);
      this.getAllRooms();
    });
  }

  getTemperaturesForRooms(): void {
    this.rooms.forEach((room) => {
      this.httpClient.get(this.baseUrl+`/GetRoomWithThermostatByRoomId?roomId=${room.roomId}`).subscribe((temperatureData: any) => {
        room.temperature = temperatureData.thermostat.temperature;
      });
    });
  }

  addThermostatAndAssignToRoom(): void {
      this.httpClient.post<any>(this.baseUrl+`/AssignThermostatToRoom?roomId=${this.data.roomId}&thermostatId=${this.data.roomId}`, null)
        .subscribe(() => {
          console.log(`Thermostat added and assigned to room ID ${this.data.roomId}`);
          this.getAllRooms(); // Update the room list after adding the thermostat
        }, (error) => {
          console.error('Error while assigning thermostat to room:', error);
        });
  }

  openChangeTemperatureWindow(): void {
    const dialogRef = this.dialog.open(EditTemperatureDialogComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        // Hier können Sie die neue Temperatur verarbeiten
        console.log('Neue Temperatur:', result);
        // this.changeTemperature(roomId, result);
      }
    });
  }
  

  changeTemperature(roomId: number, newTemperature: number): void {

    this.httpClient.post<any>(this.baseUrl+`/UpdateRoomTemperature?roomId=${roomId}&newTemperature=${newTemperature}`, null).subscribe((data: any) => {
      console.log('Temperature changed successfully');
      this.getAllRooms();
    }, (error) => {
      console.error('Error while changing temperature:', error);
    });
  }

  updateRoomName(roomId: number, newName: string): void {
    this.httpClient.post<any>(this.baseUrl+`/UpdateRoomName?roomId=${this.data.roomId}&newRoomName=${newName}`, null).subscribe(() => {
        console.log(`Room name updated successfully for room ID ${roomId}`);
        this.getAllRooms(); // Update the room list after the name change
      }, (error) => {
        console.error('Error while updating room name:', error);
      });
  }

  openChangeRoomNameWindow(): void {
    const newName = prompt(`Geben Sie den neuen Namen für den Raum ein:`);
    if (newName !== null) {
      this.updateRoomName(this.data.roomId, newName);
    }
  }

  onSave(): void {
    // Hier können Sie Ihre Logik für das Speichern der Raumdaten implementieren
    this.dialogRef.close();
  }

  onCancel(): void {
    this.dialogRef.close();
  }



}

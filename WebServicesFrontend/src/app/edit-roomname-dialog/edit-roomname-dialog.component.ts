import { Component, inject, Inject } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-edit-roomname-dialog',
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, HttpClientModule],
  templateUrl: './edit-roomname-dialog.component.html',
  styleUrls: ['./edit-roomname-dialog.component.css']
})
export class RoomNameDialogComponent {
  baseUrl: String = 'http://localhost:50000';
  newRoomName: string;
  rooms: any = [];

  constructor(
    public dialogRef: MatDialogRef<RoomNameDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private httpClient: HttpClient
  ) {
    this.newRoomName = data.currentName;
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

  //Method for changing Room Name
  updateRoomName(roomId: number, newName: string): void {
    this.httpClient.post<any>(this.baseUrl+`/UpdateRoomName?roomId=${this.data.roomId}&newRoomName=${newName}`, null).subscribe(() => {
        console.log(`Room name updated successfully for room ID ${roomId}`);
        this.getAllRooms(); //Update the room list after the name change
      }, (error) => {
        console.error('Error while updating room name:', error);
      });
  }

  onSave(): void {
    if (this.newRoomName) {
      this.updateRoomName(this.data.roomId, this.newRoomName); //call Function for Changing Roomname 
      this.dialogRef.close(this.newRoomName);
      window.location.reload(); //Reload window to display new name
    } else {
      alert('Geben Sie einen neuen Raumnamen ein!');
    }
    this.dialogRef.close(this.newRoomName);
  }

  onCancel(): void {
    this.dialogRef.close(); //close Pop Up Window
  }
}
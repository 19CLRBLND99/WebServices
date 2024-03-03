// room-dialog.component.ts

import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { EditTemperatureDialogComponent } from '../edit-temperature-dialog/edit-temperature-dialog.component';
import { RoomNameDialogComponent } from '../edit-roomname-dialog/edit-roomname-dialog.component'
import { ConfirmationDialogService } from '../confirmation-dialog/confirmation-dialog.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-room-dialog',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
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
    private httpClient: HttpClient,
    private confirmationDialogService: ConfirmationDialogService
  ) { console.log(data); }

  getAllRooms(): void {
    this.httpClient.get(this.baseUrl+'/GetAllRooms').subscribe((data: any) => {
      this.rooms = data;
      console.log(this.rooms);
      this.getTemperaturesForRooms();
    });
  }

  async deleteRoom(): Promise<void> {
    const confirmed = await this.confirmationDialogService.openConfirmationDialog(
      'Löschen bestätigen',
      'Sind Sie sicher, dass Sie diesen Raum löschen möchten?'
    );

    if (confirmed) {
      this.httpClient.delete(this.baseUrl + `/DeleteRoom?roomId=${this.data.room.roomId}`).subscribe((data: any) => {
        console.log(`Raum mit der ID ${this.data.room.roomId} wurde erfolgreich gelöscht.`);
        window.location.reload();
      });
    } else {
      console.log('Löschen abgebrochen');
    }
  }

  getTemperaturesForRooms(): void {
    this.rooms.forEach((room) => {
      this.httpClient.get(this.baseUrl+`/GetRoomWithThermostatByRoomId?roomId=${room.roomId}`).subscribe((temperatureData: any) => {
        room.temperature = temperatureData.thermostat.temperature;
      });
    });
  }

  addThermostatAndAssignToRoom(): void {
      this.httpClient.post<any>(this.baseUrl+`/AssignThermostatToRoom?roomId=${this.data.room.roomId}&thermostatId=${this.data.room.roomId}`, null)
        .subscribe(() => {
          console.log(`Thermostat added and assigned to room ID ${this.data.room.roomId}`);
          this.getAllRooms(); // Update the room list after adding the thermostat
          window.location.reload();
        }, (error) => {
          console.error('Error while assigning thermostat to room:', error);
        });
  }

  openChangeTemperatureWindow(roomId: number): void {
    const dialogRef = this.dialog.open(EditTemperatureDialogComponent, {
      width: '250px',
      data: { roomId: roomId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        // Hier können Sie die neue Temperatur verarbeiten
        console.log('Neue Temperatur:', result);
        // this.changeTemperature(roomId, result);
      }
    });
  }

  openChangeRoomNameWindow(roomId: number, roomName: string): void {
    const dialogRef = this.dialog.open(RoomNameDialogComponent, {
      width: '250px',
      data: { roomId: roomId, currentName: roomName }
    });
  
    dialogRef.afterClosed().subscribe(result => {
      console.log('Das Dialogfenster wurde geschlossen');
      // Hier können Sie die Logik implementieren, die nach dem Schließen des Dialogfensters ausgeführt werden soll
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}

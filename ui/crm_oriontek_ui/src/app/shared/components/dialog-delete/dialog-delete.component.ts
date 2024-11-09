// src/app/Dialogs/dialog-delete/dialog-delete.component.ts
import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
  styleUrls: ['./dialog-delete.component.css']
})
export class DialogDeleteComponent {
  constructor(public dialogRef: MatDialogRef<DialogDeleteComponent>) {}

  onConfirmDelete() {
    this.dialogRef.close({ success: true });
  }

  onCancel() {
    this.dialogRef.close();
  }
}

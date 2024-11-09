
import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-add-edit',
  templateUrl: './dialog-add-edit.component.html',
  styleUrls: ['./dialog-add-edit.component.css']
})
export class DialogAddEditComponent {
  constructor(public dialogRef: MatDialogRef<DialogAddEditComponent>) {}

  onSave() {
    // Lógica para guardar los datos
    this.dialogRef.close({ success: true });
  }

  onCancel() {
    this.dialogRef.close();
  }
}

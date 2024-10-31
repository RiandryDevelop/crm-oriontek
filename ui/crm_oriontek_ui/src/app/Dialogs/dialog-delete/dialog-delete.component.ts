import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Client } from 'src/app/models/client.model';

@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
})
export class DialogDeleteComponent implements OnInit {
  
  constructor(
    private dialogRef: MatDialogRef<DialogDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public clientData: Client
  ) {}

  ngOnInit(): void {}

  // Confirm deletion and close dialog
  confirmDelete(): void {
    this.dialogRef.close("delete");
  }
}

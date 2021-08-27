import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-item-modal',
  templateUrl: './new-item-modal.component.html',
  styleUrls: ['./new-item-modal.component.css']
})
export class NewItemModalComponent implements OnInit {
 model!: string
 constructor(
   public dialogRef: MatDialogRef<MatDialog>,
   @Inject(MAT_DIALOG_DATA) public data: any) {
     
   }

 ngOnInit(): void {
   
 }
 onNoClick(): void {
   this.dialogRef.close();
 }
}

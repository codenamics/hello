import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.css']
})
export class ItemDetailsComponent implements OnInit {

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

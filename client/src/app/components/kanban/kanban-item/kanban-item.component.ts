import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { board } from 'src/app/_models/board';
import { item } from 'src/app/_models/item';
import { ItemService } from 'src/app/_services/item.service';
import { ItemModalComponent } from '../../modal/item-modal/item-modal.component';

@Component({
  selector: 'app-kanban-item',
  templateUrl: './kanban-item.component.html',
  styleUrls: ['./kanban-item.component.css'],
})
export class KanbanItemComponent implements OnInit {
  constructor(public dialog: MatDialog, private itemService: ItemService) {}
  @Input()
  items: any;
  ngOnInit(): void {}
  edit(item: any): void {
    
    const dialogRef = this.dialog.open(ItemModalComponent, {
      width: '350px',
      height: '270px',
      data: { title: item.title, description: item.description },
    });

    dialogRef.afterClosed().subscribe((result) => {
      var upItem: any = {
        ...item,
        title: result.title,
        description: result.description,
       
        
      };
      console.log(upItem)
      this.itemService.updateItem(upItem).subscribe(() => {
        const index = this.items.indexOf(item)
        if (index > -1) {
         this.items.splice(index, 1,upItem);
        }
      });
    });
  }
}

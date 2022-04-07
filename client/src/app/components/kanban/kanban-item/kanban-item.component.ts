import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { item } from 'src/app/_models/item/item';
import { itemToDelete } from 'src/app/_models/item/itemToDelete';
import { ItemService } from 'src/app/_services/item.service';
import { ItemDetailsComponent } from '../../modal/item-details/item-details.component';
import { NewItemModalComponent } from '../../modal/new-item-modal/new-item-modal.component';

@Component({
  selector: 'app-kanban-item',
  templateUrl: './kanban-item.component.html',
  styleUrls: ['./kanban-item.component.css'],
})
export class KanbanItemComponent implements OnInit {
  constructor(public dialog: MatDialog, private itemService: ItemService) {}
  @Input()
  items!: any;
  randomNum: number | undefined = 0;
  ngOnInit(): void {
    this.random();
    console.log(this.randomNum);
  }
  random() {
    return (this.randomNum =
      (Math.floor(Math.random() * 11) / Math.floor(Math.random() * 11) + 1) *
      10);
  }
  details(item: item): void {
    const dialogRef = this.dialog.open(ItemDetailsComponent, {
      width: '75vw',

      data: { title: item.title, description: item.description },
    });

    dialogRef.afterClosed().subscribe((result) => {
      var upItem: item = {
        ...item,
        title: result.title,
        description: result.description,
      };

      this.itemService.updateItem(upItem).subscribe(() => {
        const index = this.items.indexOf(item);
        if (index > -1) {
          this.items.splice(index, 1, upItem);
        }
      });
    });
  }
  edit(item: item): void {
    const dialogRef = this.dialog.open(NewItemModalComponent, {
      width: '50vw',
      data: { title: item.title, description: item.description },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(result);
      if (!result) {
        return;
      }
      var upItem: item = {
        ...item,
        title: result.title,
        description: result.description,
      };

      this.itemService.updateItem(upItem).subscribe(() => {
        const index = this.items.indexOf(item);
        if (index > -1) {
          this.items.splice(index, 1, upItem);
        }
      });
    });
  }
  deleteItem(item: itemToDelete) {
    const index = this.items.indexOf(item);
    if (index > -1) {
      this.items.splice(index, 1);
    }
    this.itemService
      .deleteListItem(item.listId, item.id, this.items)
      .subscribe(() => {});
  }
}

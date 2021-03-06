import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { board } from 'src/app/_models/board';
import { item } from 'src/app/_models/item/item';
import { ItemService } from 'src/app/_services/item.service';
import { ListsService } from 'src/app/_services/lists.service';
import { ModalComponent } from '../../modal/basic-modal/modal.component';
import { v4 as uuidv4 } from 'uuid';
import { list } from 'src/app/_models/list';
import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { ItemOrderBeTweenLists } from 'src/app/_models/item/itemBetweenListsOrder';
import { NewItemModalComponent } from '../../modal/new-item-modal/new-item-modal.component';
@Component({
  selector: 'app-kanban-list',
  templateUrl: './kanban-list.component.html',
  styleUrls: ['./kanban-list.component.css'],
})
export class KanbanListComponent implements OnInit {
  title!: string;
  description!: string;
  @Input() board!: board;
  loading: boolean | undefined;
  constructor(
    private listsService: ListsService,
    private itemService: ItemService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {}
  addItem(id: string): void {
    const dialogRef = this.dialog.open(NewItemModalComponent, {
      width: '50vw',
     
      data: { title: this.title, description: this.description },
    });

    dialogRef.afterClosed().subscribe((result) => {
      var newItem: item = {
        id: uuidv4(),
        title: result.title,
        description: result.description,
        order: 0,
      };
      this.itemService.addItem(id, newItem).subscribe((list) => {
        this.board.lists.find((x) => x.id === id)?.cards.unshift(newItem);
        this.loading = false;
      });
    });
  }
  deleteList(boardId: string, list: list) {
    const index = this.board.lists.indexOf(list);
    if (index > -1) {
      this.board.lists.splice(index, 1);
    }
    this.listsService
      .deleteList(boardId, list.id, this.board.lists)
      .subscribe(() => {});
  }
  getConnectedList(): string[] {
    return this.board.lists.map((x) => `${x.id}`);
  }
  drop(event: CdkDragDrop<any[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );

      this.itemService
        .reOrderItems(event.container.id, event.container.data)
        .subscribe(() => {});
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
      let newListOrderItem: ItemOrderBeTweenLists = {
        container: {
          id: event.container.id,
          cards: [...event.container.data],
        },
        previousContainer: {
          id: event.previousContainer.id,
          cards: [...event.previousContainer.data],
        },
      };
      this.listsService
        .reOrderItemBetweenLists(newListOrderItem)
        .subscribe(() => {});
    }
  }
}

import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { board } from 'src/app/_models/board';
import { item } from 'src/app/_models/item';
import { ItemService } from 'src/app/_services/item.service';
import { ListsService } from 'src/app/_services/lists.service';
import { ModalComponent } from '../../modal/modal/modal.component';
import { v4 as uuidv4 } from 'uuid';
import { list } from 'src/app/_models/list';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { ItemOrderBeTweenLists } from 'src/app/_models/itemBetweenListsOrder';
@Component({
  selector: 'app-kanban-list',
  templateUrl: './kanban-list.component.html',
  styleUrls: ['./kanban-list.component.css']
})
export class KanbanListComponent implements OnInit {
  title!: string;
  @Input() board!: board;
  loading: boolean | undefined;
  constructor(

    
    private listsService: ListsService,
    private itemService: ItemService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
  }
  addItem(id: string): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      width: '350px',
      height: '200px',
      data: { title: this.title, placeholder: 'Item title' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      var newItem: item = {
        id: uuidv4(),
        title: result.title,
        order: 0
        
      };
      this.itemService.addItem(id, newItem).subscribe((list) => {
        this.board.lists.find(x => x.id === id)?.items.unshift(newItem);

        this.loading = false;
      });
    });
  }
  deleteList(boardId: string, list: list) {
    const index = this.board.lists.indexOf(list);
    if (index > -1) {
      this.board.lists.splice(index, 1);
    }
    this.listsService.deleteList(boardId, list.id, this.board.lists).subscribe(() => {

    });
  }
  drop(event: CdkDragDrop<any[]>) {
    if (event.previousContainer === event.container) {
      console.log(event);

      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );

      this.itemService
        .reOrderItems(event.container.id, event.container.data)
        .subscribe(() => { });
    } else {
      console.log(event);
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
      let newListOrderItem: ItemOrderBeTweenLists = {
        container: {
          id: event.container.id,
          items: [...event.container.data],
        },
        previousContainer: {
          id: event.previousContainer.id,
          items: [...event.previousContainer.data],
        },
      };

      this.listsService
        .reOrderItemBetweenLists(newListOrderItem)
        .subscribe(() => { });
    }
  }
}

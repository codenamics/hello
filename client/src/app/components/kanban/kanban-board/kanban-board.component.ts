import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { board } from 'src/app/_models/board';
import { ItemOrderBeTweenLists } from 'src/app/_models/itemBetweenListsOrder';
import { list } from 'src/app/_models/list';

import { BoardService } from 'src/app/_services/board.service';
import { ItemService } from 'src/app/_services/item.service';
import { ListsService } from 'src/app/_services/lists.service';

@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css'],
})
export class KanbanBoardComponent implements OnInit {
  board!: board;
  loading: boolean | undefined;
  constructor(
    private route: ActivatedRoute,
    private boardService: BoardService,
    private listsService: ListsService,
    private itemService: ItemService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.route.params.subscribe((params) => {
      this.boardService.getBoard(params.id).subscribe((board) => {
        this.board = board;
        this.loading = false;
      });
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

      
    this.itemService.reOrderItems(event.container.id ,event.container.data).subscribe(()=>{})
    } else {
      console.log(event);
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
      let newListOrderItem : ItemOrderBeTweenLists = {
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
        .subscribe(() => {});
    }
  }
  dropColumns(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.board.lists, event.previousIndex, event.currentIndex);
    var newListOrder: list[] = this.board.lists.map((x) => ({
      id: x.id,
      title: x.title,
      items: [],
    }));

    this.listsService.reOrderLists(this.board.id, newListOrder).subscribe(
      () => {},
      (err) => {
        console.log(err);
      }
    );
  }
}

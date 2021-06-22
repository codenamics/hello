import {
  CdkDragDrop,
  moveItemInArray,
} from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { board } from 'src/app/_models/board';

import { list } from 'src/app/_models/list';
import { v4 as uuidv4 } from 'uuid';
import { BoardService } from 'src/app/_services/board.service';
import { ItemService } from 'src/app/_services/item.service';
import { ListsService } from 'src/app/_services/lists.service';
import { ModalComponent } from '../../modal/modal/modal.component';


@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css'],
})
export class KanbanBoardComponent implements OnInit {
  title!: string;
  board!: board;
  loading: boolean | undefined;
  constructor(
    private route: ActivatedRoute,
    private boardService: BoardService,
    private listsService: ListsService,
    public dialog: MatDialog
  ) { }

  ngOnInit() {
    this.loading = true;
    this.route.params.subscribe((params) => {
      this.boardService.getBoard(params.id).subscribe((board) => {
        this.board = board;
        this.loading = false;
      });
    });
  }
  dropColumns(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.board.lists, event.previousIndex, event.currentIndex);
    var newListOrder: list[] = this.board.lists.map((x) => ({
      id: x.id,
      title: x.title,
      items: [],
    }));

    this.listsService.reOrderLists(this.board.id, newListOrder).subscribe(
      () => { },
      (err) => {
        console.log(err);
      }
    );
  }
  addList(): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      width: '350px',
      height: '200px',
      data: { title: this.title, placeholder: 'List title' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(result)
      if(result.title !== undefined){
        var newList: list = {
          id: uuidv4(),
          title: result.title,
          items: [],
        };
        this.listsService.addList(this.board.id, newList).subscribe((list) => {
          this.board.lists.unshift(newList);
          this.loading = false;
        });
      }
     
    });
  }
 
 
 
}

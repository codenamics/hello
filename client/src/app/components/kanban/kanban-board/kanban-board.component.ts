import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { board } from 'src/app/_models/board';

import { BoardService } from 'src/app/_services/board.service';

@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css'],
})
export class KanbanBoardComponent implements OnInit {
  board!: board;
  loading: boolean | undefined;
  constructor( private route: ActivatedRoute, private boardService: BoardService){

  }

   ngOnInit() {
    this.loading = true;
    this.route.params.subscribe((params) => {
      
      this.boardService.getBoard(params.id).subscribe((board)=>{
 
        this.board = board
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
      
      // var dd = this.board.arr.filter(
      //   (x: any) => x.id.toString() == event.container.id.toString()
      // );
      // console.log(dd);
    } else {
      console.log(event);
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }

    
  }
  dropColumns(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.board.lists, event.previousIndex, event.currentIndex);
   

    //  var dd = this.board.map(x => ({
    //    id: x.id, listName: x.listName}))
    //   console.log(dd)

  }
  // addList() {
  //   this.board.unshift({
  //     id: 5,
  //     listName: 'added',
  //     listItems: [
  //       'Get up',
  //       'ed viverra venenatis enim a malesuada. Sed auctor fringilla augue ut pretium. Aenean vitae feugiat nunc. Nam vitae justo erat. Sed condimentum dignissim nibh vel blandit. Morbi nulla nisi, vehicula',
  //       'Take a shower',
  //       'Check e-mail',
  //       'Walk dog',
  //     ],
  //   });

  //   localStorage['data'] = JSON.stringify(this.board);
  // }
  // addItem() {
  //   var list = this.board.find((x) => x.id == 1);
  //   console.log(list);
  //   list?.listItems.unshift('test');

  //   localStorage['data'] = JSON.stringify(this.board);
  // }
}

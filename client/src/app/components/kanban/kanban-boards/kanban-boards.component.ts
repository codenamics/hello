import { Component, OnInit } from '@angular/core';
import { board } from 'src/app/_models/board';
import { BoardService } from 'src/app/_services/board.service';
import { v4 as uuidv4 } from 'uuid';
@Component({
  selector: 'app-kanban-boards',
  templateUrl: './kanban-boards.component.html',
  styleUrls: ['./kanban-boards.component.css']
})
export class KanbanBoardsComponent implements OnInit {
  boards: board[] = [];
  loading: boolean = false;
  constructor(private BoardService: BoardService) { }

  ngOnInit(): void {
    this.BoardService.getAllBoards().subscribe((boards)=>{
      this.boards = boards;
    })
  }
  addBoard(){
    this.loading = true;
    var board : board = {
      id:  uuidv4(),
      name: "Random",
      lists: []
    }
    this.BoardService.addBoard(board).subscribe(board =>{
       this.boards.push(board)
       this.loading = false
    })
  }
}

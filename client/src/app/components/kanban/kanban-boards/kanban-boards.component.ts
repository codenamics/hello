import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { board } from 'src/app/_models/board';
import { BoardService } from 'src/app/_services/board.service';
import { v4 as uuidv4 } from 'uuid';
import { ModalComponent } from '../../modal/modal/modal.component';
@Component({
  selector: 'app-kanban-boards',
  templateUrl: './kanban-boards.component.html',
  styleUrls: ['./kanban-boards.component.css'],
})
export class KanbanBoardsComponent implements OnInit {
  title!: string;
  boards: board[] = [];
  loading: boolean = false;
  constructor(private BoardService: BoardService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.BoardService.getAllBoards().subscribe((boards) => {
      this.boards = boards;
    });
  }
  deleteBoard(id: string, board: board) {
    this.BoardService.deleteBoard(id).subscribe(() => {
      const index = this.boards.indexOf(board);
      if (index > -1) {
        this.boards = this.boards.splice(index, 1);
      }
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      width: '350px',
      height: '200px',
      data: { title: this.title },
    });

    dialogRef.afterClosed().subscribe((result) => {
      var board: board = {
        id: uuidv4(),
        title: result.title,
        lists: [],
      };
      this.BoardService.addBoard(board).subscribe((board) => {
        this.boards.unshift(board);
        this.loading = false;
      });
    });
  }
}

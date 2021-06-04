import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { stored_data } from 'src/app/_models/model';

@Component({
  selector: 'app-kanban-board',
  templateUrl: './kanban-board.component.html',
  styleUrls: ['./kanban-board.component.css']
})
export class KanbanBoardComponent implements OnInit {

  stored_data: stored_data[] = [];
  loading: boolean | undefined;
  async ngOnInit() {
    this.loading = true;

    this.loadData();
    this.loading = false;
  }

  loadData() {
    if (localStorage.getItem('data') === null) {
      localStorage['data'] = JSON.stringify([
        {
          id:1,
          listName: 'todo',
          listItems: [
            'Donec sit amet erat eget risus porttitor bibendum placerat quis sem. Nullam semper luctus velit, eu rutrum arcu pellentesque eget. Aliquam vitae tristique ligula. Aenean gravida tincidunt ante, ac facilisis lacus vulputate et. Pellentesque sed diam augue. Morbi mollis diam sit amet libero porttitor porta. Etiam semper nisi purus, et laoreet mauris porta ut. Suspendisse condimentum pharetra iaculis.',
            'Pick up groceries',
            'ed viverra venenatis enim a malesuada. Sed auctor fringilla augue ut pretium. Aenean vitae feugiat nunc. Nam vitae justo erat. Sed condimentum dignissim nibh vel blandit. Morbi nulla nisi, vehicula',
            'Fall asleep',
            'Get to work',
            'Pick up groceries',
            'Go home',
            'Fall asleep',
            'Get to work',
          ],
        },
        {id:2,
          listName: 'done',
          listItems: [
            'Get up',
            'ed viverra venenatis enim a malesuada. Sed auctor fringilla augue ut pretium. Aenean vitae feugiat nunc. Nam vitae justo erat. Sed condimentum dignissim nibh vel blandit. Morbi nulla nisi, vehicula',
            'Take a shower',
            'Check e-mail',
            'Walk dog',
          ],
        },
        {id:3,
          listName: 'inProgress',
          listItems: [
            'CV',
            'Donec sit amet erat eget risus porttitor bibendum placerat quis sem. Nullamiam augue. Morbi mollis diam sit amet libero porttitor porta. Etiam semper nisi purus, et laoreet mauris porta ut. Suspendisse condimentum pharetra iaculis.',
            'Donec sit amet erat eget risus porttitor bibendum placerat quis sem. NulAenean gravida tincidunt ante, ac facilisis lacus vulputate et. Pellentesque sed diam augue. Morbi mollis diam sit amet libero porttitor porta. Etiam semper nisi purus, et laoreet mauris porta ut. Suspendisse condimentum pharetra iaculis.',
            'Donec sit amet erat eget risus porttitor bibendum placerat quis sem. Nullam semper luctus velit, eu rutrum arcu pellentesque eget. Aliquam vitae tris diam augue. Morbi mollis diam sit amet libero porttitor porta. Etiam semper nisi purus, et laoreet mauris porta ut. Susra iaculis.',
          ],
        },

        {id:4,
          listName: 'backlog',
          listItems: [
            'Get up',
            'ed viverra venenatis enim a malesuada. Sed auctor fringilla augue ut pretium. Aenean vitae feugiat nunc. Nam vitae justo erat. Sed condimentum dignissim nibh vel blandit. Morbi nulla nisi, vehicula',
            'Take a shower',
            'Check e-mail',
            'Walk dog',
          ],
        },
        
      ]);
    }

    this.stored_data = JSON.parse(localStorage['data']);
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
    console.log(this.stored_data);
    localStorage['data'] = JSON.stringify(this.stored_data);
  }
  dropColumns(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.stored_data, event.previousIndex, event.currentIndex);
    console.log(this.stored_data);
    localStorage['data'] = JSON.stringify(this.stored_data);
  }
  addList(){
  this.stored_data.unshift(
      {
        id:5,
        listName: 'added',
        listItems: [
          'Get up',
          'ed viverra venenatis enim a malesuada. Sed auctor fringilla augue ut pretium. Aenean vitae feugiat nunc. Nam vitae justo erat. Sed condimentum dignissim nibh vel blandit. Morbi nulla nisi, vehicula',
          'Take a shower',
          'Check e-mail',
          'Walk dog',
        ],
      },
    )
    
    localStorage['data'] = JSON.stringify(this.stored_data);
  }
  addItem(){
   var list = this.stored_data.find(x => x.id == 1);
   console.log(list)
    list?.listItems.unshift('test')

    localStorage['data'] = JSON.stringify(this.stored_data);
  }
}

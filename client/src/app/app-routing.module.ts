import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { KanbanBoardComponent } from './components/kanban/kanban-board/kanban-board.component';
import { KanbanBoardsComponent } from './components/kanban/kanban-boards/kanban-boards.component';

const routes: Routes = [
  {
    path: '',
    component: KanbanBoardsComponent
  },
  {
    path: 'home',
    component: HomeComponent,
    children: [
      {
        path: 'board/:id',
        component: KanbanBoardComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {DragDropModule} from "@angular/cdk/drag-drop";
import { HomeComponent } from './components/home/home.component';
import { SideBarComponent } from './components/side-bar/side-bar.component';
import { NavComponent } from './components/nav/nav.component';

import { KanbanBoardComponent } from './components/kanban/kanban-board/kanban-board.component';
import { KanbanListComponent } from './components/kanban/kanban-list/kanban-list.component';
import { KanbanItemComponent } from './components/kanban/kanban-item/kanban-item.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SideBarComponent,
    NavComponent,
    KanbanBoardComponent,
    KanbanListComponent,
    KanbanItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DragDropModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

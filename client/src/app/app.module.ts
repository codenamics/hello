import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {DragDropModule} from "@angular/cdk/drag-drop";
import { HomeComponent } from './components/home/home.component';
import { SideBarComponent } from './components/side-bar/side-bar.component';
import { NavComponent } from './components/nav/nav.component';
import { FormsModule } from '@angular/forms';
import { KanbanBoardComponent } from './components/kanban/kanban-board/kanban-board.component';
import { KanbanListComponent } from './components/kanban/kanban-list/kanban-list.component';
import { KanbanItemComponent } from './components/kanban/kanban-item/kanban-item.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatIconModule} from '@angular/material/icon';
import { KanbanBoardsComponent } from './components/kanban/kanban-boards/kanban-boards.component';
import { ModalComponent } from './components/modal/basic-modal/modal.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { NewItemModalComponent } from './components/modal/new-item-modal/new-item-modal.component';

import {ScrollingModule} from '@angular/cdk/scrolling';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { ItemDetailsComponent } from './components/modal/item-details/item-details.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SideBarComponent,
    NavComponent,
    KanbanBoardComponent,
    KanbanListComponent,
    KanbanItemComponent,
    KanbanBoardsComponent,
    ModalComponent,
    NewItemModalComponent,
    ItemDetailsComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DragDropModule,
    FormsModule,
    MatDialogModule,
    MatIconModule,
    MatProgressSpinnerModule,
    ScrollingModule,
    MatProgressBarModule
   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

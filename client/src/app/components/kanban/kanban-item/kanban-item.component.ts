import { Component, Input, OnInit } from '@angular/core';
import {  stored_data } from 'src/app/_models/model';

@Component({
  selector: 'app-kanban-item',
  templateUrl: './kanban-item.component.html',
  styleUrls: ['./kanban-item.component.css']
})
export class KanbanItemComponent implements OnInit {
  
  constructor() { }
  @Input()
  item: any
  ngOnInit(): void {

  }

}

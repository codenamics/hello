import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { list } from '../_models/list';

@Injectable({
  providedIn: 'root'
})
export class ListsService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  addList(id: string, list: list){
    return this.http.post(this.baseUrl + 'list/' + id,  list);
  }
  deleteList(boardId: string, listId: string, lists: any){
    return this.http.put(this.baseUrl + `list/${boardId}/${listId}`, lists);
  }

  reOrderLists(id: string, lists: list[]){
    return this.http.put(this.baseUrl + 'list/' + id,  lists);
  }
  reOrderItemBetweenLists(data: any){
    console.log(data)
    return this.http.put(this.baseUrl + 'card/',  data);
  }
 
}

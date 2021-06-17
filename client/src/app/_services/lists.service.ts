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

  reOrderLists(id: string, lists: list[]){
    return this.http.put(this.baseUrl + 'list/' + id,  lists);
  }
  reOrderItemBetweenLists(data: any){
    return this.http.put(this.baseUrl + 'item/',  data);
  }
 
}

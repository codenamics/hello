import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { item } from '../_models/item/item';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  reOrderItems(id: string, items: item[]){
    return this.http.put(this.baseUrl + 'card/itemOrder/' + id,  items);
  }
  addItem(id: string, item: item){
    return this.http.post(this.baseUrl + 'card/' + id,  item);
  }
  updateItem(item: any){
    return this.http.put(this.baseUrl + 'card/updateItem/',  item);
  }
  deleteListItem(listId: string, itemId: string, itemList: item[]){
    return this.http.put(this.baseUrl + `card/${listId}/${itemId}`, itemList);
  }
  
}

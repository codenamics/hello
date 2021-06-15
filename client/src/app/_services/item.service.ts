import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { item } from '../_models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  reOrderItems(id: string, items: item[]){
    return this.http.put(this.baseUrl + 'item/itemOrder/' + id,  items);
  }
  
}

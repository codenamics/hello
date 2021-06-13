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

  reOrderLists(id: string, lists: list[]){
    return this.http.put(this.baseUrl + 'list/' + id,  lists);
  }
 
}

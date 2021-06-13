import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { board } from '../_models/board';

@Injectable({
  providedIn: 'root'
})
export class BoardService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  addBoard(board: board){
    return this.http.post<board>(this.baseUrl + 'board', board);
  }
  getAllBoards(){
    return this.http.get<board[]>(this.baseUrl + 'board')
  }
  getBoard(id: string){
    return this.http.get<board>(this.baseUrl + 'board/' + id);
  }
}

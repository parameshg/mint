import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { User } from '../entities/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ContextService } from './context.service';

@Injectable()
export class UserService {

  constructor(private http: HttpClient,
    private context: ContextService) {
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(environment.api + "/users", this.context.getHeaders());
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(environment.api + "/users/" + id, this.context.getHeaders());
  }
}

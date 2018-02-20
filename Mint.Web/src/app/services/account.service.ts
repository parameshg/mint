import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { Account } from '../entities/account';
import { ContextService } from './context.service';

@Injectable()
export class AccountService {

  constructor(private http: HttpClient,
    private context: ContextService) {
  }

  getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(environment.api + "/accounts", this.context.getHeaders());
  }

  getAccountById(id: number): Observable<Account> {
    return this.http.get<Account>(environment.api + "/accounts/" + id, this.context.getHeaders());
  }

  createAccount(tag: Account): Observable<boolean> {
    return this.http.post<boolean>(environment.api + "/accounts", tag, this.context.getHeaders());
  }

  updateAccount(tag: Account): Observable<boolean> {
    return this.http.put<boolean>(environment.api + "/accounts", tag, this.context.getHeaders());
  }

  deleteAccount(id: number): Observable<boolean> {
    return this.http.delete<boolean>(environment.api + "/accounts/" + id, this.context.getHeaders());
  }
}

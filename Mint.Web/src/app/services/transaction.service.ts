import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { Transaction } from '../entities/transaction';
import { ContextService } from './context.service';

@Injectable()
export class TransactionService {

  constructor(private http: HttpClient,
    private context: ContextService) {
  }

  getTransactionsCount(month: number, year: number): Observable<number> {
    return this.http.get<number>(environment.api + "/transactions/year/" + year + "/month/" + month + "/count", this.context.getHeaders());
  }

  getTransactions(month: number, year: number, count: number): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(environment.api + "/transactions/year/" + year + "/month/" + month + "/page/1/" + count, this.context.getHeaders());
  }

  getTransactionById(id: number): Observable<Transaction> {
    return this.http.get<Transaction>(environment.api + "/transactions/" + id, this.context.getHeaders());
  }

  createTransaction(transaction: Transaction): Observable<boolean> {
    return this.http.post<boolean>(environment.api + "/transactions", transaction, this.context.getHeaders());
  }

  updateTransaction(transaction: Transaction): Observable<boolean> {
    return this.http.put<boolean>(environment.api + "/transactions", transaction, this.context.getHeaders());
  }

  deleteTransaction(id: number): Observable<boolean> {
    return this.http.delete<boolean>(environment.api + "/transactions/" + id, this.context.getHeaders());
  }
}

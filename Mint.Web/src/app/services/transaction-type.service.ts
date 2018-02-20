import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TransactionType } from '../entities/transaction-type';
import { environment } from '../../environments/environment';
import { ContextService } from './context.service';

@Injectable()
export class TransactionTypeService {

  constructor(private http: HttpClient,
    private context: ContextService) {
  }

  getTypes(): Observable<TransactionType[]> {
    return this.http.get<TransactionType[]>(environment.api + "/types", this.context.getHeaders());
  }

  getTypeById(id: number): Observable<TransactionType> {
    return this.http.get<TransactionType>(environment.api + "/types/" + id, this.context.getHeaders());
  }

  getTypeByName(name: string): Observable<TransactionType> {
    return this.http.get<TransactionType>(environment.api + "/types/name/" + name, this.context.getHeaders());
  }
}

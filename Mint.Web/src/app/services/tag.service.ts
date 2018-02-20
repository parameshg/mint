import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Tag } from '../entities/tag';
import { environment } from '../../environments/environment';
import { ContextService } from './context.service';

@Injectable()
export class TagService {

  constructor(private http: HttpClient,
    private context: ContextService) {
  }

  getTags(): Observable<Tag[]> {
    return this.http.get<Tag[]>(environment.api + "/tags", this.context.getHeaders());
  }

  getTagById(id: number): Observable<Tag> {
    return this.http.get<Tag>(environment.api + "/tags/" + id, this.context.getHeaders());
  }

  createTag(tag: Tag): Observable<boolean> {
    return this.http.post<boolean>(environment.api + "/tags", tag, this.context.getHeaders());
  }

  updateTag(tag: Tag): Observable<boolean> {
    return this.http.put<boolean>(environment.api + "/tags", tag, this.context.getHeaders());
  }

  deleteTag(id: number): Observable<boolean> {
    return this.http.delete<boolean>(environment.api + "/tags/" + id, this.context.getHeaders());
  }
}

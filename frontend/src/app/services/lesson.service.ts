import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LessonDto } from '../dtos/lesson.dto';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class LessonService {
  private baseUrl = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) { }

  getAllByUserId(id: string): Observable<LessonDto[]> {
    return this.http.get<LessonDto[]>(`${this.baseUrl}/lesson/allLessonsByUser/${id}`);
  }
}

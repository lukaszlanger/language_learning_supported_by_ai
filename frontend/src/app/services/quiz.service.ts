import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { QuizDto } from '../dtos/quiz.dto';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getAllByLessonId(id: number): Observable<QuizDto[]> {
    return this.http.get<QuizDto[]>(`${this.baseUrl}/quiz/allByLessonId/${id}`);
  }

  generateQuizWithAI(lessonId: number): Observable<QuizDto> {
    return this.http.post<QuizDto>(`${this.baseUrl}/quiz/generateWithAI?lessonId=${lessonId}`, {});
  }

  update(quiz: QuizDto): Observable<QuizDto> {
    return this.http.put<QuizDto>(`${this.baseUrl}/quiz/update`, quiz);
  }
}

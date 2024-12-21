import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FlashcardDto } from '../dtos/flashcard.dto';
import { environment } from 'src/environments/environment.prod';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlashcardService {
  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}
  
  getAllByLessonId(id: number): Observable<FlashcardDto[]> {
    return this.http.get<FlashcardDto[]>(`${this.baseUrl}/flashcard/getAllByLessonId/${id}`);
  }

  createFlashcard(flashcard: FlashcardDto): Observable<FlashcardDto> {
    return this.http.post<FlashcardDto>(`${this.baseUrl}/flashcard/create`, flashcard);
  }

  generateFlashcardsWithAI(userId: string, lessonId: number): Observable<any> {
    const requestDto = {
      userId,
      lessonId,
    };
    return this.http.post(`${this.baseUrl}/flashcard/generateWithAI`, requestDto);
  }
  
}
